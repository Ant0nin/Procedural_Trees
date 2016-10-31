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

    static bool b_init = false;
    static TreeGeneratorPipeline pl = null;

    /*static float param_lambda;
    static float param_alpha;
    static float param_epsilon;
    static float param_eta;
    static int param_n;*/

    public override void OnInspectorGUI()
    {
        if (!b_init) {
            pl = new TreeGeneratorPipeline();
            pl.step_ms = new TreeGeneratorMS();
            pl.step_sc = new TreeGeneratorSC();
            pl.step_bh = new TreeGeneratorBH();
            pl.step_sa = new TreeGeneratorSA();
            pl.step_bw = new TreeGeneratorBW();
        }

        TreeModel tree = (TreeModel)target;
        GameObject gameObj = tree.gameObject;
        Mesh mesh = gameObj.GetComponent<Mesh>();

        if (mesh)
            pl.step_ms.boundingBox = mesh.bounds.size;
        else
            pl.step_ms.boundingBox = new Vector3(1, 2, 1);

        pl.step_ms.boundingBox = EditorGUILayout.Vector3Field("Bounding box", pl.step_ms.boundingBox);
        pl.nb_it = EditorGUILayout.IntSlider("Number of iterations", pl.nb_it, TreeGeneratorPipeline.NB_IT_MIN, TreeGeneratorPipeline.NB_IT_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---

        display_boundingBox = EditorGUILayout.Toggle("Show bounds", display_boundingBox);
        display_markers = EditorGUILayout.Toggle("Show markers", display_markers);
        display_skeleton = EditorGUILayout.Toggle("Show skeleton", display_skeleton);
        display_leaves = EditorGUILayout.Toggle("Show leaves", display_leaves);
        display_texture = EditorGUILayout.Toggle("Show textures", display_texture);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---

        pl.step_ms.nb_markers = EditorGUILayout.IntSlider("Number of markers", pl.step_ms.nb_markers, TreeGeneratorMS.NB_MARKERS_MIN, TreeGeneratorMS.NB_MARKERS_MAX);
        /*param_theta = EditorGUILayout.Slider("theta", param_theta, THETA_MIN, THETA_MAX);
        param_r = EditorGUILayout.Slider("r", param_r, R_MIN, R_MAX);
        param_phi = EditorGUILayout.Slider("phi", param_phi, PHI_MIN, PHI_MAX);*/

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

        Mesh outputMesh = pl.execute();

        string outputFilename = "Assets/Meshes/tree_mesh_01.asset"; // TODO : dynamic path
        AssetDatabase.CreateAsset(outputMesh, outputFilename);
        meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(outputFilename);
    }
}
