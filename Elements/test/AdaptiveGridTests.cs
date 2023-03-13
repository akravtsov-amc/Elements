using Elements.Geometry;
using Elements.Serialization.glTF;
using Elements.Spatial.AdaptiveGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;
using System.Diagnostics;
using System.IO;

namespace Elements.Tests
{
    public class AdaptiveGridTests : ModelTest
    {
        [Fact]
        public void SegmentTreeTest()
        {
            var tree = new AdaptiveGrid.SegmentTree();
            ulong id;

            // Step 1
            tree.Insert(5.000000000000000, 5.000000000000000, 1.000000000000000, 1);
            Assert.False(tree.Find(1.094167920456591, 2.869292059020680, 2.832708877740317, 3.017035860444298, 3.871292023174524, 4.814723933331128, out id));
            // Step 2
            tree.Insert(0.000000000000000, 4.000000000000000, 1.000000000000000, 2);
            Assert.False(tree.Find(3.445189480963231, 3.948879104350081, 2.044064588475917, 3.466961830736343, 1.557146900675084, 4.892419366658347, out id));
            // Step 3
            tree.Insert(5.000000000000000, 3.000000000000000, 4.000000000000000, 3);
            Assert.False(tree.Find(1.000951936831107, 2.848410526137432, 3.155740721008367, 4.522616158750699, 2.624631696720226, 2.829755938584377, out id));
            // Step 4
            tree.Insert(0.000000000000000, 5.000000000000000, 4.000000000000000, 4);
            Assert.False(tree.Find(1.708316392616223, 3.884766534227434, 1.269905142213839, 2.055816827396243, 1.219713628239313, 1.718727627851993, out id));
            // Step 5
            tree.Erase(5.000000000000000, 5.000000000000000, 1.000000000000000, 1);
            Assert.False(tree.Find(1.039453897182327, 1.426508920997618, 0.682870914325650, 4.144674795167252, 1.491370643719181, 4.145004333860009, out id));
            // Step 6
            tree.Insert(1.000000000000000, 0.000000000000000, 0.000000000000000, 5);
            Assert.False(tree.Find(0.533342123110552, 2.399838902849811, 1.955072989390457, 3.401120910394476, 0.415272425485610, 4.951172817922872, out id));
            // Step 7
            tree.Erase(0.000000000000000, 4.000000000000000, 1.000000000000000, 2);
            Assert.False(tree.Find(0.683138799574318, 1.262891989053614, 0.907535415498315, 1.280221167612529, 0.582227768832428, 3.207089099307532, out id));
            // Step 8
            tree.Insert(3.000000000000000, 0.000000000000000, 4.000000000000000, 6);
            Assert.False(tree.Find(1.802008888847748, 4.454609165095983, 1.626535982537962, 3.953708368659798, 0.345647069880268, 4.373002188910248, out id));
            // Step 9
            tree.Erase(0.000000000000000, 5.000000000000000, 4.000000000000000, 4);
            Assert.False(tree.Find(3.796642814659770, 4.565181172560389, 2.720370824420187, 4.438610938840847, 3.846450470598389, 4.500469378106144, out id));
            // Step 10
            tree.Insert(1.000000000000000, 1.000000000000000, 3.000000000000000, 7);
            Assert.False(tree.Find(2.497327147485456, 3.585421427351091, 0.414692677832837, 4.980700712778996, 2.799330846588883, 4.484416662338279, out id));
            // Step 11
            tree.Erase(1.000000000000000, 1.000000000000000, 3.000000000000000, 7);
            Assert.False(tree.Find(2.208168634263227, 3.776297899394367, 0.635108340884212, 4.181790306948091, 4.808424840664691, 4.943099557178181, out id));
            // Step 12
            tree.Erase(5.000000000000000, 3.000000000000000, 4.000000000000000, 3);
            Assert.False(tree.Find(0.662560337642132, 3.506210172491406, 0.451797087260736, 1.165426598977133, 1.038188355084537, 4.965067628597301, out id));
            // Step 13
            tree.Insert(1.000000000000000, 2.000000000000000, 3.000000000000000, 8);
            Assert.False(tree.Find(1.559939592941995, 2.039617201950148, 1.458965897672908, 3.076923121113174, 1.037109539479201, 3.704836241639665, out id));
            // Step 14
            tree.Insert(0.000000000000000, 0.000000000000000, 1.000000000000000, 9);
            Assert.False(tree.Find(4.123233059135377, 4.841588498901643, 2.446691411226046, 3.060945857441641, 1.951592092577259, 4.529223903608780, out id));
            // Step 15
            tree.Insert(3.000000000000000, 4.000000000000000, 1.000000000000000, 10);
            Assert.False(tree.Find(1.736760625331281, 2.361264029154883, 3.129460583742816, 4.197525810107386, 3.253919222455733, 3.400974704954373, out id));
            // Step 16
            tree.Insert(2.000000000000000, 0.000000000000000, 0.000000000000000, 11);
            Assert.False(tree.Find(3.925395114044241, 4.393606282678462, 0.358618329642333, 1.008077650611030, 0.820078029316359, 3.317361040819918, out id));
            // Step 17
            tree.Insert(3.000000000000000, 2.000000000000000, 4.000000000000000, 12);
            Assert.False(tree.Find(3.629142563599795, 4.799404218809019, 3.753754945023909, 4.733166571997907, 2.030842109946760, 4.942511850986911, out id));
            // Step 18
            tree.Erase(1.000000000000000, 0.000000000000000, 0.000000000000000, 5);
            Assert.False(tree.Find(1.238330523046430, 4.226026323421854, 0.392471201084911, 1.022221230098088, 1.022679893321823, 4.664412242825714, out id));
            // Step 19
            tree.Insert(1.000000000000000, 0.000000000000000, 0.000000000000000, 13);
            Assert.True(tree.Find(2.542058852881135, 3.286822459350951, 3.321740088983599, 4.858433113674550, 0.409843340074934, 2.879946752358982, out id));
            Assert.True((new List<ulong> { 10 }).Contains(id));
            // Step 20
            tree.Erase(3.000000000000000, 2.000000000000000, 4.000000000000000, 12);
            Assert.False(tree.Find(3.448149652254288, 4.908021943634775, 0.494615804995584, 1.573177933113240, 3.058689565916558, 4.558210536426143, out id));
            // Step 21
            tree.Erase(2.000000000000000, 0.000000000000000, 0.000000000000000, 11);
            Assert.False(tree.Find(2.331686094983438, 3.849902339193930, 0.689912490528848, 2.715281043472180, 0.376400403562373, 1.633103239474645, out id));
            // Step 22
            tree.Insert(3.000000000000000, 1.000000000000000, 0.000000000000000, 14);
            Assert.False(tree.Find(0.915587110388012, 4.964257328015785, 0.603351199764145, 2.622201537767577, 3.692397461425882, 4.284923067389997, out id));
            // Step 23
            tree.Insert(5.000000000000000, 4.000000000000000, 5.000000000000000, 15);
            Assert.False(tree.Find(0.002919292007170, 2.042301908114172, 1.048305377523929, 4.940992914637190, 1.088984821919329, 1.191313988410812, out id));
            // Step 24
            tree.Erase(1.000000000000000, 0.000000000000000, 0.000000000000000, 13);
            Assert.False(tree.Find(1.737418475382695, 4.248055030759127, 1.586915727276216, 3.598255202403378, 3.697034709844267, 3.996408491473316, out id));
            // Step 25
            tree.Insert(4.000000000000000, 4.000000000000000, 0.000000000000000, 16);
            Assert.False(tree.Find(2.263058269665969, 2.284200761135886, 2.865206151463704, 4.812104272507021, 3.058864164547044, 4.877449514258699, out id));
            // Step 26
            tree.Insert(4.000000000000000, 3.000000000000000, 3.000000000000000, 17);
            Assert.False(tree.Find(0.125096926580208, 1.988337098983770, 0.237700578149476, 1.820398026417864, 0.852226658328007, 2.318682193414367, out id));
            // Step 27
            tree.Erase(5.000000000000000, 4.000000000000000, 5.000000000000000, 15);
            Assert.False(tree.Find(2.623880135388859, 2.855488372139273, 3.022123696513847, 4.548989786420293, 2.949379146284423, 4.234240292986122, out id));
            // Step 28
            tree.Insert(3.000000000000000, 4.000000000000000, 5.000000000000000, 18);
            Assert.False(tree.Find(0.377119697256246, 2.059794454304364, 3.766811605084603, 4.752194889861125, 1.167339028186038, 4.660727731013163, out id));
            // Step 29
            tree.Erase(4.000000000000000, 4.000000000000000, 0.000000000000000, 16);
            Assert.False(tree.Find(0.642633513960891, 2.940417959685637, 1.500600062309512, 4.037312991374190, 0.985552081547759, 2.274287546369920, out id));
            // Step 30
            tree.Insert(3.000000000000000, 0.000000000000000, 1.000000000000000, 19);
            Assert.False(tree.Find(1.289634604612733, 4.491937464783176, 1.411613483359737, 3.441654703312744, 2.340561330245956, 2.424615714305088, out id));
            // Step 31
            tree.Erase(0.000000000000000, 0.000000000000000, 1.000000000000000, 9);
            Assert.False(tree.Find(0.827577515377368, 2.125811294471021, 4.468143810016138, 4.526730611498897, 2.502029255134741, 4.373966349626350, out id));
            // Step 32
            tree.Insert(3.000000000000000, 2.000000000000000, 2.000000000000000, 20);
            Assert.False(tree.Find(2.379914091220531, 3.822705496916718, 1.633765436440814, 1.742410905477060, 0.698407710223944, 1.603642040884640, out id));
            // Step 33
            tree.Insert(0.000000000000000, 5.000000000000000, 3.000000000000000, 21);
            Assert.False(tree.Find(2.428740624125952, 2.434724480600814, 3.231199605092861, 3.763427174251355, 2.172399534095903, 4.844954146912557, out id));
            // Step 34
            tree.Insert(2.000000000000000, 3.000000000000000, 4.000000000000000, 22);
            Assert.True(tree.Find(0.890297546387571, 2.497437532857731, 0.365782084749299, 3.805402585063407, 0.377554744471250, 3.467640851619969, out id));
            Assert.True((new List<ulong> { 8 }).Contains(id));
            // Step 35
            tree.Insert(1.000000000000000, 4.000000000000000, 0.000000000000000, 23);
            Assert.False(tree.Find(1.692091201750299, 2.416141256078423, 0.140403740544404, 4.669584175425634, 0.760316975811015, 2.717471148930482, out id));
            // Step 36
            tree.Erase(3.000000000000000, 0.000000000000000, 4.000000000000000, 6);
            Assert.False(tree.Find(1.969457095731232, 4.115305308272924, 0.642501476380509, 1.687896918619678, 0.949282513147885, 4.384440826708149, out id));
            // Step 37
            tree.Insert(3.000000000000000, 1.000000000000000, 4.000000000000000, 24);
            Assert.False(tree.Find(0.318876872279208, 0.904985513576304, 0.941759414215714, 4.307097588532613, 0.527256865517241, 4.309874920395650, out id));
            // Step 38
            tree.Insert(3.000000000000000, 3.000000000000000, 3.000000000000000, 25);
            Assert.True(tree.Find(1.278153978614809, 4.819531834000403, 1.534687624752371, 2.534091070368649, 1.437791653670734, 3.429038662589565, out id));
            Assert.True((new List<ulong> { 20 }).Contains(id));
            // Step 39
            tree.Erase(3.000000000000000, 4.000000000000000, 5.000000000000000, 18);
            Assert.False(tree.Find(1.339336574161938, 2.135859569270603, 3.148481357629799, 3.451395424181180, 1.676986101526334, 3.358452351655774, out id));
            // Step 40
            tree.Erase(2.000000000000000, 3.000000000000000, 4.000000000000000, 22);
            Assert.False(tree.Find(0.008304323287762, 0.895018757887047, 1.232082153422988, 3.047881148369784, 1.869233554974342, 2.248676070906085, out id));
            // Step 41
            tree.Erase(1.000000000000000, 2.000000000000000, 3.000000000000000, 8);
            Assert.False(tree.Find(4.012058277438944, 4.065994458510238, 1.234734773727912, 2.266587103552390, 2.478882649172994, 3.678275388568878, out id));
            // Step 42
            tree.Insert(5.000000000000000, 2.000000000000000, 2.000000000000000, 26);
            Assert.False(tree.Find(2.486272757013880, 4.357029144288313, 1.180048150403668, 1.462102388833476, 0.862050714644619, 4.431012286509917, out id));
            // Step 43
            tree.Erase(3.000000000000000, 4.000000000000000, 1.000000000000000, 10);
            Assert.False(tree.Find(0.211394373908349, 2.770172220386456, 0.544593146049421, 3.555134868137352, 1.047134440070047, 2.199921425294947, out id));
            // Step 44
            tree.Insert(5.000000000000000, 2.000000000000000, 3.000000000000000, 27);
            Assert.False(tree.Find(2.115528776240875, 2.139323244079401, 0.709273173851634, 3.167029878745871, 1.674699204449129, 4.191940303107888, out id));
            // Step 45
            tree.Insert(5.000000000000000, 3.000000000000000, 1.000000000000000, 28);
            Assert.False(tree.Find(3.477939738381485, 3.490148804875380, 4.033268175435303, 4.112062181291717, 0.744041979001191, 4.597275232645401, out id));
            // Step 46
            tree.Erase(0.000000000000000, 5.000000000000000, 3.000000000000000, 21);
            Assert.False(tree.Find(0.846905027136947, 3.974413152014969, 2.205645065179488, 3.586309532829253, 3.387502778196797, 4.674531546583643, out id));
            // Step 47
            tree.Erase(4.000000000000000, 3.000000000000000, 3.000000000000000, 17);
            Assert.False(tree.Find(0.658896862651234, 2.340034445106801, 3.102845298730866, 3.255947229713863, 1.620797582067284, 4.110455322390190, out id));
            // Step 48
            tree.Erase(3.000000000000000, 1.000000000000000, 0.000000000000000, 14);
            Assert.False(tree.Find(2.002853829859031, 2.077919453992424, 1.660947954775672, 2.869813363303772, 4.446585874498902, 4.913610845345076, out id));
            // Step 49
            tree.Insert(1.000000000000000, 3.000000000000000, 4.000000000000000, 29);
            Assert.False(tree.Find(2.069307658832544, 3.145466281766713, 1.611955381256971, 3.769991254366696, 0.393421257479634, 1.704745583243357, out id));
            // Step 50
            tree.Erase(5.000000000000000, 2.000000000000000, 3.000000000000000, 27);
            Assert.True(tree.Find(0.339094775270162, 4.405447667295677, 1.882327350656126, 3.974387946799072, 0.395772632230975, 2.794711138974115, out id));
            Assert.True((new List<ulong> { 20 }).Contains(id));
        }

        [Fact, Trait("Category", "Examples")]
        public void AdaptiveGridPolygonKeyPointsExample()
        {
            // <example>

            var adaptiveGrid = new AdaptiveGrid();
            var points = new List<Vector3>()
            {
                new Vector3(-6, -4),
                new Vector3(-2, -4),
                new Vector3(3, -4),
                new Vector3(1, 4.5),
                new Vector3(6, 3),
            };
            adaptiveGrid.AddFromPolygon(Polygon.Rectangle(15, 10).TransformedPolygon(
                new Transform(new Vector3(), new Vector3(10, 0, 10))), points);

            // </example>

            WriteToModelWithRandomMaterials(adaptiveGrid, "Elements_Spatial_AdaptiveGrid_AdaptiveGrid");
        }

        [Fact]
        public void AdaptiveGridBboxKeyPointsExample()
        {
            // <example2>

            var adaptiveGrid = new AdaptiveGrid();
            var points = new List<Vector3>()
            {
                new Vector3(-6, -4),
                new Vector3(-2, -4),
                new Vector3(3, -4),
                new Vector3(1, 4.5, 3),
                new Vector3(6, 3, -2),
            };
            adaptiveGrid.AddFromBbox(new BBox3(new Vector3(-7.5, -5, -3), new Vector3(10, 10, 3)), points);

            points = new List<Vector3>()
            {
                new Vector3(-6, -4, 3),
                new Vector3(-2, 0, 3),
                new Vector3(0, 4, 3),
                new Vector3(2, 6, 3)
            };
            var rectangle = Polygon.Rectangle(new Vector3(-10, -5), new Vector3(15, 10));
            adaptiveGrid.AddFromPolygon(rectangle.TransformedPolygon(new Transform(new Vector3(0, 0, 3))), points);
            points = new List<Vector3>()
            {
                new Vector3(-6, -4, 2),
                new Vector3(-2, 0, 2),
                new Vector3(0, 4, 2),
                new Vector3(2, 6, 2)
            };
            adaptiveGrid.AddFromPolygon(rectangle.TransformedPolygon(new Transform(new Vector3(0, 0, 2))), points);

            // </example2>

            WriteToModelWithRandomMaterials(adaptiveGrid, "Elements_Spatial_AdaptiveGrid_AdaptiveGridBboxKeyPoints");
        }

        [Fact]
        public void AdaptiveGridAddVertex()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var points = new List<Vector3>()
            {
                new Vector3(-6, -4),
                new Vector3(-2, -4),
                new Vector3(3, -4),
                new Vector3(1, 4.5, 3),
                new Vector3(6, 3, -2),
            };
            adaptiveGrid.AddFromPolygon(Polygon.Rectangle(15, 10), points);

            ulong id;
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(-2, -4), out id));
            var oldV = adaptiveGrid.GetVertex(id);
            var edgesBefore = oldV.Edges.Count;

            var newV = adaptiveGrid.AddVertex(new Vector3(-2, -4, 2), new ConnectVertexStrategy(oldV));
            Assert.NotNull(newV);
            Assert.False(newV.Id == 0);
            Assert.Single(newV.Edges);
            Assert.True(newV.Edges.First().StartId == id || newV.Edges.First().EndId == id);
            Assert.Equal(edgesBefore + 1, oldV.Edges.Count());
            Assert.Contains(oldV.Edges, e => e.StartId == newV.Id || e.EndId == newV.Id);
        }

        [Fact]
        public void AdaptiveGridSubtractBoxCutEdges()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10));

            var points = new List<Vector3>();
            for (int i = 1; i < 10; i++)
            {
                points.Add(new Vector3(i, i, 1));
            }

            adaptiveGrid.AddFromExtrude(polygon, Vector3.ZAxis, 2, points);
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 5, 1), out _));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4.9, 1), out _));

            adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4, 1), out var borderId);
            var borderV = adaptiveGrid.GetVertex(borderId);
            var numEdges = borderV.Edges.Count;
            var numVertices = adaptiveGrid.GetVertices().Count;

            var o = Obstacle.FromBBox(
                new BBox3(new Vector3(4.9, 4.9, 0), new Vector3(5.1, 5.1, 2)));
            adaptiveGrid.SubtractObstacle(o);
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 5, 1), out _));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4.9, 1), out _));

            Assert.Equal(numEdges - 1, borderV.Edges.Count);
            //On each elevation one vertex is removed and no added
            Assert.Equal(numVertices - (3 * 1), adaptiveGrid.GetVertices().Count);
        }

        [Fact]
        public void AdaptiveGridSubtractBoxAddPerimeter()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10));

            var points = new List<Vector3>();
            for (int i = 1; i < 10; i++)
            {
                points.Add(new Vector3(i, i, 1));
            }

            adaptiveGrid.AddFromExtrude(polygon, Vector3.ZAxis, 2, points);
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 5, 1), out _));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4.9, 1), out _));

            adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4, 1), out var borderId);
            var borderV = adaptiveGrid.GetVertex(borderId);
            var numEdges = borderV.Edges.Count;
            var numVertices = adaptiveGrid.GetVertices().Count;

            var o = Obstacle.FromBBox(
                new BBox3(new Vector3(4.9, 4.9, 0), new Vector3(5.1, 5.1, 2)),
                addPerimeterEdges: true);
            adaptiveGrid.SubtractObstacle(o);
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 5, 1), out _));
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(5, 4.9, 1), out _));

            Assert.Equal(numEdges, borderV.Edges.Count);
            //There are 3 elevations: extrusion is done from 0 to 2 and split points are at  1.
            //On each elevation one vertex is removed and 8 added as box perimeter.
            //TODO: elevations are not connected!!!
            Assert.Equal(numVertices + (3 * 7), adaptiveGrid.GetVertices().Count);
        }

        [Fact]
        public void AdaptiveGridSubtractBoxSmallDifference()
        {
            var edgesNumber = 75;
            var adaptiveGrid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(new Vector3(-41, -51), new Vector3(-39, -49));

            var points = new List<Vector3>();
            points.Add(new Vector3(-40, -49.9, 1));
            points.Add(new Vector3(-40, -49.80979, 1));

            adaptiveGrid.AddFromExtrude(polygon, Vector3.ZAxis, 2, points);

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 0), out _));
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 1), out _));
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 2), out _));
            Assert.Equal(edgesNumber, adaptiveGrid.GetEdges().Count);

            var o = Obstacle.FromBBox(
                new BBox3(new Vector3(-40.2, -50.190211303259034, 0),
                          new Vector3(-39.8, -49.809788696740966, 2)));
            adaptiveGrid.SubtractObstacle(o);

            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 0), out _));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 1), out _));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(-40, -49.9, 2), out _));
            Assert.Equal(edgesNumber - 14, adaptiveGrid.GetEdges().Count);
        }

        [Fact]
        public void AdaptiveGridSubtractMisalignedPolygon()
        {
            var boundary = new Polygon(
                new Vector3(-15.0, 49.599999999999994, 0), //TODO: Root cause of an issue, coordinates of boundary vertices are slightly misaligned
                new Vector3(-45.0, 49.6, 0),
                new Vector3(-45.0, 0, 0),
                new Vector3(-15.0, 0, 0));

            var obstacles = new List<Obstacle>
            { 
                //Small box with x-axis aligned edges to subtract
                Obstacle.FromBBox(new BBox3(new Vector3(-30.41029, 19.60979, 0),
                                            new Vector3(-29.58971, 20.39021, 0))),
                //Big box intersecting one of the edges of boundary, it should remove edges and vertices 
                Obstacle.FromBBox(new BBox3(new Vector3(-22.08622, 17.62839, 0),
                                            new Vector3(-8.57565, 38.31022, 0))),
                //Small box with x-axis aligned edges to subtract and no vertices added to grid
                Obstacle.FromBBox(new BBox3(new Vector3(-30.1, 40.79, 0),
                                            new Vector3(-29.7, 41.39021, 0)))
            };

            var points = new List<Vector3>()
            {
                new Vector3(-29.8, 40.540211303259035, 0),
                new Vector3(-30.0, 49.599999999999994, 0),
                new Vector3(-29.8, 41.540211303259035, 0),
                
                //1st BBox vertices
                new Vector3(-30.41029, 19.60979, 0),
                new Vector3(-29.58971, 19.60979, 0),
                new Vector3(-29.58971, 20.39021, 0),
                new Vector3(-30.41029, 20.39021, 0),
                
                //2nd BBox vertices inside polygon
                new Vector3(-22.08622, 17.62839, 0),
                new Vector3(-22.08622, 38.31022, 0)
            };

            var adaptiveGrid = new AdaptiveGrid();
            adaptiveGrid.AddFromPolygon(boundary, points);

            var edgesCount = adaptiveGrid.GetEdges().Count();
            var verticiesCount = adaptiveGrid.GetVertices().Count();

            adaptiveGrid.SubtractObstacles(obstacles);

            Assert.Equal(edgesCount - 9, adaptiveGrid.GetEdges().Count);
            Assert.Equal(verticiesCount - 2, adaptiveGrid.GetVertices().Count);
        }

        [Fact]
        public void AdaptiveGridSubstructRotatedBox()
        {
            var polygon = Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10));
            var transfrom = new Transform().Rotated(Vector3.ZAxis, 45);

            var points = new List<Vector3>();
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    points.Add(new Vector3(i, j));
                }
            }

            var adaptiveGrid = new AdaptiveGrid(transfrom);
            adaptiveGrid.AddFromPolygon(polygon, points);

            //Obstacle aligned with adaptive grid transformation.
            //Forms big (3;1) -> (5;3) -> (3;5) -> (1;3) rectangle.
            var bbox = new BBox3(new Vector3(2, 2), new Vector3(4, 4));
            var withoutTransfrom = Obstacle.FromBBox(bbox, addPerimeterEdges: true);
            adaptiveGrid.SubtractObstacle(withoutTransfrom);

            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(3, 3), out _, adaptiveGrid.Tolerance));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(3, 2), out _, adaptiveGrid.Tolerance));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(3, 4), out _, adaptiveGrid.Tolerance));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(2, 3), out _, adaptiveGrid.Tolerance));
            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(4, 3), out _, adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(2, 2), out var id, adaptiveGrid.Tolerance));
            var v = adaptiveGrid.GetVertex(id);
            Assert.Equal(3, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(1.5, 2.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(2.5, 1.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(4, 2), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(3, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(3.5, 1.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(4.5, 2.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(4, 4), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(3, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(3.5, 4.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(4.5, 3.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(2, 4), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(3, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(1.5, 3.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(2.5, 4.5), adaptiveGrid.Tolerance));

            //Obstacle aligned with global transformation.
            //Forms small (6;6) -> (8;6) -> (8;8) -> (6;8) rectangle.
            bbox = new BBox3(new Vector3(6, 6), new Vector3(8, 8));
            var withTransform = Obstacle.FromBBox(bbox, addPerimeterEdges: true);
            withTransform.Orientation = new Transform();
            adaptiveGrid.SubtractObstacle(withTransform);

            Assert.False(adaptiveGrid.TryGetVertexIndex(new Vector3(7, 7), out _, adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(6, 6), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(5, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(6, 7), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(5.5, 6.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7, 6), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(6.5, 5.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(8, 6), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(5, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7, 6), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7.5, 5.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(8, 7), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(8.5, 6.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(8, 8), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(5, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(8, 7), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(8.5, 7.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7, 8), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7.5, 8.5), adaptiveGrid.Tolerance));

            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(6, 8), out id, adaptiveGrid.Tolerance));
            v = adaptiveGrid.GetVertex(id);
            Assert.Equal(5, v.Edges.Count);
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(7, 8), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(6.5, 8.5), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(6, 7), adaptiveGrid.Tolerance));
            Assert.Contains(v.Edges, e => adaptiveGrid.GetVertex(
                e.OtherVertexId(v.Id)).Point.IsAlmostEqualTo(new Vector3(5.5, 7.5), adaptiveGrid.Tolerance));

            WriteToModelWithRandomMaterials(adaptiveGrid);
        }

        [Fact]
        public void BrokenSubtractionForMisalignedPolygon()
        {
            var boundaryVerticies = new List<Vector3>
            {
                new Vector3(0.241, -40, 7),
                new Vector3(0.241, -60, 7),
                new Vector3(80, -60.000000000000014, 7),
                new Vector3(80, -40.000000000000014, 7)
            };

            var boundary = new Polygon(boundaryVerticies);

            var grid = new AdaptiveGrid()
            {
                Boundaries = boundary
            };

            grid.AddFromPolygon(boundary, new[] { Vector3.Origin });

            var profile = Polygon.Rectangle(0.2, 0.2);
            var column = new Column(
                new Vector3(0.5, -56.22727272727274),
                10,
                new Line(new Vector3(0.5, -56.22727272727274, 10), new Vector3(0.5, -56.22727272727274, 0)),
                profile);

            var obstacle = Obstacle.FromColumn(column, 0.2, true);
            var result = grid.SubtractObstacle(obstacle);

            Assert.True(result);
            Assert.Equal(8, grid.GetEdges().Count);
            Assert.All(grid.GetVertices(), x => Assert.Equal(2, x.Edges.Count));

            WriteToModelWithRandomMaterials(grid);
        }

        [Theory]
        [MemberData(nameof(GetObstaclesForAllowOutsideBoundaryTest))]
        public void AadaptiveGridSubtractObstacleAllowOutsideBoundaryTest(Obstacle obstacle, bool expectedResult, int additionalVertices, int additionalEdges)
        {
            var boundary = Polygon.Rectangle(20, 40);
            var grid = new AdaptiveGrid { Boundaries = boundary };
            grid.AddFromPolygon(boundary, new List<Vector3> { Vector3.Origin });
            grid.AddVertices(new List<Vector3> { new Vector3(0, 20), new Vector3(0, 50) }, AdaptiveGrid.VerticesInsertionMethod.ConnectAndCut);

            var edgesCount = grid.GetEdges().Count;
            var verticesCount = grid.GetVertices().Count;

            var result = grid.SubtractObstacle(obstacle);

            WriteToModelWithRandomMaterials(grid);

            Assert.Equal(expectedResult, result);
            Assert.Equal(verticesCount + additionalVertices, grid.GetVertices().Count);
            Assert.Equal(edgesCount + additionalEdges, grid.GetEdges().Count);
        }

        public static IEnumerable<object[]> GetObstaclesForAllowOutsideBoundaryTest()
        {
            var profile = Polygon.Rectangle(1, 1);

            //Column outside of boundary and does not intersect with any edge or vertex
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(-15, 0), 5, null, profile), 0, true), false, 0, 0 };
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(-15, 0), 5, null, profile), 0, true, true), false, 0, 0 };
            //Column intersects with boundary
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(-10, 0), 5, null, profile), 0, true), true, 4, 4 };
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(-10, 0), 5, null, profile), 0, true, true), true, 6, 7 };
            //Column fully inside in boundary
            yield return new object[] { Obstacle.FromColumn(new Column(Vector3.Origin, 5, null, profile), 0, true), true, 7, 8 };
            yield return new object[] { Obstacle.FromColumn(new Column(Vector3.Origin, 5, null, profile), 0, true, true), true, 7, 8 };
            //Column outside of boundary and intersects with grid edge
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(0, 30), 5, null, profile), 0, true), true, 2, 1, };
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(0, 30), 5, null, profile), 0, true, true), true, 6, 7 };
            //Column outside of boundary and intersects with grid vertex
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(0, 50), 5, null, profile), 0, true), true, 0, 0, };
            yield return new object[] { Obstacle.FromColumn(new Column(new Vector3(0, 50), 5, null, profile), 0, true, true), true, 4, 5 };
        }

        [Fact]
        public void AdaptiveGridLongSectionDoNowThrow()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(new Vector3(0, 0), new Vector3(200000, 10));

            var points = new List<Vector3>();
            points.Add(new Vector3(1, 5));
            points.Add(new Vector3(1999, 5));

            adaptiveGrid.AddFromExtrude(polygon, Vector3.ZAxis, 2, points);
        }

        [Fact]
        public void AdaptiveGridTwoAlignedSections()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var polygon1 = Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10));
            var polygon2 = Polygon.Rectangle(new Vector3(10, 2), new Vector3(20, 12));

            var points = new List<Vector3>();
            points.AddRange(polygon1.Vertices);
            points.AddRange(polygon2.Vertices);

            adaptiveGrid.AddFromExtrude(polygon1, Vector3.ZAxis, 2, points);
            adaptiveGrid.AddFromExtrude(polygon2, Vector3.ZAxis, 2, points);

            ulong id;
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(10, 2), out id));
            var vertex = adaptiveGrid.GetVertex(id);
            //Up, North, South, East, West
            Assert.Equal(5, vertex.Edges.Count);
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(10, 10), out id));
            vertex = adaptiveGrid.GetVertex(id);
            Assert.Equal(5, vertex.Edges.Count);
        }

        [Fact]
        public void AdaptiveGridDoesntAddTheSameVertex()
        {
            var adaptiveGrid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10));
            adaptiveGrid.AddFromPolygon(polygon, new List<Vector3>());
            Assert.True(adaptiveGrid.TryGetVertexIndex(new Vector3(0, 10), out var id));
            var vertex = adaptiveGrid.GetVertex(id);
            var halfTol = adaptiveGrid.Tolerance / 2;
            var modified = vertex.Point + new Vector3(0, 0, halfTol);
            adaptiveGrid.TryGetVertexIndex(new Vector3(10, 0), out var otherId);
            var newVertex = adaptiveGrid.AddVertex(modified,
                new ConnectVertexStrategy(adaptiveGrid.GetVertex(otherId)));
            Assert.Equal(id, newVertex.Id);
            modified = vertex.Point + new Vector3(-halfTol, -halfTol, -halfTol);
            adaptiveGrid.TryGetVertexIndex(modified, out otherId, adaptiveGrid.Tolerance);
            Assert.Equal(id, otherId);
        }

        [Fact]
        public void AdaptiveGridAddVertices()
        {
            var grid = new AdaptiveGrid();

            //Just add
            var simpleLine = new Vector3[] { new Vector3(10, 0), new Vector3(20, 0) };
            var added = grid.AddVertices(simpleLine, AdaptiveGrid.VerticesInsertionMethod.Insert);
            Assert.Equal(2, added.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(10, 0), out var id0));
            Assert.True(grid.TryGetVertexIndex(new Vector3(20, 0), out var id1));
            var v0 = grid.GetVertex(id0);
            var v1 = grid.GetVertex(id1);
            Assert.Empty(v0.Edges);
            Assert.Empty(v1.Edges);

            //Add and connect
            simpleLine = new Vector3[] { new Vector3(0, 0), new Vector3(2, 0), new Vector3(2, 0), new Vector3(5, 0) };
            added = grid.AddVertices(simpleLine, AdaptiveGrid.VerticesInsertionMethod.Connect);
            //Duplicates are ignored
            Assert.Equal(3, added.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(0, 0), out id0));
            Assert.True(grid.TryGetVertexIndex(new Vector3(2, 0), out id1));
            Assert.True(grid.TryGetVertexIndex(new Vector3(5, 0), out var id2));
            v0 = grid.GetVertex(id0);
            v1 = grid.GetVertex(id1);
            var v2 = grid.GetVertex(id2);
            Assert.Single(v0.Edges);
            Assert.Equal(2, v1.Edges.Count);
            Assert.Single(v2.Edges);
            Assert.Equal(v0.Edges.First().OtherVertexId(v0.Id), v1.Id);
            Assert.Equal(v2.Edges.First().OtherVertexId(v2.Id), v1.Id);

            //Add, connect and self intersect
            var singleIntersection = new Vector3[] {
                new Vector3(0, 5),
                new Vector3(5, 5),
                new Vector3(10, 5),
                new Vector3(10, 10),
                new Vector3(8, 10),
                new Vector3(8, 2)
            };
            added = grid.AddVertices(singleIntersection, AdaptiveGrid.VerticesInsertionMethod.ConnectAndSelfIntersect);
            Assert.Equal(8, added.Count); //Single intersection point represented twice.
            Assert.True(grid.TryGetVertexIndex(new Vector3(8, 5), out var id));
            var v = grid.GetVertex(id);
            Assert.Equal(4, v.Edges.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(5, 5), out id0));
            Assert.True(grid.TryGetVertexIndex(new Vector3(10, 5), out id1));
            Assert.True(grid.TryGetVertexIndex(new Vector3(8, 10), out id2));
            Assert.True(grid.TryGetVertexIndex(new Vector3(8, 2), out var id3));
            Assert.Contains(v.Edges, e => e.StartId == id0 || e.EndId == id0);
            Assert.Contains(v.Edges, e => e.StartId == id1 || e.EndId == id1);
            Assert.Contains(v.Edges, e => e.StartId == id2 || e.EndId == id2);
            Assert.Contains(v.Edges, e => e.StartId == id3 || e.EndId == id3);

            var douleIntersection = new Vector3[] {
                new Vector3(10, 0),
                new Vector3(20, 0),
                new Vector3(20, 5),
                new Vector3(15, 5),
                new Vector3(15, -5),
                new Vector3(12, -5),
                new Vector3(12, 5),
            };
            added = grid.AddVertices(douleIntersection, AdaptiveGrid.VerticesInsertionMethod.ConnectAndSelfIntersect);
            Assert.Equal(11, added.Count); //Two intersection points represented twice.
            Assert.True(grid.TryGetVertexIndex(new Vector3(15, 0), out id0));
            Assert.True(grid.TryGetVertexIndex(new Vector3(12, 0), out id1));
            v0 = grid.GetVertex(id0);
            v1 = grid.GetVertex(id1);
            Assert.Equal(4, v0.Edges.Count);
            Assert.Equal(4, v1.Edges.Count);
            Assert.Contains(v0.Edges, e => e.StartId == id1 || e.EndId == id1);
            Assert.True(grid.TryGetVertexIndex(new Vector3(10, 0), out id2));
            Assert.True(grid.TryGetVertexIndex(new Vector3(20, 0), out id3));
            v2 = grid.GetVertex(id2);
            var v3 = grid.GetVertex(id3);
            Assert.Single(v2.Edges);
            Assert.Equal(2, v3.Edges.Count);
            Assert.Contains(v2.Edges, e => e.StartId == id1 || e.EndId == id1);
            Assert.Contains(v3.Edges, e => e.StartId == id0 || e.EndId == id0);

            //Add connect and cut
            simpleLine = new Vector3[] { new Vector3(2, 10), new Vector3(2, 0) };
            added = grid.AddVertices(simpleLine, AdaptiveGrid.VerticesInsertionMethod.ConnectAndCut);
            Assert.Equal(3, added.Count);
            Assert.Contains(added, v => v.Point.IsAlmostEqualTo(new Vector3(2, 10)));
            Assert.Contains(added, v => v.Point.IsAlmostEqualTo(new Vector3(2, 5)));
            Assert.Contains(added, v => v.Point.IsAlmostEqualTo(new Vector3(2, 0)));
            Assert.True(grid.TryGetVertexIndex(new Vector3(2, 5), out id0));
            Assert.True(grid.TryGetVertexIndex(new Vector3(2, 0), out id1));
            v0 = grid.GetVertex(id0);
            v1 = grid.GetVertex(id1);
            Assert.Equal(4, v0.Edges.Count);
            Assert.Equal(3, v1.Edges.Count);

            //Add cut and extend.
            grid = new AdaptiveGrid();
            grid.AddFromPolygon(Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10)),
                                new List<Vector3> { new Vector3(5, 5) });

            var toExtend = new Vector3[] { new Vector3(1, 5), new Vector3(4, 2), new Vector3(8, 6) };
            added = grid.AddVertices(toExtend, AdaptiveGrid.VerticesInsertionMethod.ConnectCutAndExtend);
            Assert.Equal(8, added.Count);
            Assert.Equal(new Vector3(0, 6), added[0].Point);
            Assert.Equal(new Vector3(1, 5), added[1].Point);
            Assert.Equal(new Vector3(5, 1), added[2].Point);
            Assert.Equal(new Vector3(2, 0), added[3].Point);
            Assert.Equal(new Vector3(4, 2), added[4].Point);
            Assert.Equal(new Vector3(5, 3), added[5].Point);
            Assert.Equal(new Vector3(7, 5), added[6].Point);
            Assert.Equal(new Vector3(10, 8), added[7].Point);
            Assert.Equal(3, added[0].Edges.Count);
            Assert.Equal(4, added[1].Edges.Count);
            Assert.Equal(3, added[2].Edges.Count);
            Assert.Equal(3, added[3].Edges.Count);
            Assert.Equal(4, added[4].Edges.Count);
            Assert.Equal(4, added[5].Edges.Count);
            Assert.Equal(4, added[6].Edges.Count);
            Assert.Equal(3, added[7].Edges.Count);
        }

        [Fact]
        public void AddVerticesWithCustomExtension()
        {
            var grid = new AdaptiveGrid();
            grid.AddFromPolygon(Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10)),
                                new List<Vector3> { });

            //Default HintExtendDistance is 3.
            var toExtend = new Vector3[] { new Vector3(1, 3), new Vector3(1, 6) };
            var added = grid.AddVerticesWithCustomExtension(toExtend, grid.HintExtendDistance);
            Assert.Equal(2, added.Count);
            Assert.Equal(new Vector3(1, 0), added[0].Point);
            Assert.Equal(new Vector3(1, 6), added[1].Point);
            Assert.Equal(3, added[0].Edges.Count);
            Assert.Single(added[1].Edges);

            toExtend = new Vector3[] { new Vector3(5, 3), new Vector3(5, 6) };
            added = grid.AddVerticesWithCustomExtension(toExtend, 4);
            Assert.Equal(2, added.Count);
            Assert.Equal(new Vector3(5, 0), added[0].Point);
            Assert.Equal(new Vector3(5, 10), added[1].Point);
            Assert.Equal(3, added[0].Edges.Count);
            Assert.Equal(3, added[1].Edges.Count);

            toExtend = new Vector3[] { new Vector3(8, 3), new Vector3(8, 6) };
            added = grid.AddVerticesWithCustomExtension(toExtend, 2);
            Assert.Equal(2, added.Count);
            Assert.Equal(new Vector3(8, 3), added[0].Point);
            Assert.Equal(new Vector3(8, 6), added[1].Point);
            Assert.Single(added[0].Edges);
            Assert.Single(added[1].Edges);
        }

        [Fact]
        public void AddAngledVerticesWithCustomExtension()
        {
            var grid = new AdaptiveGrid();
            grid.AddFromPolygon(Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10)),
                                new List<Vector3> { });

            var toExtend = new Vector3[] { new Vector3(1, 7), new Vector3(2, 8) };
            var added = grid.AddVerticesWithCustomExtension(toExtend, 2);
            Assert.Equal(2, added.Count);
            Assert.Equal(new Vector3(0, 6), added[0].Point);
            Assert.Equal(new Vector3(2, 8), added[1].Point);
            Assert.Equal(3, added[0].Edges.Count);
            Assert.Single(added[1].Edges);
        }

        [Fact]
        public void Add3DVerticesWithCustomExtension()
        {
            var grid = new AdaptiveGrid();
            grid.AddFromPolygon(Polygon.Rectangle(new Vector3(0, 0), new Vector3(10, 10)),
                                new List<Vector3> { new Vector3(2, 2), new Vector3(5, 5), new Vector3(8, 8) });

            var toInsert = new Vector3[] {
                new Vector3(2, 5, 0),
                new Vector3(3, 5, 0),
                new Vector3(4, 5, 1),
                new Vector3(5, 5, 1),
                new Vector3(6, 5, 0),
                new Vector3(7, 5, 0)
            };

            var verticesBefore = grid.GetVertices().Count;
            grid.AddVerticesWithCustomExtension(toInsert, 2);
            //Start point already exist and the last one is snapped.
            Assert.Equal(verticesBefore + 4, grid.GetVertices().Count);

            Assert.True(grid.TryGetVertexIndex(new Vector3(3, 5, 0), out var id, grid.Tolerance));
            var vertex = grid.GetVertex(id);
            Assert.Equal(3, vertex.Edges.Count);
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(4, 5, 1)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(2, 5, 0)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(5, 5, 0)));

            Assert.True(grid.TryGetVertexIndex(new Vector3(4, 5, 1), out id, grid.Tolerance));
            vertex = grid.GetVertex(id);
            Assert.Equal(2, vertex.Edges.Count);
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(3, 5, 0)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(5, 5, 1)));

            Assert.True(grid.TryGetVertexIndex(new Vector3(5, 5, 1), out id, grid.Tolerance));
            vertex = grid.GetVertex(id);
            Assert.Equal(2, vertex.Edges.Count);
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(4, 5, 1)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(6, 5, 0)));

            Assert.True(grid.TryGetVertexIndex(new Vector3(6, 5, 0), out id, grid.Tolerance));
            vertex = grid.GetVertex(id);
            Assert.Equal(3, vertex.Edges.Count);
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(5, 5, 1)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(5, 5, 0)));
            Assert.Contains(vertex.Edges, e => grid.GetVertex(e.OtherVertexId(id)).Point.IsAlmostEqualTo(new Vector3(8, 5, 0)));
        }

        [Fact]
        public void AdaptiveGridVertexGetEdgeOtherVertexId()
        {
            var grid = SampleGrid();
            var vertex = grid.GetVertex(2);
            Assert.Null(vertex.GetEdge(4));
            Assert.Null(vertex.GetEdge(2));

            var edge = vertex.GetEdge(1);
            Assert.True(edge.OtherVertexId(2) == 1);
            Assert.Throws<ArgumentException>(() => edge.OtherVertexId(3));
            var startVertex = grid.GetVertex(edge.StartId);
            Assert.True(startVertex.Point.IsAlmostEqualTo(new Vector3(0, 0)));
        }

        [Fact]
        public void AdaptiveGridClosestVertex()
        {
            var grid = SampleGrid();
            var closest = grid.ClosestVertex(new Vector3(5, 4));
            Assert.Equal(4u, closest.Id);
        }

        [Fact]
        public void AdaptiveGridClosestEdge()
        {
            var grid = SampleGrid();
            var edge = grid.ClosestEdge(new Vector3(9, 3), out var closest);
            Assert.True(edge.StartId == 3 || edge.StartId == 4);
            Assert.True(edge.EndId == 3 || edge.EndId == 4);
            Assert.Equal(new Vector3(8, 2), closest);
        }

        [Fact]
        public void AdaptiveGridCutEdge()
        {
            var grid = SampleGrid();
            var vertex = grid.GetVertex(1);
            var edge = vertex.GetEdge(4);
            var cut = grid.CutEdge(edge, new Vector3(0, 5));
            Assert.DoesNotContain(edge, vertex.Edges);
            Assert.DoesNotContain(edge, grid.GetEdges());
            Assert.Equal(2, cut.Edges.Count);
            Assert.Contains(cut.Edges, e => e.OtherVertexId(cut.Id) == 1);
            Assert.Contains(cut.Edges, e => e.OtherVertexId(cut.Id) == 4);
        }

        [Fact]
        public void AdaptiveGridEdgeGetVerticesGetLine()
        {
            var grid = SampleGrid();
            var vertexA = grid.GetVertex(1);
            var vertexB = grid.GetVertex(4);
            var edge = vertexA.GetEdge(4);
            var vertices = grid.GetVertices(edge);
            Assert.Equal(2, vertices.Count);
            Assert.Contains(vertices, v => v == vertexA);
            Assert.Contains(vertices, v => v == vertexB);

            var line = grid.GetLine(edge);
            Assert.True(line.Start.IsAlmostEqualTo(vertexA.Point) || line.End.IsAlmostEqualTo(vertexA.Point));
            Assert.True(line.Start.IsAlmostEqualTo(vertexB.Point) || line.End.IsAlmostEqualTo(vertexB.Point));
        }

        [Fact]
        public void AdaptiveGridRemoveVertex()
        {
            var grid = SampleGrid();
            var oldVertexCount = grid.GetVertices().Count;
            var oldEdgeCount = grid.GetEdges().Count;
            var vertex = grid.GetVertex(1);
            var edges = vertex.Edges.ToList();
            var otherVertices = edges.Select(e => grid.GetVertex(e.OtherVertexId(1)));
            grid.RemoveVertex(vertex);
            Assert.DoesNotContain(vertex, grid.GetVertices());
            Assert.Equal(oldVertexCount - 1, grid.GetVertices().Count);
            Assert.Equal(oldEdgeCount - 2, grid.GetEdges().Count);
            foreach (var e in edges)
            {
                Assert.DoesNotContain(e, grid.GetEdges());
                Assert.DoesNotContain(otherVertices, v => v.Edges.Contains(e));
            }
        }

        [Fact]
        public void AdaptiveGridAddEdgeNoCut()
        {
            var grid = SampleGrid();
            var v0 = grid.GetVertex(4);
            var v0ec = v0.Edges.Count;
            var v1 = grid.GetVertex(5);
            var v1ec = v1.Edges.Count;
            var oldVertexCount = grid.GetVertices().Count;
            var oldEdgeCount = grid.GetEdges().Count;
            var newEdges = grid.AddEdge(v0, v1, cut: false);
            Assert.Equal(oldVertexCount, grid.GetVertices().Count);
            Assert.Equal(oldEdgeCount + 1, grid.GetEdges().Count);
            Assert.Equal(v0ec + 1, v0.Edges.Count);
            Assert.Equal(v1ec + 1, v1.Edges.Count);
            Assert.Contains(newEdges.First(), v0.Edges);
            Assert.Contains(newEdges.First(), v1.Edges);
            Assert.True(newEdges.First().StartId == v0.Id);
            Assert.True(newEdges.First().EndId == v1.Id);

            oldEdgeCount = grid.GetEdges().Count;
            var otherEdge = newEdges.First();
            newEdges = grid.AddEdge(new Vector3(4, 3), new Vector3(6, 3), cut: false);
            Assert.Equal(oldVertexCount + 2, grid.GetVertices().Count);
            Assert.Equal(oldEdgeCount + 1, grid.GetEdges().Count);
            v0 = grid.GetVertex(newEdges.First().StartId);
            v1 = grid.GetVertex(newEdges.First().EndId);
            Assert.Equal(new Vector3(4, 3), v0.Point);
            Assert.Equal(new Vector3(6, 3), v1.Point);
            Assert.True(grid.GetLine(otherEdge).Intersects(grid.GetLine(newEdges.First()), out _));
        }

        [Fact]
        public void AdaptiveGridRemoveEdge()
        {
            var grid = SampleGrid();
            var v0 = grid.GetVertex(2);
            var v1 = grid.GetVertex(5);
            var v0ec = v0.Edges.Count;
            var oldVertexCount = grid.GetVertices().Count;
            var oldEdgeCount = grid.GetEdges().Count;
            var edge = v0.GetEdge(v1.Id);
            grid.RemoveEdge(edge);
            Assert.Equal(oldVertexCount - 1, grid.GetVertices().Count);
            Assert.Equal(oldEdgeCount - 1, grid.GetEdges().Count);

            Assert.DoesNotContain(edge, grid.GetEdges());
            Assert.DoesNotContain(v1, grid.GetVertices()); //v1 had only one edge.
            Assert.Contains(v0, grid.GetVertices()); //v0 had two edges
            Assert.Equal(v0ec - 1, v0.Edges.Count);
            Assert.DoesNotContain(edge, v0.Edges);
        }

        [Fact]
        public void AdaptiveGridAddCutEdge()
        {
            var grid = SampleGrid();

            //1. Just intersection.
            var v0 = grid.AddVertex(new Vector3(4, 1));
            var v1 = grid.AddVertex(new Vector3(6, 1));
            var edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Equal(2, edges.Count);
            var intersectionVertex = grid.GetVertex(edges.First().EndId);
            Assert.Equal(new Vector3(5, 1), intersectionVertex.Point);
            Assert.Equal(4, intersectionVertex.Edges.Count);

            //2. Multiply intersections
            v0 = grid.AddVertex(new Vector3(0, 4));
            v1 = grid.AddVertex(new Vector3(10, 4));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Equal(3, edges.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(4, 4), out var otherId));
            Assert.True(edges.First().StartId == otherId || edges.First().EndId == otherId);
            intersectionVertex = grid.GetVertex(otherId);
            Assert.Equal(4, intersectionVertex.Edges.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(6, 4), out otherId));
            Assert.True(edges.Last().StartId == otherId || edges.Last().EndId == otherId);
            intersectionVertex = grid.GetVertex(otherId);
            Assert.Equal(4, intersectionVertex.Edges.Count);

            //3. Miss
            v0 = grid.AddVertex(new Vector3(0, 7));
            v1 = grid.AddVertex(new Vector3(10, 7));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Single(edges);
            var startVertex = grid.GetVertex(edges.First().StartId);
            var endVertex = grid.GetVertex(edges.First().EndId);
            Assert.Equal(new Vector3(0, 7), startVertex.Point);
            Assert.Equal(new Vector3(10, 7), endVertex.Point);
            Assert.Single(startVertex.Edges);
            Assert.Single(endVertex.Edges);

            //4. In Plane Touch
            v0 = grid.AddVertex(new Vector3(2, 0));
            v1 = grid.AddVertex(new Vector3(2, -5));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Single(edges);
            startVertex = grid.GetVertex(edges.First().StartId);
            endVertex = grid.GetVertex(edges.First().EndId);
            Assert.Equal(new Vector3(2, 0), startVertex.Point);
            Assert.Equal(3, startVertex.Edges.Count);
            Assert.Single(endVertex.Edges);

            //5. Out Plane Touch
            v0 = grid.AddVertex(new Vector3(8, 0));
            v1 = grid.AddVertex(new Vector3(8, 0, 2));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Single(edges);
            startVertex = grid.GetVertex(edges.First().StartId);
            Assert.Equal(new Vector3(8, 0), startVertex.Point);
            Assert.Equal(3, startVertex.Edges.Count);
            Assert.Single(endVertex.Edges);

            //6. Inside other edge
            v0 = grid.AddVertex(new Vector3(1, 1));
            v1 = grid.AddVertex(new Vector3(3, 3));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Single(edges);
            startVertex = grid.GetVertex(edges.First().StartId);
            endVertex = grid.GetVertex(edges.First().EndId);
            Assert.Equal(2, startVertex.Edges.Count);
            Assert.Equal(2, endVertex.Edges.Count);
            var otherEdge = startVertex.Edges.First(e => e.StartId != endVertex.Id && e.EndId != endVertex.Id);
            var otherVertex = grid.GetVertex(otherEdge.StartId == startVertex.Id ? otherEdge.EndId : otherEdge.StartId);
            Assert.Equal(new Vector3(0, 0), otherVertex.Point);
            otherEdge = endVertex.Edges.First(e => e.StartId != startVertex.Id && e.EndId != startVertex.Id);
            otherVertex = grid.GetVertex(otherEdge.StartId == startVertex.Id ? otherEdge.EndId : otherEdge.StartId);
            Assert.Equal(new Vector3(4, 4), otherVertex.Point);

            //7. Overlaps other edges
            v0 = grid.AddVertex(new Vector3(4, 6));
            v1 = grid.AddVertex(new Vector3(11, -1));
            edges = grid.AddEdge(v0.Id, v1.Id);
            Assert.Equal(4, edges.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(5, 5), out otherId));
            Assert.True(edges.First().StartId == otherId || edges.First().EndId == otherId);
            endVertex = grid.GetVertex(otherId);
            startVertex = grid.GetVertex(edges.First().StartId == otherId ? edges.First().EndId : edges.First().StartId);
            Assert.Single(startVertex.Edges);
            Assert.Equal(3, endVertex.Edges.Count);
            Assert.True(grid.TryGetVertexIndex(new Vector3(6, 4), out otherId));
            Assert.Contains(endVertex.Edges, e => e.StartId == otherId || e.EndId == otherId);
            otherVertex = grid.GetVertex(otherId);
            Assert.Equal(4, otherVertex.Edges.Count);
            startVertex = grid.GetVertex(edges.Last().StartId);
            endVertex = grid.GetVertex(edges.Last().EndId);
            Assert.Equal(3, startVertex.Edges.Count);
            Assert.Single(endVertex.Edges);
            Assert.Contains(startVertex.Edges, e => e.StartId == otherId || e.EndId == otherId);
            otherVertex = grid.GetVertex(otherId);
            Assert.Equal(new Vector3(6, 4), otherVertex.Point);
        }

        [Fact]
        public void AdaptiveGridAddVertexWithAngle()
        {
            var grid = new AdaptiveGrid();

            //1. Aligned with direction. 0 vertices exist.
            var s = new ConnectVertexWithAngleStrategy(new Vector3(0, 5), new Vector3(0, 1), 45);
            var startVertex = grid.AddVertex(new Vector3(0, 0), s);
            Assert.Null(s.MiddleVertex);
            Assert.Equal(new Vector3(0, 0), startVertex.Point);
            var id00 = startVertex.Id;
            Assert.Equal(new Vector3(0, 5), s.EndVertex.Point);
            var id05 = s.EndVertex.Id;

            //2. Ortho aligned with direction, 1 vertex exist.
            s = new ConnectVertexWithAngleStrategy(new Vector3(0, 0), new Vector3(0, 1), 45);
            startVertex = grid.AddVertex(new Vector3(5, 0), s);
            Assert.Null(s.MiddleVertex);
            Assert.Equal(new Vector3(5, 0), startVertex.Point);
            Assert.Equal(id00, s.EndVertex.Id);
            var id50 = startVertex.Id;

            //3. 0 degree, 2 vertices exist.
            s = new ConnectVertexWithAngleStrategy(new Vector3(5, 0), new Vector3(0, 1), 0);
            startVertex = grid.AddVertex(new Vector3(0, 5), s);
            Assert.Equal(new Vector3(5, 5), s.MiddleVertex.Point);
            Assert.Equal(new Vector3(0, 5), startVertex.Point);
            Assert.Equal(id05, startVertex.Id);
            Assert.Equal(id50, s.EndVertex.Id);

            //4. 90 degrees, 0 vertices exist.
            s = new ConnectVertexWithAngleStrategy(new Vector3(10, 0), new Vector3(0, 1), 90);
            startVertex = grid.AddVertex(new Vector3(15, 5), s);
            Assert.Equal(new Vector3(15, 0), s.MiddleVertex.Point);

            //5. 45 degrees, 1 intersection.
            s = new ConnectVertexWithAngleStrategy(new Vector3(13, -2), new Vector3(1, 0), 45);
            startVertex = grid.AddVertex(new Vector3(10, 5), s);
            Assert.Equal(new Vector3(10, 1), s.MiddleVertex.Point);
            Assert.Equal(45.0, (s.EndVertex.Point - s.MiddleVertex.Point).AngleTo(s.MiddleVertex.Point - startVertex.Point), 3);
            Assert.Contains(s.MiddleVertex.Edges, e =>
                e.StartId != s.MiddleVertex.Id && grid.GetVertex(e.StartId).Point.IsAlmostEqualTo(new Vector3(11, 0)) ||
                e.EndId != s.MiddleVertex.Id && grid.GetVertex(e.EndId).Point.IsAlmostEqualTo(new Vector3(11, 0)));

            //5. 45 degrees, tilted direction.
            s = new ConnectVertexWithAngleStrategy(new Vector3(11, 15), new Vector3(1, 1), 45);
            startVertex = grid.AddVertex(new Vector3(10, 10), s);
            Assert.Equal(new Vector3(11, 11), s.MiddleVertex.Point);
            Assert.Equal(45.0, (s.EndVertex.Point - s.MiddleVertex.Point).AngleTo(s.MiddleVertex.Point - startVertex.Point), 3);

            //6. 1 to 2 ration (26.565 degrees)
            s = new ConnectVertexWithAngleStrategy(new Vector3(15, 5), new Vector3(0, 1), 26.565);
            startVertex = grid.AddVertex(new Vector3(20, 0), s);
            Assert.Equal(new Vector3(17.5, 0), s.MiddleVertex.Point);
            var angle = (s.EndVertex.Point - s.MiddleVertex.Point).AngleTo(s.MiddleVertex.Point - startVertex.Point);
            Assert.True(angle.ApproximatelyEquals(26.565) || angle.ApproximatelyEquals(90 - 26.565));

            //7.  1 to 2 ration (26.565 degrees) full length
            s = new ConnectVertexWithAngleStrategy(new Vector3(20, 0), new Vector3(1, 0), 26.565);
            startVertex = grid.AddVertex(new Vector3(30, 5), s);
            Assert.Null(s.MiddleVertex);
            Assert.Contains(startVertex.Edges, e => e.StartId == s.EndVertex.Id || e.EndId == s.EndVertex.Id);
        }

        [Fact]
        public void AdaptiveGridStoreAndDuplicateElevation()
        {
            AdaptiveGrid grid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(Vector3.Origin, new Vector3(10, 10));
            grid.AddFromExtrude(polygon, Vector3.ZAxis, 1, new List<Vector3>() { new Vector3(5, 5) });
            grid.AddEdge(new Vector3(0, 5, 1), new Vector3(0, 5, 2), false);
            grid.AddEdge(new Vector3(10, 5, 1), new Vector3(10, 5, 4), false);

            var plane = new Plane(new Vector3(0, 0, 1), Vector3.ZAxis);
            var snapshot = grid.SnapshotEdgesOnPlane(plane);
            Assert.Equal(12, snapshot.Count);

            grid.TryGetVertexIndex(new Vector3(5, 0, 1), out var id, grid.Tolerance);
            grid.RemoveVertex(grid.GetVertex(id));
            var edgesBefore = grid.GetEdges().Count;

            var transform = new Transform(0, 0, 2);
            grid.InsertSnapshot(snapshot, transform);
            Assert.Equal(edgesBefore + 20, grid.GetEdges().Count);

            Assert.True(grid.TryGetVertexIndex(new Vector3(0, 5, 3), out id, grid.Tolerance));
            var v = grid.GetVertex(id);
            Assert.Equal(4, v.Edges.Count);
            var neighbourPoints = v.Edges.Select(e => grid.GetVertex(e.OtherVertexId(v.Id)).Point);
            Assert.Contains(new Vector3(0, 0, 3), neighbourPoints);
            Assert.Contains(new Vector3(0, 10, 3), neighbourPoints);
            Assert.Contains(new Vector3(5, 5, 3), neighbourPoints);
            Assert.Contains(new Vector3(0, 5, 2), neighbourPoints);
            Assert.DoesNotContain(new Vector3(0, 5, 1), neighbourPoints);

            Assert.True(grid.TryGetVertexIndex(new Vector3(5, 0, 3), out id, grid.Tolerance));
            v = grid.GetVertex(id);
            Assert.Equal(3, v.Edges.Count);
            neighbourPoints = v.Edges.Select(e => grid.GetVertex(e.OtherVertexId(v.Id)).Point);
            Assert.Contains(new Vector3(0, 0, 3), neighbourPoints);
            Assert.Contains(new Vector3(10, 0, 3), neighbourPoints);
            Assert.Contains(new Vector3(5, 5, 3), neighbourPoints);
            Assert.DoesNotContain(new Vector3(5, 0, 1), neighbourPoints);

            Assert.True(grid.TryGetVertexIndex(new Vector3(10, 5, 3), out id, grid.Tolerance));
            v = grid.GetVertex(id);
            Assert.Equal(5, v.Edges.Count);
            neighbourPoints = v.Edges.Select(e => grid.GetVertex(e.OtherVertexId(v.Id)).Point);
            Assert.Contains(new Vector3(10, 0, 3), neighbourPoints);
            Assert.Contains(new Vector3(10, 10, 3), neighbourPoints);
            Assert.Contains(new Vector3(5, 5, 3), neighbourPoints);
            Assert.Contains(new Vector3(10, 5, 1), neighbourPoints);
            Assert.Contains(new Vector3(10, 5, 4), neighbourPoints);
        }

        [Fact]
        public void EdgeInfoFlagsTest()
        {
            AdaptiveGrid grid = new AdaptiveGrid();
            var polygon = Polygon.Rectangle(Vector3.Origin, new Vector3(10, 10));
            grid.AddFromPolygon(polygon, new List<Vector3>() { new Vector3(5, 5) });
            grid.AddEdge(Vector3.Origin, new Vector3(0, 0, 5));

            grid.TryGetVertexIndex(Vector3.Origin, out var id0);
            grid.TryGetVertexIndex(new Vector3(0, 0, 5), out var id1);
            grid.TryGetVertexIndex(new Vector3(0, 5), out var id2);

            var verticalEdge = grid.GetVertex(id0).Edges.First(e => e.StartId == id1 || e.EndId == id1);
            var horizontalEdge = grid.GetVertex(id0).Edges.First(e => e.StartId == id2 || e.EndId == id2);
            EdgeInfo verticalEdgeInfo = new EdgeInfo(grid, verticalEdge);
            EdgeInfo horizontalEdgeInfo = new EdgeInfo(grid, horizontalEdge);
            Assert.True(verticalEdgeInfo.HasAnyFlag(EdgeFlags.HasVerticalChange));
            Assert.False(horizontalEdgeInfo.HasAnyFlag(EdgeFlags.HasVerticalChange));

            horizontalEdgeInfo.AddFlags(EdgeFlags.UserDefinedHint2D | EdgeFlags.HasVerticalChange);
            Assert.True(horizontalEdgeInfo.HasAnyFlag(EdgeFlags.HasVerticalChange));
            Assert.True(horizontalEdgeInfo.HasAnyFlag(EdgeFlags.UserDefinedHint2D));
            Assert.False(horizontalEdgeInfo.HasAnyFlag(EdgeFlags.UserDefinedHint3D));
            Assert.True(horizontalEdgeInfo.HasAnyFlag(EdgeFlags.UserDefinedHint2D | EdgeFlags.UserDefinedHint3D));
        }

        //          (4)
        //         /   \
        //        /     \
        //       /       \
        //      /   (5)   \
        //     /     |     \
        //    /      |      \
        //  (1)-----(2)-----(3)
        //
        private AdaptiveGrid SampleGrid()
        {
            AdaptiveGrid grid = new AdaptiveGrid();
            var strip = grid.AddVertices(new Vector3[] {
                new Vector3(0, 0), //1
                new Vector3(5, 0), //2
                new Vector3(10, 0) //3
            }, AdaptiveGrid.VerticesInsertionMethod.Connect);

            grid.AddVertex(new Vector3(5, 5), new ConnectVertexStrategy(strip[0], strip[2]), cut: false); //4
            grid.AddVertex(new Vector3(5, 2), new ConnectVertexStrategy(strip[1]), cut: false); //5
            return grid;
        }

        private void WriteToModelWithRandomMaterials(AdaptiveGrid grid, [CallerMemberName] string memberName = "")
        {
            var random = new Random();
            Name = memberName;
            foreach (var edge in grid.GetEdges())
            {
                Model.AddElement(new ModelCurve(grid.GetLine(edge), material: random.NextMaterial()));
            }
        }
    }
}
