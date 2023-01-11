﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Elements.Algorithms
{
    class BinaryHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        public BinaryHeap()
        {
            n = 0;
            a = new List<(TKey, TValue)>();
        }

        private void heapify(int i)
        {
            while (true)
            {
                int j = (i << 1) + 1;
                if (n <= j) break;
                if (j + 1 < n && a[j + 1].Item1.CompareTo(a[j].Item1) > 0) ++j;
                if (a[j].Item1.CompareTo(a[i].Item1) <= 0) break;
                (a[i], a[j]) = (a[j], a[i]);
                i = j;
            }
        }

        public BinaryHeap((TKey, TValue)[] array)
        {
            n = array.Length;
            a = new List<(TKey, TValue)>(array);
            for (int i = n - 1; i >= 0; --i) heapify(i);
        }

        public void Insert(TKey key, TValue value)
        {
            if (n < a.Count) a[n] = (key, value);
            else a.Add((key, value));
            int i = n++;
            while (i > 0)
            {
                int j = (i - 1) >> 1;
                if (a[i].Item1.CompareTo(a[j].Item1) <= 0) break;
                (a[i], a[j]) = (a[j], a[i]);
                i = j;
            }
        }

        public void Insert((TKey, TValue) element)
        {
            Insert(element.Item1, element.Item2);
        }

        public (TKey, TValue) Extract()
        {
            (a[0], a[n - 1]) = (a[n - 1], a[0]);
            --n;
            heapify(0);
            return a[n];
        }

        public void Clear()
        {
            n = 0;
        }

        public bool Empty { get { return n == 0; } }
        public int Size { get { return n; } }
        public (TKey, TValue) Max { get { return a[0]; } }


        private int n;
        private List<(TKey, TValue)> a;
    }
}
