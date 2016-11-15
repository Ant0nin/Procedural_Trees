using UnityEngine;
using System.Collections.Generic;

public class Node<T> // generic
{
    public Node<T> parent = null;
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

    public Node(ref TreeStructure<T> belongsTo, Node<T> parent_, T value_)
    {
        parent = parent_;
        value = value_;
        level = parent.level + 1;
        main = null;
        lateral = null;

        Node<T> me = this;
        belongsTo.addInLevel(ref me);
    }

    public bool isLeaf() {
        return (main == null && lateral == null);
    }
    public bool isRoot() {
        return (parent == null);
    }
}