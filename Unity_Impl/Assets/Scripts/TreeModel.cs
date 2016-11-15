using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "tree_config_", menuName = "Vegetation/Tree")]
public class TreeModel : ScriptableObject
{
    public Vector3 boundingBox;
    public Tree<Bud> skeleton;
    public List<Vector3> markers;
    public GameObject leafPrefab;

    public void reset()
    {
        boundingBox = new Vector3(1, 2, 1); // default bounding box
        Vector3 budPosition = new Vector3(boundingBox.x / 2, boundingBox.y / 2, 0); // default seed position => bottom
        Bud bud = new Bud(budPosition, true);
        Node<Bud> root = new Node<Bud>(bud);
        skeleton = new Tree<Bud>(root);
    }

    public void OnEnable()
    {
        reset();
    }
}