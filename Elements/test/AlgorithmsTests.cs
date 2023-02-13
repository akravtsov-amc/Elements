﻿using System;
using System.Collections.Generic;
using Elements.Algorithms;
using Elements.Tests;
using System.Linq;
using Xunit;

namespace Elements.Algorithms.Tests
{
    public class AlgorithmsTests : ModelTest
    {
        public AlgorithmsTests()
        {
            this.GenerateIfc = false;
        }

        /// <summary>
        /// Tests whether the Algorithms.DisjointSetUnion initializes without exceptions
        /// </summary>
        [Fact]
        public void DsuInitTest()
        {
            var dsu = new DisjointSetUnion(0);
            dsu = new DisjointSetUnion(1000);
        }

        /// <summary>
        /// tests whether the Algorithms.DisjointSetUnion works properly
        /// </summary>
        [Fact]
        public void DsuTest()
        {
            var dsu = new DisjointSetUnion(10);
            bool res;
            res = dsu.AddEdge(0, 1); Assert.True(res);
            res = dsu.AddEdge(9, 3); Assert.True(res);
            res = dsu.AddEdge(9, 6); Assert.True(res);
            res = dsu.AddEdge(6, 0); Assert.True(res);
            res = dsu.AddEdge(6, 2); Assert.True(res);
            res = dsu.AddEdge(5, 3); Assert.True(res);
            res = dsu.AddEdge(1, 3); Assert.True(!res);
            res = dsu.AddEdge(9, 0); Assert.True(!res);
            res = dsu.AddEdge(8, 7); Assert.True(res);
            res = dsu.AddEdge(5, 6); Assert.True(!res);
            res = dsu.AddEdge(9, 2); Assert.True(!res);

            Assert.Equal(3, dsu.NumComponents);
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(1));
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(2));
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(3));
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(5));
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(6));
            Assert.Equal(dsu.GetParent(0), dsu.GetParent(9));
            Assert.Equal(dsu.GetParent(7), dsu.GetParent(8));
            Assert.NotEqual(dsu.GetParent(0), dsu.GetParent(4));
            Assert.NotEqual(dsu.GetParent(0), dsu.GetParent(8));
            Assert.NotEqual(dsu.GetParent(8), dsu.GetParent(4));
            Assert.Equal(7, dsu.ComponentSize(0));
            Assert.Equal(2, dsu.ComponentSize(7));
            Assert.Equal(1, dsu.ComponentSize(4));
        }

        /// <summary>
        /// Tests whether the first, simple version of the Steiner tree calculation works well
        /// </summary>
        [Fact]
        public void SteinerTreeMk1Test()
        {
            var graph = new SteinerTreeCalculator(7);
            graph.AddEdge(0, 1, 7);
            graph.AddEdge(2, 4, 3);
            graph.AddEdge(5, 3, 4);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(5, 4, 10);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(0, 5, 6);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(1, 5, 5);

            var edges = new HashSet<(int, int, double)>(graph.GetTreeMk1(new int[4] {0, 2, 4, 5 }));
            Assert.Equal(4, edges.Count);
            Assert.True(edges.Contains((0, 5, 6)) || edges.Contains((5, 0, 6)));
            Assert.True(edges.Contains((3, 5, 4)) || edges.Contains((5, 3, 4)));
            Assert.True(edges.Contains((3, 4, 1)) || edges.Contains((4, 3, 1)));
            Assert.True(edges.Contains((3, 2, 1)) || edges.Contains((2, 3, 1)));
        }

        /// <summary>
        /// Tests whether the second, more customizable version of the Steiner tree calculation works well
        /// </summary>
        [Fact]
        public void SteinerTreeMk2Test()
        {
            var graph = new SteinerTreeCalculator(7);
            graph.AddEdge(0, 1, 7);
            graph.AddEdge(2, 4, 3);
            graph.AddEdge(5, 3, 4);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(5, 4, 10);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(0, 5, 6);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(1, 5, 5);

            var tree = graph.GetTreeMk2(new int[4] { 0, 2, 4, 5 });
            var edges = new HashSet<(int, int, double)>();
            for (int i = 0; i < 7; ++i)
            {
                foreach (var ed in tree[i])
                {
                    edges.Add((i, ed.Key, ed.Value));
                }
            }
            Assert.Equal(8, edges.Count);
            Assert.True(edges.Contains((0, 5, 6)) && edges.Contains((5, 0, 6)));
            Assert.True(edges.Contains((3, 5, 4)) && edges.Contains((5, 3, 4)));
            Assert.True(edges.Contains((3, 4, 1)) && edges.Contains((4, 3, 1)));
            Assert.True(edges.Contains((3, 2, 1)) && edges.Contains((2, 3, 1)));
        }

        [Fact]
        public void TreapTest()
        {
            var t = new Treap<double>();
            Func<int, int> sqr = x => x * x;
            Func<double, int> f = x => sqr((int)Math.Floor(x));
            TreapIterator<double> it = null;

            // Step 0
            t.Insert(6.95426297609277);
            it = t.LowerBound<int>(73, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.95426297609277, it.Get, 12);
            // Step 1
            t.Insert(5.665417755480634);
            it = t.UpperBound<int>(99, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.95426297609277, it.Get, 12);
            // Step 2
            t.Erase(5.665417755480634);
            it = t.LowerBound<int>(39, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 3
            t.Erase(6.95426297609277);
            it = t.UpperBound<int>(72, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MovePrevious());
            // Step 4
            t.Insert(8.458901392754028);
            it = t.LowerBound<int>(72, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 5
            t.Insert(0.7628652995883478);
            it = t.UpperBound<int>(32, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.False(it.MoveNext());
            // Step 6
            t.Insert(3.9858345198441683);
            it = t.UpperBound<int>(34, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.False(it.MoveNext());
            // Step 7
            t.Insert(0.8811150703804027);
            it = t.UpperBound<int>(17, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.False(it.MoveNext());
            // Step 8
            t.Erase(3.9858345198441683);
            it = t.LowerBound<int>(0, f);
            Assert.False(it.IsEnd);
            Assert.Equal(0.7628652995883478, it.Get, 12);
            Assert.False(it.MovePrevious());
            // Step 9
            t.Insert(1.0666842462211046);
            it = t.UpperBound<int>(87, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(8.458901392754028, it.Get, 12);
            // Step 10
            t.Insert(9.902345635845744);
            it = t.LowerBound<int>(33, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(1.0666842462211046, it.Get, 12);
            // Step 11
            t.Insert(2.5257839781072278);
            it = t.UpperBound<int>(37, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(2.5257839781072278, it.Get, 12);
            // Step 12
            t.Insert(6.622395637737052);
            it = t.UpperBound<int>(100, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 13
            t.Insert(6.2457546115220905);
            it = t.UpperBound<int>(41, f);
            Assert.False(it.IsEnd);
            Assert.Equal(8.458901392754028, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 14
            t.Erase(6.2457546115220905);
            it = t.UpperBound<int>(25, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.622395637737052, it.Get, 12);
            Assert.True(it.MoveNext());
            Assert.Equal(8.458901392754028, it.Get, 12);
            // Step 15
            t.Erase(9.902345635845744);
            it = t.UpperBound<int>(30, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.622395637737052, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(2.5257839781072278, it.Get, 12);
            // Step 16
            t.Erase(8.458901392754028);
            it = t.UpperBound<int>(20, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.622395637737052, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(2.5257839781072278, it.Get, 12);
            // Step 17
            t.Erase(0.8811150703804027);
            it = t.LowerBound<int>(5, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.622395637737052, it.Get, 12);
            Assert.False(it.MoveNext());
            // Step 18
            t.Insert(4.263346549515394);
            it = t.LowerBound<int>(90, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 19
            t.Erase(0.7628652995883478);
            it = t.UpperBound<int>(27, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.622395637737052, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(4.263346549515394, it.Get, 12);
            // Step 20
            t.Insert(6.484569168862656);
            it = t.LowerBound<int>(6, f);
            Assert.False(it.IsEnd);
            Assert.Equal(4.263346549515394, it.Get, 12);
            Assert.True(it.MovePrevious());
            Assert.Equal(2.5257839781072278, it.Get, 12);
            // Step 21
            t.Insert(2.953355210915051);
            it = t.UpperBound<int>(19, f);
            Assert.False(it.IsEnd);
            Assert.Equal(6.484569168862656, it.Get, 12);
            Assert.True(it.MoveNext());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 22
            t.Insert(5.963838801378268);
            it = t.UpperBound<int>(37, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 23
            t.Erase(2.5257839781072278);
            it = t.LowerBound<int>(38, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 24
            t.Insert(1.5455745882032579);
            it = t.UpperBound<int>(59, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 25
            t.Insert(3.8913227990191035);
            it = t.UpperBound<int>(65, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 26
            t.Insert(3.4735212506625626);
            it = t.UpperBound<int>(87, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 27
            t.Insert(3.2907627531754424);
            it = t.LowerBound<int>(100, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
            // Step 28
            t.Insert(4.847498101295202);
            it = t.UpperBound<int>(58, f);
            Assert.True(it.IsEnd);
            Assert.False(it.MoveNext());
            // Step 29
            t.Erase(6.484569168862656);
            it = t.LowerBound<int>(90, f);
            Assert.True(it.IsEnd);
            Assert.True(it.MovePrevious());
            Assert.Equal(6.622395637737052, it.Get, 12);
        }
    }
}
