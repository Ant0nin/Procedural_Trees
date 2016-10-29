using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "tree_", menuName = "TreeModel")]
public class TreeModel : ScriptableObject
{
    public Node<Vector3> root;
    // texture, etc...
}