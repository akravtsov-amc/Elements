﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Elements.Algorithms
{
    // Types of the tree initialization
    // It relates to the way the SteinerTreeCalculator data structure keeps track of the connectivity components
    // Manual     - the recomputation is done only when the Init function is called
    // AutoInsert - Uses a dsu to keep track of the components, requires that there are no DeleteEdge operations
    public enum TreeInitializationType { Manual, AutoInsert };

    class SteinerTreeCalculator
    {
        // initializes from a graph - an array of pairs (v, len) for each vertex u - vertices with which u is connected, len is the length of the edge
        // graph must be undirected!
        public SteinerTreeCalculator((int, double)[][] graph, TreeInitializationType type = TreeInitializationType.Manual)
        {
            this.type = type;

            n = graph.Length;
            g = new Dictionary<int, double>[n];
            if (type == TreeInitializationType.AutoInsert) dsu_main = new DisjointSetUnion(n);
            if (type == TreeInitializationType.Manual) comp = new int[n];
            for (var i = 0; i < n; ++i)
            {
                g[i] = new Dictionary<int, double>();
                foreach (var (k, v) in graph[i])
                    AddEdge(i, k, v);
            }

            if (type == TreeInitializationType.Manual) Init();
        }

        // an auxiliary function for the Init operation
        // marks all the vertices in a connectivity component, which contains a starting vertex
        // uses the depth-first search
        private void dfsInit(int v, int id, ref Dictionary<int, double>[] g, ref int[] used)
        {
            used[v] = 1;
            comp[v] = id;
            foreach (var ed in g[v])
            {
                if (used[ed.Key] == 1) continue;
                dfsInit(ed.Key, id, ref g, ref used);
            }
        }

        // perpares the graph for the steiner tree calculation
        // recalculates the connectivity components
        public void Init()
        {
            if (type != TreeInitializationType.Manual) return;
            var used = new int[n];
            for (int i = 0, id = 0; i < n; ++i)
                if (used[i] == 0) dfsInit(i, id++, ref g, ref used);
        }

        // initializes an empty graph with n vertices
        public SteinerTreeCalculator(int size, TreeInitializationType type = TreeInitializationType.Manual)
        {
            n = size;
            g = new Dictionary<int, double>[n];
            if (type == TreeInitializationType.AutoInsert) dsu_main = new DisjointSetUnion(n);
            if (type == TreeInitializationType.Manual) comp = new int[n];
            for (var i = 0; i < n; ++i) g[i] = new Dictionary<int, double>();
        }

        // adds an undirected weighed edge to the graph
        public void AddEdge(int u, int v, double l)
        {
            g[u][v] = g[v][u] = l;
            if (type == TreeInitializationType.AutoInsert) dsu_main.AddEdge(u, v);
        }

        // removes an undirected edge from the graph
        public void RemoveEdge(int u, int v)
        {
            g[u].Remove(v);
            g[v].Remove(u);
        }

        // a universal method to get an id of the connectivity component of a given vertex regardless of the initialization type
        // returns -1 if an initialization type is not supported
        private int getComponentId(int v)
        {
            if (type == TreeInitializationType.Manual) return comp[v];
            if (type == TreeInitializationType.AutoInsert) return dsu_main.GetParent(v);
            return -1;
        }

        // a helper function that removes hanging unnecessary vertices from the built steiner tree (based on dfs)
        // it's always called from a necessary vertex
        // asymptotic complexity: O(k), where k is the number of vertices in the subtree
        private void dfsTrim(int v, int p, ref Dictionary <int, double>[] g, ref HashSet<int> hs, ref int[] used, ref List<(int, int, double)> e)
        {
            used[v] = 2;
            foreach (var ed in g[v])
            {
                if (used[ed.Key] == 2) continue;
                dfsTrim(ed.Key, v, ref g, ref hs, ref used, ref e);
            }
            if (g[v].Count == 1 && !hs.Contains(v))
            {
                g[p].Remove(v);
                g[v].Remove(p);
            }
            else if (p != -1) e.Add((p, v, g[p][v]));
        }

        public (int, int, double)[] GetTreeMk1(int[] vertices)
        {
            // Actually calculates a set of edges in a Steiner tree based in the given set of vertices
            // Important: It's an approximate algorithm, it does not deterministically give the actual Steiner tree
            // Algorithm:
            // We start with a set of 1-vertex trees (given vertices)
            // We take the shortes edge originating in our forest, add it to the forest 
            // We do so until we have no edges left or we've ended up with only one tree
            // We trim the tree with our dfs_trim function
            // Details:
            // The heap contains a list of edges originating from our current forest
            // used describes which vertices are currently in the forest
            // gg describes the current subtree
            // dsu is used to check that we do not create cycles in the forest
            // Asymptotic complexity: O((n+m)logn), where
            //     n is the total number of vertices
            //     m is the number of edges
            var q = new BinaryHeap<double, (int, int)>();
            int[] used = new int[n];
            var gg = new Dictionary<int, double>[n];
            for (var i = 0; i < n; ++i) gg[i] = new Dictionary<int, double>();
            var dsu = new DisjointSetUnion(n);

            int cnt = vertices.Length;
            foreach (var v in vertices)
            {
                used[v] = 1;
                foreach (var ed in g[v]) q.Insert(-ed.Value, (v, ed.Key));
            }
            while (cnt > 1 && !q.Empty)
            {
                var tp = q.Extract();
                int u = tp.Item2.Item1, v = tp.Item2.Item2;
                double l = -tp.Item1;
                if (!dsu.AddEdge(u, v)) continue;
                gg[u][v] = gg[v][u] = l;
                if (used[v] > 0) --cnt;
                else
                {
                    used[v] = 1;
                    foreach (var ed in g[v]) q.Insert(-ed.Value, (v, ed.Key));
                }
            }

            HashSet<int> hs = new HashSet<int>(vertices);
            var e = new List<(int, int, double)>();
            foreach (var v in vertices)
            {
                if (used[v] == 2) continue;
                used[v] = 2;
                dfsTrim(v, -1, ref gg, ref hs, ref used, ref e);
            }
            return e.ToArray();
        }

        // Actually calculates a set of edges in a Steiner tree based in the given set of vertices
        // Important: It's an approximate algorithm, it does not deterministically give the actual Steiner tree
        // Algorithm:
        // We start with a set of 1-vertex trees (given vertices)
        // We weigh edges using the following formula:
        //     \alpha * w + \beta * comp1_size * comp2_size * w, where
        //     \alpha is given and it represents the contribution of the sum of the weights' of edges in a tree to the weight of the tree (we are looking for a tree with the minimal weight)
        //     \beta is given and it represents the contribution of the sum of the weights' of paths in a tree to the weight of the tree (we are looking for a tree with the minimal weight)
        //     w is the weight of the edge
        //     comp1_size and comp2_size are the current sizes of the components which the edge connects
        // We take the lightest edge originating in our forest, add it to the forest 
        // We do so until we have no edges left or we've ended up with only one tree
        // We trim the tree with our dfs_trim function
        // Details:
        // The heap contains a list of edges originating from our current forest
        // used describes which vertices are currently in the forest
        // gg describes the current subtree
        // dsu is used to check that we do not create cycles in the forest
        // Asymptotic complexity: O((n+m)logn), where
        //     n is the total number of vertices
        //     m is the number of edges
        public Dictionary<int, double>[] GetTreeMk2(int[] vertices, double alpha = 1.0, double beta = 0.0)
        {
            var q = new BinaryHeap<double, (int, int)>();
            int[] used = new int[n];
            var gg = new Dictionary<int, double>[n];
            for (var i = 0; i < n; ++i) gg[i] = new Dictionary<int, double>();
            var dsu = new DisjointSetUnion(n);

            var comp_cnt = new List<int>[n];
            for (int i = 0; i < n; ++i) comp_cnt[i] = new List<int>();
            foreach (var v in vertices)
            {
                comp_cnt[getComponentId(v)].Add(v);
            }

            for (int cmp = 0; cmp < n; ++cmp)
            {
                foreach (var v in comp_cnt[cmp])
                {
                    used[v] = 1;
                    foreach (var ed in g[v]) q.Insert(-(alpha * ed.Value + beta * ed.Value * dsu.ComponentSize(v) * dsu.ComponentSize(ed.Key)), (v, ed.Key));
                }

                while (!q.Empty)
                {
                    var tp = q.Extract();
                    int u = tp.Item2.Item1, v = tp.Item2.Item2;
                    if (!dsu.AddEdge(u, v)) continue;

                    gg[u][v] = gg[v][u] = g[u][v];
                    if (used[v] == 0)
                    {
                        used[v] = 1;
                        foreach (var ed in g[v]) q.Insert(-(alpha * ed.Value + beta * ed.Value * dsu.ComponentSize(v) * dsu.ComponentSize(ed.Key)), (v, ed.Key));
                    }
                }
            }

            HashSet<int> hs = new HashSet<int>(vertices);
            var e = new List<(int, int, double)>();
            foreach (var v in vertices)
            {
                if (used[v] == 2) continue;
                used[v] = 2;
                dfsTrim(v, -1, ref gg, ref hs, ref used, ref e);
            }

            return gg;
        }

        // returns the number of vertices in the graph
        public int Size { get { return n; } }

        private int n; // number of vertices
        private Dictionary<int, double>[] g; // compressed adjancency matrix
        private int[] comp; // component id of each vertex
        private TreeInitializationType type; // components computation type
        private DisjointSetUnion dsu_main; // dsu for the whole graph
    }
}
