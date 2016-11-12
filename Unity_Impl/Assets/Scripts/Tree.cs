using UnityEngine;
using System.Collections.Generic;

public class Tree<T> // generic
{
    public Node<T> root;
    public List<Node<T>> leaves;
    // TODO : possibilité d'accéder aux differents 'level' pour pouvoir parcourir le graph-tree en largeur

    public Tree(Node<T> root_) {
        root = root_;
        leaves = new List<Node<T>>();
        leaves.Add(root);
    }
}