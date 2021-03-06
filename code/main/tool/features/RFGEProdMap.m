classdef RFGEProdMap < FeatureMap & PrimitiveSerializable
    %RFGEPRODMAP Random Fourier features for expected product kernel 
    %using Gaussian kernel for mean embeddings.
    %    - Input is a Distribution or a DistArray
    %    - If the input Distribution is not a DistNormal, then convert it to one 
    %    by moment matching. Computing a random features for an ExpFam Distribution
    %    amounts to computing expectation of cos() under the distribution. 
    %    To avoid deriving E[cos()] for every new Distribution type, we simply 
    %    convert it to a DistNormal.
    %
    
    properties(SetAccess=protected)
        % Gaussian width^2 for the embedding kernel. Can be a scalar or a vector 
        % of the same size as the dimension of the input distribution.
        gwidth2;
        % number of random features
        numFeatures;

        % weight matrix. dim x numFeatures
        W;
        % coefficients b. 1 x numFeatures. 
        % Drawn from U[0, 2*pi]
        B;
    end
    
    methods

        function this=RFGEProdMap(gwidth2, numFeatures)
            % gwidth2 can be a vector of the same size as the dimension of the  
            % input distribution, or a scalar (isotropic width).
            assert(all(gwidth2>0));
            assert(numFeatures>0);
            this.gwidth2=gwidth2;
            this.numFeatures=numFeatures;
            % W,B will be initialized the first time features are generated.
            % dimension of W is determined at that time.
            this.W=[];
            this.B=[];
        end

        function Z=genFeatures(this, D)
            % Z = numFeatures x n
            % Support multivariate DistNormal
            assert(isa(D, 'Distribution') || isa(D, 'DistArray'));
            this.initMap(D);
            n=length(D);
            %M=[D.mean];
            [M, V]=RFGEProdMap.getMVs(D);
            % always make V 3d
            if size(V, 1)==1
                assert(ndims(V)<3);
                V=shiftdim(V, -1);
            end

            Z=zeros(this.numFeatures, n );
            W=this.W;
            B=this.B;
            C=cos(bsxfun(@plus, W'*M, B')); % numFeatures x n
            for j=1:n
                S=sum( (W'*V(:,:,j)).*W', 2);
                E=exp(-0.5*S);
                Z(:, j)=E;
            end
            Z=Z.*C*sqrt(2/this.numFeatures);
        end

        function M=genFeaturesDynamic(this, D)
            % Generate feature vectors in the form of DynamicMatrix.
            assert(isa(D, 'Distribution') || isa(D, 'DistArray'));
            this.initMap(D);
            g=this.getGenerator(D);
            n=length(D);
            M=DefaultDynamicMatrix(g, this.numFeatures, n);
        end

        function g=getGenerator(this, D)
            g=@(I, J)this.generator(D, I, J);

        end

        function Z=generator(this, D, I, J )
            % I=indices of features, J=sample indices 
            assert(isa(D, 'Distribution') || isa(D, 'DistArray'));

            this.initMap(D);
            % array of Distribution's
            DJ=D(J);
            [M, V]=RFGEProdMap.getMVs(DJ);
            % always make V 3d
            if size(V, 1)==1
                assert(ndims(V)<3);
                V=shiftdim(V, -1);
            end

            Z=zeros(length(I), length(J) );
            W=this.W(:, I);
            B=this.B(:, I);
            C=cos(bsxfun(@plus, W'*M, B')); % length(I) x length(J)
            for j=1:length(J)
                S=sum( (W'*V(:,:,j)).*W', 2);
                E=exp(-0.5*S);
                Z(:, j)=E;
            end
            Z=Z.*C*sqrt(2/this.numFeatures);
        end

        function fm=cloneParams(this, numFeatures)
            fm=RFGEProdMap(this.gwidth2, numFeatures);
        end

        function D=getNumFeatures(this)
            D = this.numFeatures;
        end

        function s=shortSummary(this)
            s = sprintf('%s(w^2=%.3f, #feat=%d)', ...
                mfilename, this.gwidth2, this.numFeatures);
        end

        % from PrimitiveSerializable interface
        function s=toStruct(this)
            s.className=class(this);
            s.gwidth2=this.gwidth2;
            s.numFeatures=this.numFeatures;
            s.W=this.W;
            s.B=this.B;
        end

        %function s=saveobj(this)

        %end
    end

    methods(Access=private)
        function initMap(this, D)
            % Initialize the map only once. 
            if isempty(this.W) 
                assert(isempty(this.B));
                % Fourier transform of a Gaussian kernel is a Gaussian with 
                % reciprocal width.
                assert(length(unique([D.d]))==1);
                dim=unique([D.d]); % .d from Distribution interface 
                assert(length(dim)==1, 'dimensions are not unique');
                %display(sprintf('dim: %d', dim));
                assert(length(this.gwidth2) == 1 || length(this.gwidth2) == dim, ...
                   'Input dim. does not match the length of param. vector.');
                this.W= diag(1./sqrt(this.gwidth2))*randn(dim, this.numFeatures);
                this.B=rand(1, this.numFeatures)*2*pi;

            end
        end
    end
    
    methods (Static)
        %function obj=loadobj(s)
        %end

        function map=createFromWeights(W, B)
            % Create an RFGEProdMap by manually specifying weight matrix W and B.
            % W is dim x numFeatures
            % B is 1 x numFeatures
            assert(isnumeric(W));
            assert(isnumeric(B));
            assert(size(W, 2)==size(B, 2));
            nf=size(W, 2);

            gwidth2=inf();
            map=RFGEProdMap(gwidth2, nf);
            map.W=W;
            map.B=B;
        end

        function [M, V]=getMVs(D)
            % Get means and variances of the Distribution's
            assert(isa(D, 'Distribution') || isa(D, 'DistArray'));
            dims=[D.d];
            isMulti=dims(1)>1;
            M=[D.mean];
            if isMulti
                V=cat(3, D.variance);
                assert(size(V, 1)>1);
            else
                V=[D.variance];
            end
        end

        function N=toDistNormal(D)
            % Convert Distribution (not DistArray) to DistNormal by moment matching 
            % D can be an array of Distribution's
            assert(isa(D, 'Distribution') );
            if isa(D, 'DistNormal')
                N=D;
                return;
            end
            fromBuilder=D.getDistBuilder();
            Mcell=fromBuilder.getMoments(D);
            toBuilder=DistNormalBuilder();
            N=toBuilder.fromMoments(Mcell);
            assert(isa(N, 'DistNormal'));
            assert(length(N)==length(D));

        end

        function baseMed=getBaseMedianHeuristic(X, subsamples)
            % Compute the base median heuristic to which factors will be multiplied
            % to generate a list of FeatureMap candidates.
            % baseMed = meddist(means)^2/meddist(variances)
            % I have no strong justification for this heuristic.
            %
            % *** This heuristic does not have a good justification ***
            %
            warning('getBaseMedianHeuristic() does not have a good justification.')
            assert(isa(X, 'DistArray') || isa(X, 'Distribution'));
            if nargin < 2
                subsamples=1500;
            end
            n=length(X);

            I=randperm(n, min(n, subsamples));
            RX=X(I);
            Means=[RX.mean];
            d=unique([RX.d]);
            if d==1
                Vars=[RX.variance];
            else
                Vars=cat(3, RX.variance);
                Vars=reshape(Vars, [d^2, length(I)]);
            end
            mmed2=meddistance(Means)^2;
            vmed=meddistance(Vars);
            % my own heuristic
            baseMed=mmed2/vmed;
        end

        function avgCov=getAverageCovariance(D, subsamples)
            % compute the average of the covariance matrices.
            assert(isa(D, 'DistArray') || isa(D, 'Distribution'));
            if nargin<2
                subsamples=5000;
            end

            n=length(D);
            I=randperm(n, min(n, subsamples));
            RD=D(I);
            d=unique([RD.d]);
            if d==1
                Vars=[RD.variance];
                avgCov=mean(Vars);
            else
                Vars=cat(3, RD.variance);
                avgCov=mean(Vars, 3);
            end
        end

        function FMs=candidatesAvgCov(X, medf, numFeatures, subsamples)

            % - Generate a cell array of FeatureMap candidates from medf 
            % a list  of factors to be  multipled with the average variances.
            % The average variance is computed by taking the average of 
            % the diagonal entries in the average of all covariance matrices.
            %
            % - By convention, we call it medf even though the factors are not 
            % multiplied with a median.
            %
            % - subsamples can be used to limit the samples used to compute
            % the heuristic.
            assert(isa(X, 'DistArray') || isa(X, 'Distribution'));
            assert(isnumeric(medf));
            assert(~isempty(medf));
            assert(all(medf>0));
            assert(numFeatures>0);
            if nargin < 4
                subsamples = 5000;
            end
            avgCov=RFGEProdMap.getAverageCovariance(X, subsamples);
            meanVar=mean(diag(avgCov));
            FMs=cell(1, length(medf));
            for i=1:length(medf)
                gw2=meanVar*medf(i);
                FMs{i}=RFGEProdMap(gw2, numFeatures);
            end
        end

        function FMs = candidates(X, medf,  numFeatures, subsamples )
            % - Generate a cell array of FeatureMap candidates from medf 
            % a list  of factors to be  multipled with the 
            % median heuristic.
            %
            % - subsamples can be used to limit the samples used to compute
            % median distance.
            assert(isa(X, 'DistArray') || isa(X, 'Distribution'));
            assert(isnumeric(medf));
            assert(~isempty(medf));
            assert(all(medf > 0));
            if nargin < 4
                subsamples = 1500;
            end
            baseMed=RFGEProdMap.getBaseMedianHeuristic(X, subsamples);
            FMs=cell(1, length(medf));
            for i=1:length(medf)
                gw2=baseMed*medf(i);
                FMs{i}=RFGEProdMap(gw2, numFeatures);
            end

        end %end candidates() method
    end
end

