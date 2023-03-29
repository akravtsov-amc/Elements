﻿using Elements.Geometry;
using Elements.Geometry.Solids;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elements.Spatial.AdaptiveGrid
{
    /// <summary>
    /// AdaptiveGrid obstacle represented by a set of points with extra parameters.
    /// Points are used to created bounding box that is aligned with transformation parameter
    /// with extra offset. Since offset is applied on the box, distance on corners is even larger.
    /// Can be constructed from different objects.
    /// </summary>
    public class Obstacle : GeometricElement
    {
        private double _offset;
        private Polygon _boundary;
        private double _height;
        private readonly List<Polygon> _secondaryPolygons = new List<Polygon>();
        private readonly List<Polygon> _primaryPolygons = new List<Polygon>();

        /// <summary>
        /// Create an obstacle from a column.
        /// </summary>
        /// <param name="column">Column to avoid.</param>
        /// <param name="offset">Extra space around obstacle bounding box.</param>
        /// <param name="addPerimeterEdges">Should edges be created around obstacle.</param>
        /// <param name="allowOutsideBoundary">Should edges be created when obstacle is outside of <see cref="AdaptiveGrid.Boundaries"/></param>
        /// <returns>New obstacle object.</returns>
        public static Obstacle FromColumn(Column column, double offset = 0, bool addPerimeterEdges = false, bool allowOutsideBoundary = false)
        {
            var p = column.Profile.Perimeter.TransformedPolygon(
                new Transform(column.Location));
            return new Obstacle(p, column.Height, offset, addPerimeterEdges, allowOutsideBoundary, null);
        }

        /// <summary>
        /// Create an obstacle from a wall.
        /// </summary>
        /// <param name="wall">Wall to avoid.</param>
        /// <param name="offset">Extra space around obstacle bounding box.</param>
        /// <param name="addPerimeterEdges">Should edges be created around obstacle.</param>
        /// <param name="allowOutsideBoundary">Should edges be created when obstacle is outside of <see cref="AdaptiveGrid.Boundaries"/></param>
        /// <returns>New obstacle object.</returns>
        public static Obstacle FromWall(StandardWall wall, double offset = 0, bool addPerimeterEdges = false, bool allowOutsideBoundary = false)
        {
            var ortho = wall.CenterLine.Direction().Cross(Vector3.ZAxis);
            var polygon = new Polygon
            (
                wall.CenterLine.Start + ortho * wall.Thickness / 2,
                wall.CenterLine.End + ortho * wall.Thickness / 2,
                wall.CenterLine.End - ortho * wall.Thickness / 2,
                wall.CenterLine.Start - ortho * wall.Thickness / 2
            );

            var transfrom = new Transform(Vector3.Origin, wall.CenterLine.Direction(), ortho, Vector3.ZAxis);

            return new Obstacle(polygon, wall.Height, offset, addPerimeterEdges, allowOutsideBoundary, transfrom);
        }

        /// <summary>
        /// Create an obstacle from a bounding box.
        /// </summary>
        /// <param name="box">Bounding box to avoid.</param>
        /// <param name="offset">Extra space around obstacle bounding box.</param>
        /// <param name="addPerimeterEdges">Should edges be created around obstacle.</param>
        /// <param name="allowOutsideBoundary">Should edges be created when obstacle is outside of <see cref="AdaptiveGrid.Boundaries"/></param>
        /// <returns>New obstacle object.</returns>
        public static Obstacle FromBBox(BBox3 box, double offset = 0, bool addPerimeterEdges = false, bool allowOutsideBoundary = false)
        {
            var polygon = new Polygon
            (
                new Vector3(box.Min.X, box.Min.Y, box.Min.Z),
                new Vector3(box.Min.X, box.Max.Y, box.Min.Z),
                new Vector3(box.Max.X, box.Max.Y, box.Min.Z),
                new Vector3(box.Max.X, box.Min.Y, box.Min.Z)
            );

            var height = box.Max.Z - box.Min.Z;

            return new Obstacle(polygon, height, offset, addPerimeterEdges, allowOutsideBoundary, null);
        }

        /// <summary>
        /// Create an obstacle from a line.
        /// </summary>
        /// <param name="line">Line to avoid.</param>
        /// <param name="offset">Extra space around obstacle bounding box. Should be larger than 0.</param>
        /// <param name="addPerimeterEdges">Should edges be created around obstacle.</param>
        /// <param name="allowOutsideBoundary">Should edges be created when obstacle is outside of <see cref="AdaptiveGrid.Boundaries"/></param>
        /// <returns>New obstacle object.</returns>
        public static Obstacle FromLine(Line line, double offset = 0.1, bool addPerimeterEdges = false, bool allowOutsideBoundary = false)
        {
            if (offset < Vector3.EPSILON)
            {
                throw new ArgumentException("Offset should be larger then zero.");
            }

            Transform frame = null;
            Polygon polygon = null;
            var height = offset * 2;
            if (!line.Direction().IsParallelTo(Vector3.ZAxis))
            {
                var forward = line.Direction();
                var ortho = Vector3.ZAxis.Cross(forward);
                var up = forward.Cross(ortho);
                frame = new Transform(line.Start, forward, ortho, up);

                polygon = new Polygon
                (
                    line.Start + offset * (forward.Negate() + ortho - up),
                    line.Start + offset * (forward.Negate() - ortho - up),
                    line.End + offset * (forward - ortho - up),
                    line.End + offset * (forward + ortho - up)
                );
            }
            else
            {
                var lowPoint = line.Start.Z < line.End.Z ? line.Start : line.End;
                polygon = new Polygon
                (
                    lowPoint + offset * (Vector3.XAxis + Vector3.YAxis - Vector3.ZAxis),
                    lowPoint + offset * (Vector3.XAxis - Vector3.YAxis - Vector3.ZAxis),
                    lowPoint + offset * (Vector3.XAxis.Negate() - Vector3.YAxis - Vector3.ZAxis),
                    lowPoint + offset * (Vector3.XAxis.Negate() + Vector3.YAxis - Vector3.ZAxis)
                );
                height += line.Length();
            }


            return new Obstacle(polygon, height, 0, addPerimeterEdges, allowOutsideBoundary, frame);
        }

        /// <summary>
        /// Create an obstacle from a list of points.
        /// </summary>
        /// <param name="boudary">Boundary of an obstacle</param>
        /// <param name="height">Height of an obstacle</param>
        /// <param name="offset">Extra space around obstacle bounding box.</param>
        /// <param name="addPerimeterEdges">Should edges be created around obstacle.</param>
        /// <param name="allowOutsideBoundary">Should edges be created when obstacle is outside of <see cref="AdaptiveGrid.Boundaries"/></param>
        /// <param name="orienatation">Orientation of the obstacle in space. Helpful for better bounding box creation.</param>
        public Obstacle(Polygon boudary, double height, double offset, bool addPerimeterEdges, bool allowOutsideBoundary, Transform orienatation)
        {
            Boundary = boudary;
            Height = height;
            Offset = offset;
            AddPerimeterEdges = addPerimeterEdges;
            AllowOutsideBoudary = allowOutsideBoundary;
            Orientation = orienatation;

            Material = BuiltInMaterials.Mass;

            UpdatePolygons();
        }

        /// <summary>
        /// List of points defining obstacle.
        /// </summary>
        public List<Vector3> Points => _primaryPolygons?.SelectMany(x => x.Vertices).ToList() ?? new List<Vector3>();
        public List<Vector3> OneSidePoints => _primaryPolygons?.Count > 0 ? _primaryPolygons.First().Vertices.ToList() : new List<Vector3>();

        /// <summary>
        /// Perimeter defining obstacle.
        /// </summary>
        public Polygon Boundary
        {
            get => _boundary;
            set
            {
                _boundary = value;
                UpdatePolygons();
            }
        }

        /// <summary>
        /// Additional information about obstacle orientation in space.
        /// Use inverted orientation to work in local space of the obstacle.
        /// </summary>
        public Transform Orientation
        {
            get;
            set;
        }

        /// <summary>
        /// Obstacle height, offset by Boundary normal vector
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                UpdatePolygons();
            }
        }

        /// <summary>
        /// Offset of bounding box created from the list of points.
        /// </summary>
        public double Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                UpdatePolygons();
            }
        }

        /// <summary>
        /// Should edges be created around obstacle.
        /// If false - any intersected edges are just discarded.
        /// If true - intersected edges are cut to obstacle and perimeter edges are inserted.
        /// </summary>
        public bool AddPerimeterEdges { get; set; }

        /// <summary>
        /// Should edges be created when obstacle is outside <see cref="AdaptiveGrid.Boundaries"/>, it will work only when <see cref="AddPerimeterEdges"/> property is true />
        /// </summary>
        public bool AllowOutsideBoudary { get; set; }

        /// <summary>
        /// Check if any segment of polyline intersects with obstacle or is inside of obstacle
        /// </summary>
        /// <param name="polyline">Polyline to check</param>
        /// <param name="tolerance">Tolerance of checks</param>
        /// <returns>Result of check</returns>
        public bool Intersects(Polyline polyline, double tolerance = 1e-05)
        {
            return polyline.Segments().Any(s => Intersects(s, tolerance));
        }

        public bool Intersects(Line line, double tolerance = 1e-5)
        {
            return Intersects(line, out _, tolerance).Count > 0;
        }

        private List<Polygon> OffsetPolygon(Polygon polygon, double offset)
        {
            var transform = new Transform(polygon.Vertices.First(), polygon.Plane().Normal);
            var invertedTransform = transform.Inverted();
            return polygon.TransformedPolygon(invertedTransform).Offset(offset).Select(poly => poly.TransformedPolygon(transform)).ToList();
        }

        private void InsertAndMerge(List<Line> lst, Line ln, double tolerance)
        {
            if (lst.Count > 0 && (ln.Start - lst.Last().End).LengthSquared() <= tolerance * tolerance)
            {
                lst[lst.Count - 1] = new Line(lst.Last().Start, ln.End);
            }
            else
            {
                lst.Add(ln);
            }
        }

        private void TrimWithPolygon(Line line, Polygon polygon, out List<Line> insideSegments, out List<Line> outsideSegments, double tolerance)
        {
            insideSegments = new List<Line>();
            var tempInsideSegments = line.Trim(polygon, out outsideSegments);
            foreach (var segment in tempInsideSegments)
            {
                if (OffsetPolygon(polygon, tolerance).Any(poly => poly.Contains(segment.Start) || poly.Contains(segment.End) || poly.Edges().Any(e => new Line(e.from, e.to).Intersects(segment, out _))))
                {
                    insideSegments.Add(segment);
                }
                else
                {
                    InsertAndMerge(outsideSegments, segment, tolerance);
                }
            }
        }

        private List<Line> IntersectPerpendicular(Line line, out List<Line> outsideSegments, double tolerance)
        {
            var insideSegments = new List<Line>();
            outsideSegments = new List<Line>();

            var basePolygon = _primaryPolygons.First();
            var secondBasePolygon = _primaryPolygons.Last();
            var lineVector = line.End - line.Start;
            line.Intersects(basePolygon.Plane(), out var point1, infinite: true);

            if (!OffsetPolygon(basePolygon, tolerance).Any(poly => poly.Contains(point1)))
            {
                outsideSegments.Add(new Line(line.Start, line.End));
                return insideSegments;
            }
            line.Intersects(secondBasePolygon.Plane(), out var point2, infinite: true);

            double q0 = lineVector.LengthSquared();
            double q1 = lineVector.Dot(point1 - line.Start);
            double q2 = lineVector.Dot(point2 - line.Start);
            if (q1 > q2)
            {
                (q1, q2, point1, point2) = (q2, q1, point2, point1);
            }

            if (q1 <= 0)
            {
                if (q2 < q0)
                {
                    outsideSegments.Add(new Line(point2, line.End));
                    insideSegments.Add(new Line(line.Start, point2));
                }
                else
                {
                    insideSegments.Add(new Line(line.Start, line.End));
                }
            }
            else
            {
                outsideSegments.Add(new Line(line.Start, point1));
                if (q2 < q0)
                {
                    outsideSegments.Add(new Line(point2, line.End));
                    insideSegments.Add(new Line(point1, point2));
                }
                else
                {
                    insideSegments.Add(new Line(point1, line.End));
                }
            }
            return insideSegments;
        }

        public List<Line> Intersects(Line line, out List<Line> outsideSegments, double tolerance = 1e-5)
        {
            outsideSegments = new List<Line>();
            var insideSegments = new List<Line>();

            if (_primaryPolygons.Count == 0)
            {
                outsideSegments.Add(new Line(line.Start, line.End));
                return insideSegments;
            }
            foreach (var polygon in _primaryPolygons)
            {
                if (!line.IsOnPlane(polygon.Plane()))
                {
                    continue;
                }

                TrimWithPolygon(line, polygon, out insideSegments, out outsideSegments, tolerance);
                return insideSegments;
            }
            var basePolygon = _primaryPolygons.First();
            var secondBasePolygon = _primaryPolygons.Last();
            var normal = basePolygon.Normal().Unitized().Negate();
            var lineVector = line.End - line.Start;
            if (lineVector.Cross(normal).IsZero())
            {
                return IntersectPerpendicular(line, out outsideSegments, tolerance);
            }
            var center = basePolygon.Vertices.First();
            double hStart = normal.Dot(line.Start - center);
            double hEnd = normal.Dot(line.End - center);
            double hSecond = normal.Dot(secondBasePolygon.Vertices.First() - center);
            bool reversed = false;

            if (hStart > hEnd)
            {
                (hStart, hEnd) = (hEnd, hStart);
                line = line.Reversed();
                reversed = true;
            }

            var tempOutsideSegments = new List<Line>();
            if (hEnd <= 0 || hStart >= hSecond)
            {
                tempOutsideSegments.Add(new Line(line.Start, line.End));
            }
            else
            {
                Line leftExtraSegment = null;
                Line rightExtraSegment = null;

                var point1 = line.Start - normal * hStart;
                if (hStart < 0)
                {
                    line.Intersects(basePolygon.Plane(), out point1);
                    hStart = 0;
                    leftExtraSegment = new Line(line.Start, point1);
                }
                var point2 = line.End - normal * hEnd;
                if (hEnd > hSecond)
                {
                    line.Intersects(secondBasePolygon.Plane(), out point2);
                    rightExtraSegment = new Line(point2, line.End);
                    hEnd = hSecond;
                    point2 -= normal * hEnd;
                }

                var localLine = new Line(point1, point2);
                var localLineVector = point2 - point1;
                var localMaxParameter = localLineVector.LengthSquared();
                TrimWithPolygon(localLine, basePolygon, out var localInsideSegments, out var localOutsideSegments, tolerance);

                var localPointParameter = new Func<Vector3, double>(p => localLineVector.Dot(p - point1) / localMaxParameter);
                var bringPointUp = new Func<Vector3, Vector3>(p => p + (localPointParameter(p) * (hEnd - hStart) + hStart) * normal);
                var bringLineUp = new Func<Line, Line>(ln => new Line(bringPointUp(ln.Start), bringPointUp(ln.End)));

                if (leftExtraSegment != null)
                {
                    tempOutsideSegments.Add(leftExtraSegment);
                }
                localOutsideSegments.ForEach(ln => InsertAndMerge(tempOutsideSegments, bringLineUp(ln), tolerance));
                if (rightExtraSegment != null)
                {
                    InsertAndMerge(tempOutsideSegments, rightExtraSegment, tolerance);
                }

                insideSegments = localInsideSegments.Select(bringLineUp).ToList();
            }

            if (reversed)
            {
                var reversedListOfLines = new Func<List<Line>, List<Line>>(lst => lst.Select(ln => ln.Reversed()).Reverse().ToList());
                outsideSegments = reversedListOfLines(tempOutsideSegments);
                insideSegments = reversedListOfLines(insideSegments);
                line = line.Reversed();
            }
            else
            {
                outsideSegments = tempOutsideSegments;
            }

            return insideSegments;
        }
        
        /// <summary>
        /// Create visual representation of obstacle
        /// </summary>
        public override void UpdateRepresentations()
        {
            var allPolygons = _secondaryPolygons.ToList();
            allPolygons.AddRange(_primaryPolygons);

            Representation = new Representation(allPolygons.Select(x => new Lamina(x)).Cast<SolidOperation>().ToList());
        }

        private static bool DoesPolygonIntersectsWithLine(Polygon polygon, Line line, double tolerance = 1e-05)
        {
            var plane = polygon.Plane();
            if (!line.Intersects(plane, out var intersection, true))
            {
                return false;
            }

            if (!polygon.Contains(intersection, out _))
            {
                return false;
            }

            var points = new List<Vector3> { line.Start, line.End };

            return points.Any(x => plane.SignedDistanceTo(x) < -tolerance);

        }

        private static bool IntersectsWithHorizontalPolygon(Polygon polygon, Line line, double tolerance = 1e-05)
        {
            if (polygon.Contains(line.Start, out _) || polygon.Contains(line.End, out _))
            {
                return true;
            }

            if (!polygon.Intersects(line, out var intersections, false, includeEnds: true))
            {
                return false;
            }

            return intersections.Any() && intersections.Count % 2 == 0;
        }

        private void UpdatePolygons()
        {
            if (Boundary == null)
            {
                return;
            }

            _secondaryPolygons.Clear();
            _primaryPolygons.Clear();

            var boundaryTransform = new Transform(Boundary.Centroid(), Boundary.Plane().Normal);
            var boundary = Boundary.TransformedPolygon(boundaryTransform.Inverted()).Offset(Offset).FirstOrDefault();

            if (boundary == null)
            {
                return;
            }

            boundary = boundary.TransformedPolygon(boundaryTransform);

            if (!boundary.IsClockWise())
            {
                boundary = boundary.Reversed();
            }

            var offsetVector = boundary.Normal() * Offset;
            boundary = boundary.TransformedPolygon(new Transform(offsetVector));
            _primaryPolygons.Add(boundary);

            var topVector = boundary.Normal().Negate() * (Height + 2 * Offset);

            if (topVector.IsAlmostEqualTo(Vector3.Origin))
            {
                return;
            }

            var topBoundary = boundary.TransformedPolygon(new Transform(topVector)).Reversed();
            _primaryPolygons.Add(topBoundary);

            _secondaryPolygons.AddRange(_primaryPolygons);
            foreach (var segment in boundary.Segments())
            {
                var polygon = new Polygon(segment.Start, segment.End, segment.End + topVector, segment.Start + topVector).Reversed();
                _secondaryPolygons.Add(polygon);
            }
        }
    }
}
