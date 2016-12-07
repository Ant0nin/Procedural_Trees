using UnityEngine;
using System.Collections.Generic;

public class TreeGraph<T> // generic
{
    public Node<T> root;
    public List<Node<T>> leaves;
    public List<List<Node<T>>> levels;

    public TreeGraph(Node<T> root_) {
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
}