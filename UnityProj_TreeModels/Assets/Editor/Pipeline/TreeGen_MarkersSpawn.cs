using UnityEngine;
using System.Collections.Generic;

public class TreeGen_MarkersSpawn : ITreeGenComp
{
    public int nb_markers = 75;
    public Vector3 boundingBox = new Vector3(10f, 10f, 10f);
    public const int NB_MARKERS_MIN = 10;
    public const int NB_MARKERS_MAX = 300;

    public void execute(ref TreeData tree)
    {
        tree.markers = new List<Vector3>(nb_markers);
        tree.boundingBox = boundingBox;

        for (int i = 0; i < nb_markers; i++)
        {
            float x = Random.Range(0, tree.boundingBox.x);
            float y = Random.Range(0, tree.boundingBox.y);
            float z = Random.Range(0, tree.boundingBox.z);
            tree.markers.Add(new Vector3(x, y, z));
        }

        Vector3 budPosition = new Vector3(boundingBox.x / 2, 0, boundingBox.z / 2); // default seed position => bottom
        Bud bud = new Bud(budPosition, true);
        Node<Bud> root = new Node<Bud>(bud);
        tree.skeleton = new TreeGraph<Bud>(root);
    }
}
