using UnityEngine;
using System.Collections.Generic;

public class TreeModel
{
    public Vector3 boundingBox;
    public Tree<Bud> skeleton;
    public List<Vector3> markers;
    public GameObject leafPrefab;

    public TreeModel() : this(new Vector3(1, 2, 1)) {} // default bounding box

    public TreeModel(Vector3 boundingBox_)
    {
        boundingBox = boundingBox_;
        Vector3 budPosition = new Vector3(boundingBox.x / 2, boundingBox.y / 2, 0); // default seed position => bottom
        Bud bud = new Bud(budPosition);
        Node<Bud> root = new Node<Bud>(bud);
        skeleton = new Tree<Bud>(root);
    }
}