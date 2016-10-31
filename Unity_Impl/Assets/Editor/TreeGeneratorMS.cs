using UnityEngine;
using System.Collections;

public class TreeGeneratorMS : TreePipelineComponent
{
    public int nb_markers;
    public const int NB_MARKERS_MIN = 100;
    public const int NB_MARKERS_MAX = 1000;

    public Vector3 boundingBox;

    public TreeGeneratorMS() {
        nb_markers = 500;
    }

    public void execute(TreeModel tree)
    {
        tree.markers = new Vector3[nb_markers];
        for (int i = 0; i < nb_markers; i++)
        {
            float x = Random.Range(0, boundingBox.x);
            float y = Random.Range(0, boundingBox.y);
            float z = Random.Range(0, boundingBox.z);
            tree.markers[i] = new Vector3(x, y, z);
        }
    }
}
