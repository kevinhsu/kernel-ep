function test_suite = test_DistNormal()
%
initTestSuite;

end

function test_distHellinger()

% test bound [0,1], symmetry
for i=1:20
    d1 = DistNormal(randn(1)*30, rand(1)*20);
    % self distance = 0
    assertElementsAlmostEqual(d1.distHellinger(d1), 0 );

    d2 = DistNormal(randn(1)*2, rand(1)*50 );
    dist1 = d1.distHellinger(d2);
    dist2 = d2.distHellinger(d1);
    % bound
    assertTrue(dist1>=0);
    assertTrue(dist1<=1);
    
    % symmetry
    assertElementsAlmostEqual(dist1, dist2);
end
end

function test_klDivergence()
    d=DistNormal(3, 4);
    assertElementsAlmostEqual(d.klDivergence(d), 0);
    d2=DistNormal(3, 5);
    assert(d.klDivergence(d2)>0);
    assert(d2.klDivergence(d)>0);


end


function test_parameters()
    m=3;
    v=4;
    d=DistNormal(m, v);
    C = d.parameters;
    assertElementsAlmostEqual(C{1}, m);
    assertElementsAlmostEqual(C{2}, v);
end
