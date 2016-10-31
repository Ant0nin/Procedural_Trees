using UnityEngine;
using System.Collections.Generic;

public class TreeModel : MonoBehaviour
{
    public Vector3 boundingBox;
    public Tree<Bud> skeleton;
    public List<Vector3> markers;
    public GameObject leafPrefab;
}