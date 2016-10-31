using UnityEngine;
using System.Collections.Generic;

public class Tree<T> // generic
{
    public Node<T> root;
    public List<Node<T>> leaves;
}