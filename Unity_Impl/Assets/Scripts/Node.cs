using UnityEngine;
using System.Collections.Generic;

public class Node<T> // generic
{
    public Node<T> parent = null;
    //public List<Node<T>> childs;
    public Node<T> main;
    public Node<T> lateral;

    public T value;
    public uint level;

    public Node(T value_) // only for root
    {
        value = value_;
        level = 0;
        main = null;
        lateral = null;
    }

    public Node(Node<T> parent_, T value_)
    {
        parent = parent_;
        value = value_;
        level = parent.level + 1;
        main = null;
        lateral = null;
    }

    public bool isLeaf() {
        return (main == null && lateral == null);
    }
    public bool isRoot() {
        return (parent == null);
    }
}