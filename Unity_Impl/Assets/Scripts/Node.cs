using UnityEngine;
using System.Collections.Generic;

public class Node<T> // generic
{
    public Node<T> parent = null;
    public List<Node<T>> childs;
    public T value;
    public uint level;

    public Node(T value_) // only for root
    {
        value = value_;
        level = 0;
        childs = new List<Node<T>>();
    }

    public Node(Node<T> parent_, T value_)
    {
        parent = parent_;
        value = value_;
        level = parent.level + 1;
        childs = new List<Node<T>>();
    }

    public bool isLeaf() {
        return (childs.Count == 0);
    }
    public bool isRoot() {
        return (parent == null);
    }
}