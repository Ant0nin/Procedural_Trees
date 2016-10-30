using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections;

[CustomEditor(typeof(TreeModel))]
public class TreeInspector : Editor
{
    static bool display_boundingBox;
    static bool display_markers;
    static bool display_skeleton;
    static bool display_leaves;
    static bool display_texture;

    Vector3 param_boundingBox;

    static int param_nb_it;
    const int NB_IT_MIN = 1;
    const int NB_IT_MAX = 100;

    // space colonization ------
    static int param_nb_markers;
    static float param_theta;
    static float param_r;
    static float param_phi;
    const int NB_MARKERS_MIN = 10;
    const int NB_MARKERS_MAX = 1000;
    const float THETA_MIN = 60;
    const float THETA_MAX = 120;
    const float R_MIN = 4;
    const float R_MAX = 6;
    const float PHI_MIN = 1.9f;
    const float PHI_MAX = 2.1f;
    // -------------------------

    static float param_lambda;
    static float param_alpha;
    static float param_epsilon;
    static float param_eta;
    static int param_n;

    public override void OnInspectorGUI()
    {
        TreeModel tree = (TreeModel)target;
        GameObject gameObj = tree.gameObject;
        Mesh mesh = gameObj.GetComponent<Mesh>();

        if (mesh)
            param_boundingBox = mesh.bounds.size;
        else
            param_boundingBox = new Vector3(1, 2, 1);

        param_boundingBox = EditorGUILayout.Vector3Field("Bounding box", param_boundingBox);
        param_nb_it = EditorGUILayout.IntSlider("Number of iterations", param_nb_it, NB_IT_MIN, NB_IT_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---

        display_boundingBox = EditorGUILayout.Toggle("Show bounds", display_boundingBox);
        display_markers = EditorGUILayout.Toggle("Show markers", display_markers);
        display_skeleton = EditorGUILayout.Toggle("Show skeleton", display_skeleton);
        display_leaves = EditorGUILayout.Toggle("Show leaves", display_leaves);
        display_texture = EditorGUILayout.Toggle("Show textures", display_texture);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---

        param_nb_markers = EditorGUILayout.IntSlider("Number of markers", param_nb_markers, NB_MARKERS_MIN, NB_MARKERS_MAX);
        param_theta = EditorGUILayout.Slider("theta", param_theta, THETA_MIN, THETA_MAX);
        param_r = EditorGUILayout.Slider("r", param_r, R_MIN, R_MAX);
        param_phi = EditorGUILayout.Slider("phi", param_phi, PHI_MIN, PHI_MAX);

        // ...

        if (GUILayout.Button("Generate tree")) {
            OnButtonClick();
        }
    }
    
    /*public void OnSceneGUI()
    {
        if (display_leaves) {
            // ...
        }
        if (display_texture) {
            // ...
        }
        if (display_boundingBox) {
            // ...
        }
        if (display_markers) {
            // ...
        }
        if (display_skeleton) {
            // ...
        }
    }*/

    public void OnButtonClick()
    {
        TreeModel tree = (TreeModel)target;
        GameObject gameObj = tree.gameObject;
        MeshFilter meshFilter = gameObj.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObj.GetComponent<MeshRenderer>();

        // reset -------
        if(!meshFilter)
            meshFilter = gameObj.AddComponent<MeshFilter>();
        /*else if (meshFilter && meshFilter.sharedMesh)
            DestroyImmediate(meshFilter.sharedMesh, true);*/

        if (!meshRenderer)
            meshRenderer = gameObj.AddComponent<MeshRenderer>();
        // TODO : remove leaves into the scene (child gameobjects)
        // -------------

        TreeGeneratorPipeline process = new TreeGeneratorPipeline();
        Mesh outputMesh = process.execute();

        string outputFilename = "Assets/Meshes/tree_mesh_01.asset"; // TODO : dynamic path
        AssetDatabase.CreateAsset(outputMesh, outputFilename);
        meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(outputFilename);
    }
}
