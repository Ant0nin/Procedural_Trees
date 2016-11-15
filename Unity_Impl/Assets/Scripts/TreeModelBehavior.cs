using UnityEngine;
using System.Collections;

public class TreeModelBehavior : MonoBehaviour
{
    public TreeModel treeModel;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector3 marker in treeModel.markers)
        {
            Gizmos.DrawSphere(transform.position + marker, 0.02f); // TODO : need translation
        }
    }
}
