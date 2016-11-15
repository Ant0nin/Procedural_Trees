using UnityEngine;
using System.Collections.Generic;

public class Tree<T> // generic
{
    public Node<T> root;
    public List<Node<T>> leaves;
    private List<List<Node<T>>> levels;

    public Tree(Node<T> root_) {
        root = root_;
        leaves = new List<Node<T>>();
        leaves.Add(root);
        levels = new List<List<Node<T>>>();
        addInLevel(ref root);
    }

    public void addInLevel(ref Node<T> node)
    {
        if (levels.Count <= node.level)
            levels.Add(new List<Node<T>>());

        levels[(int)node.level].Add(node);
    }

    public List<Node<T>> getLevel(int n) {
        return levels[n];
    }

    public int getLevelsQuantity()
    {
        return levels.Count;
    }
}