﻿namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }
        //DFS
        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();

            var currentPath = new LinkedList<int>();
            currentPath.AddFirst(this.Key);

            int curentSum = this.Key;

            this.Dfs(this, result, currentPath, ref curentSum, sum);

            return result;
        }

        private void Dfs(
            Tree<int> subTree,
            List<List<int>> result, 
            LinkedList<int> currentPath, 
            ref int curentSum, 
            int wantedSum)
        {
            foreach (var child in subTree.Children)
            {
                curentSum += child.Key;
                currentPath.AddLast(child.Key);
                this.Dfs(child, result, currentPath, ref curentSum, wantedSum);
            }

            if (curentSum == wantedSum)
            {
                result.Add(new List<int>(currentPath)); //правим това по този начин защото този израз прави копие на currentPath и запазва само стойностите! иначе двата листа се свързват с динамиччна референция и ако нещо от currentPath се премахне то ще се премахне и в result
            }

            curentSum -= subTree.Key;
            currentPath.RemoveLast();
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            var result = new List<Tree<int>>();
            var allSubtrees = this.GetAllNodesBfs();

            foreach (var subtree in allSubtrees)
            {
                if (this.HasGivenSum(subtree, sum))
                {
                    result.Add(subtree);
                }
            }

            return result;
        }

        private bool HasGivenSum(Tree<int> subtree, int wantedSum)
        {
            int actualSum = subtree.Key;
            this.DfsGetSubtreeSym(subtree, wantedSum, ref actualSum);

            return actualSum == wantedSum;
        }

        private void DfsGetSubtreeSym(Tree<int> subtree, int wantedSum, ref int actualSum)
        {
            foreach (var child in subtree.Children)
            {
                actualSum += child.Key;
                this.DfsGetSubtreeSym(child, wantedSum, ref actualSum);
            }
        }

        private List<Tree<int>> GetAllNodesBfs()
        {
            var result = new List<Tree<int>>();
            var queue = new Queue<Tree<int>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }

            }

            return result;
        }
    }
}
