using UnityEngine;
using System.Collections.Generic;

public class Node<T> // generic
{
    public Node<T> parent = null;
    public List<Node<T>> childs;
    public T value;
    public int level;

    public bool isLeaf() {
        return (childs.Count == 0);
    }
    public bool isRoot() {
        return (parent == null);
    }
}