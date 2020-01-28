using System.Collections.Generic;
using Elements.Geometry;
using Xunit;

namespace Elements.Tests
{
    public class ModelCurveTests : ModelTest
    {
        public ModelCurveTests()
        {
            this.GenerateIfc = false;
        }

        [Trait("Category","Examples")]
        [Fact]
        public void ModelCurve()
        {
            this.Name = "Elements_ModelCurve";

            // <example>
            // A line
            var line = new Line(Vector3.Origin, new Vector3(5,5,5));

            // An arc
            var arc = new Arc(Vector3.Origin, 2.0, 45.0, 135.0);

            // A polygon
            var pline = Polygon.L(2, 2, 0.5);

            // A Bezier
            var a = Vector3.Origin;
            var b = new Vector3(5, 0, 1);
            var c = new Vector3(5, 5, 2);
            var d = new Vector3(0, 5, 3);
            var e = new Vector3(0, 0, 4);
            var f = new Vector3(5, 0, 5);
            var ctrlPts = new List<Vector3>{a,b,c,d,e,f};
            var bezier = new Bezier(ctrlPts);

            var lineModelCurve = new ModelCurve(line, new Material("Red", Colors.Red));
            var arcModelCurve = new ModelCurve(arc, new Material("Orange", Colors.Orange), new Transform(5, 0, 0));
            var plineModelCurve = new ModelCurve(pline, new Material("Purple", Colors.Purple), new Transform(10, 0, 0));
            var bezierModelCurve = new ModelCurve(bezier, new Material("Green", Colors.Green), new Transform(15, 0, 0));
            // </example>

            this.Model.AddElements(new[]{lineModelCurve, arcModelCurve, plineModelCurve, bezierModelCurve});
        }

        [Fact]
        public void OffsetModelCurves()
        {
            this.Name = "OffsetModelCurves";

            var pline = new Polygon(new []{
                new Vector3(0, 0),
                new Vector3(20, -10),
                new Vector3(25, 5),
                new Vector3(15, 3),
                new Vector3(20, 20),
                new Vector3(5, 19),
                new Vector3(0, 15)
            });
            var mcs = new List<ModelCurve>();
            var m = new Material("Purple", Colors.Blue);
            var plineModelCurve = new ModelCurve(pline, m);
            mcs.Add(plineModelCurve);
            var distance = -0.2;
            for(var i=0; i<100; i++)
            {
                mcs.AddRange(OffsetPolygon(distance, pline, m));
            }
            this.Model.AddElements(mcs);
        }

        private IList<ModelCurve>OffsetPolygon(double distance, Polygon p, Material m)
        {
            var mcs = new List<ModelCurve>();
            var offset = p.Offset(distance);
            if(offset == null || offset.Length == 0)
            {
                return mcs;
            }
            foreach(var pin in offset)
            {
                var mc = new ModelCurve(p, m);
                mcs.Add(mc);
                mcs.AddRange(OffsetPolygon(distance, pin, m));
            }
            return mcs;
        }

        [Fact]
        public void ModelPoints()
        {
            this.Name = "ModelPoints";

            var modelPoints = new ModelPoints();
            for(var x=0; x<25; x++)
            {
                for(var y=0; y<25; y++)
                {
                    for(var z=0; z<25; z++)
                    {
                        modelPoints.Locations.Add(new Vector3(x,y,z));
                    }
                }
            }
            
            this.Model.AddElement(modelPoints);
        }
    }
}