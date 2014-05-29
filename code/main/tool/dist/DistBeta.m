classdef DistBeta < handle &  Sampler & Density & Distribution ...
        & HasHellingerDistance
    %DISTBETA Summary of this class goes here
    %   Detailed explanation goes here
    
    properties (SetAccess=protected)
        alpha;
        beta;
        mean;
        variance;
        % from Distribution
        parameters;
    end
    
    methods
        %constructor
        function this = DistBeta(a, b)
            assert(~isempty(a));
            assert(~isempty(b));
            
            if size(a, 1)==1 && size(b, 1)==1 && size(a, 2) > 1
                % object array of many 1d Beta's
                assert(size(a, 2)==size(b, 2));
                n = size(a, 2);
                this = DistBeta.empty();
                for i=1:n
                    this(i) = DistBeta(a(i), b(i));
                end
            else
                % one object
                this.alpha = a;
                this.beta = b;
                this.mean =this.alpha/(this.alpha+this.beta);
                this.variance=a*b/( ((a+b)^2)*(a+b+1) );
                this.parameters = {a, b};
            end
            
        end
        
        function X = draw(this, N)
            % return 1xN sample from the distribution
            X = betarnd(this.alpha, this.beta, 1, N);
            
        end
        
        
        function D=density(this, X)
            % support DistBeta(1:2,3:4).density(0.3), for example
            D = betapdf(X, [this.alpha], [this.beta]);
        end
        
        function f=func(this)
            % return a function handle for density. Useful for plotting
            f = @(x)betapdf(x, this.alpha, this.beta);
        end
        
        function p=isproper(this)
            % return true if this is a proper distribution e.g., not have
            % negative alpha or beta
            a = this.alpha;
            b = this.beta;
            p = isfinite(a) && isfinite(b) && a>0 && b>0;
        end
        
        
        function X = sampling0(this, N)
            X = this.draw( N);
        end
        
        function dist=distHellinger(this, d2)
            % Compute Hellinger distance from this DistBeta to d2,
            % another DistBeta. Hellinger distance is bounded between 0
            % and 1
            % Refer: https://en.wikipedia.org/wiki/Hellinger_distance
            
            assert(isa(d2, 'DistBeta'));
            a1 = this.alpha;
            b1 = this.beta;
            a2 = d2.alpha;
            b2 = d2.beta;
            % call Matlab's beta function
            r = beta( (a1+a2)/2, (b1+b2)/2 )/sqrt(beta(a1,b1)*beta(a2,b2));
            dist = sqrt(1 - r);
            
        end
        
    end
    
    methods (Static)
        
        
    end
    
end

