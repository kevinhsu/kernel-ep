function [ X, T, Xout, Tout ] = gentrain_dist2(X, T, op)
%GENTRAIN_DIST2 Generate training data for learning a conditional mean
%embedding operator mapping distributions to distribution.
% The function is for the case where the factor takes 2 incoming messages
% m_x and m_t and outputs m_out. Assume the factor is p(x|t).
%
% Xout, Tout contain outgoing messages without dividing by the cavity i.e.,
% q(.).
%
% TODO: use multicore package to generate in parallel

% Typically an array of DistNormal
assert(isa(X, 'Density'));
assert(isa(T, 'Density'));
assert(isa(X, 'Sampler'));
assert(isa(T, 'Sampler'));
assert(length(X) == length(T));
% xd = X(1).d;
% td = T(1).d;

op.seed = myProcessOptions(op, 'seed', 1);

% importance sampling data size. K in Nicolas's paper.
op.iw_samples = myProcessOptions(op, 'iw_samples', 1e5);

% Importance weight vector can be a numerically zero vector when, for
% example, the messages have very small variance. iw_trials specifies the
% number of times to draw IW samples to try before giving up on the
% messages.
op.iw_trials = myProcessOptions(op, 'iw_trials', 5);

% Instead of sampling from the in_proposal, if sample_cond_msg is true,
% then sample from mt (message from T) instead. T is the conditioned
% variable. If true, in_proposal is not needed and ignored.
op.sample_cond_msg = myProcessOptions(op, 'sample_cond_msg', false);

% true to use multicore package for parallel processing. 
op.use_multicore = myProcessOptions(op, 'use_multicore', false);

% The number of jobs to split into. Only used if use_multicore=true.
op.multicore_job_count = myProcessOptions(op, 'multicore_job_count', 4);

% multicore settings. A structure.
multicore_settings = myProcessOptions(op, 'multicore_settings', struct());

N = length(X);
if op.use_multicore
    perJob = floor(N/op.multicore_job_count);

    multicore_settings.multicoreDir= myProcessOptions(multicore_settings, ...
        'multicoreDir', '/nfs/nhome/live/wittawat/SHARE/gatsby/research/code/tmp');
    multicore_settings.maxEvalTimeSingle = max(80*perJob, 600) + 600;
    params = cell(1, op.multicore_job_count);
    ind = ceil(op.multicore_job_count*(1:N)/N);
    for i=1:length(params)
        I = ind==i;
        s = struct();
        s.X = X(I);
        s.T = T(I);
        s.op = op;
        params{i} = s;
    end
    resultCell = startmulticoremaster(@multicoreFunc, params, multicore_settings);
    % merge results 
    X2 = [];
    T2 = [];
    Xout2 = [];
    Tout2 = [];
    for j=1:length(resultCell)
        sj = resultCell{j};
        X2 = [X2, sj.X];
        T2 = [T2, sj.T];
        Xout2 = [Xout2, sj.Xout];
        Tout2 = [Tout2, sj.Tout];
    end
    X = X2;
    T = T2;
    Xout = Xout2;
    Tout = Tout2;
else
    % not using multicore
    %
    [X, T, Xout, Tout] = generateMsgs(X, T,  op);
end

end


function s2 = multicoreFunc(s)
    X = s.X;
    T = s.T;
    op = s.op;
    [X, T, Xout, Tout] = generateMsgs(X, T, op);
    s2.X = X;
    s2.T = T;
    s2.Xout = Xout;
    s2.Tout = Tout;
end

function [X, T, Xout, Tout] = generateMsgs(X, T, op)
    % The DistBuilder for x in p(x|t) i.e., the left variable. This DistBuilder will
    % determine the output distribution of x. If [], Xout will be [].
    % Output DistNormal by default.
    N=length(X);
    if isfield(op, 'left_distbuilder')
        if isempty(op.left_distbuilder)
            left_distbuilder = [];
        else
            left_distbuilder = op.left_distbuilder;
        end
    else
        left_distbuilder = DistNormal.getDistBuilder();
    end
    assert(isa(left_distbuilder, 'DistBuilder') || isempty(left_distbuilder));

    % The DistBuilder for t in p(x|t) i.e., the right variable. This DistBuilder will
    % determine the output distribution of t. If [], Tout will be [].
    % Output DistNormal by default.
    if isfield(op, 'right_distbuilder')
        if isempty(op.right_distbuilder)
            right_distbuilder = [];
        else
            right_distbuilder = op.right_distbuilder;
        end
    else
        right_distbuilder = DistNormal.getDistBuilder();
    end
    assert(isa(right_distbuilder, 'DistBuilder') || isempty(right_distbuilder));

    if ~op.sample_cond_msg
        % proposal distribution for for the conditional varibles (i.e. t)
        % in the factor. Require: Sampler & Density.
        in_proposal = op.in_proposal;
        assert(isa(in_proposal, 'Density'));
        assert(isa(in_proposal, 'Sampler'));
    end

    % A forward sampling function taking samples (array) from in_proposal and
    % outputting samples from the conditional distribution represented by the
    % factor.
    cond_factor = op.cond_factor;

    % change seed
    oldRng = rng();
    rng(op.seed);
    % number of importance-weighted samples.
    K = op.iw_samples;

    % outputs
    if ~isempty(left_distbuilder)
        Xout = left_distbuilder.empty(0, 1);
    end

    if ~isempty(right_distbuilder)
        Tout = right_distbuilder.empty(0, 1);
    end
    index = 1;
    % Indices of bad messages
    BadInd = [];
    for i=1:N
        mx = X(i);
        mt = T(i);
        for j=1:op.iw_trials
            if op.sample_cond_msg
                % sample from mt instead of in_proposal
                TP = mt.sampling0(K);
                XP = cond_factor(TP);
                % compute importance weights
                W = mx.density(XP);
            else
                TP = in_proposal.sampling0(K);
                XP = cond_factor(TP);
                % compute importance weights
                W = mx.density(XP).*mt.density(TP) ./ in_proposal.density(TP);
            end

            assert( all(W >= 0));
            wsum = sum(W);
            WN = W/wsum;
            % projection. p(x|t)
            if ~isempty(left_distbuilder)
                mx_out = left_distbuilder.fromSamples(XP, WN);
            end

            if ~isempty(right_distbuilder)
                mt_out = right_distbuilder.fromSamples(TP, WN);
            end

            if (isempty(left_distbuilder) || mx_out.isProper() )...
                    && (isempty(right_distbuilder) || mt_out.isProper() )

                if ~isempty(left_distbuilder)
                    Xout(index) = mx_out;
                end

                if ~isempty(right_distbuilder)
                    Tout(index) = mt_out;
                end
                index = index + 1;
                display(sprintf('generated %d', i));
                break;

            else
                if j==op.iw_trials
                    % not successful 
                    BadInd(end+1) = i;
                    % Assume mx and mt are somehow hard to deal with. Skip
                end
                % Try again
            end

        end

    end

    % exclude bad messages
    X(BadInd) = [];
    T(BadInd) = [];
    if isempty(left_distbuilder)
        Xout=[];
    else
        assert(length(Xout)==length(X));
    end

    if isempty(right_distbuilder)
        Tout = [];
    else
        assert(length(Tout)==length(T));
    end

    assert(length(X)==length(T));

    %%%%%%%%%%%%%%%%%%%%%%%%%%
    rng(oldRng);
end
