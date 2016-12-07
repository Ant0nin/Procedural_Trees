using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(TreeData))]
public class TreeInspector : Editor
{
    TreeGen pl = null;
    TreeGen_MarkersSpawn step_ms = null;
    TreeGen_SpaceColonization step_sc = null;
    TreeGen_BorchertHonda step_bh = null;
    TreeGen_ShootAgregation step_sa = null;
    TreeGen_BranchWidth step_bw = null;
    TreeGen_MeshGen step_mg = null;

    public void OnEnable()
    {
        step_ms = new TreeGen_MarkersSpawn();
        step_sc = new TreeGen_SpaceColonization();
        step_bh = new TreeGen_BorchertHonda();
        step_sa = new TreeGen_ShootAgregation();
        step_bw = new TreeGen_BranchWidth();
        step_mg = new TreeGen_MeshGen();
        pl = new TreeGen(
            step_sc, step_bh, step_sa, step_bw
        );
    }

    public override void OnInspectorGUI()
    {
        TreeData tree = target as TreeData;

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_ms.boundingBox = EditorGUILayout.Vector3Field("Bounding box", step_ms.boundingBox);
        pl.nb_it = EditorGUILayout.IntSlider("Number of iterations", pl.nb_it, TreeGen.NB_IT_MIN, TreeGen.NB_IT_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_ms.nb_markers = EditorGUILayout.IntSlider("Number of markers", step_ms.nb_markers, TreeGen_MarkersSpawn.NB_MARKERS_MIN, TreeGen_MarkersSpawn.NB_MARKERS_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_sc.theta_rad = EditorGUILayout.Slider("theta_rad", step_sc.theta_rad, TreeGen_SpaceColonization.THETA_MIN, TreeGen_SpaceColonization.THETA_MAX);
        step_sc.r = EditorGUILayout.Slider("r", step_sc.r, TreeGen_SpaceColonization.R_MIN, TreeGen_SpaceColonization.R_MAX);
        step_sc.phi = EditorGUILayout.Slider("phi", step_sc.phi, TreeGen_SpaceColonization.PHI_MIN, TreeGen_SpaceColonization.PHI_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_bh.lambda = EditorGUILayout.Slider("lambda", step_bh.lambda, TreeGen_BorchertHonda.LAMBDA_MIN, TreeGen_BorchertHonda.LAMBDA_MAX);
        step_bh.alpha = EditorGUILayout.Slider("alpha", step_bh.alpha, TreeGen_BorchertHonda.ALPHA_MIN, TreeGen_BorchertHonda.ALPHA_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_sa.epsilon = EditorGUILayout.Slider("epsilon", step_sa.epsilon, TreeGen_ShootAgregation.EPSILON_MIN, TreeGen_ShootAgregation.EPSILON_MAX);
        step_sa.eta = EditorGUILayout.Slider("eta", step_sa.eta, TreeGen_ShootAgregation.ETA_MIN, TreeGen_ShootAgregation.ETA_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_bw.initialDiameter = EditorGUILayout.Slider("initial diameter", step_bw.initialDiameter, TreeGen_BranchWidth.INITIAL_DIAMETER_MIN, TreeGen_BranchWidth.INITIAL_DIAMETER_MAX);
        step_bw.n = EditorGUILayout.Slider("n", step_bw.n, TreeGen_BranchWidth.N_MIN, TreeGen_BranchWidth.N_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---
        step_mg.subdivision_qty = EditorGUILayout.IntSlider("mesh subdivisions", step_mg.subdivision_qty, TreeGen_MeshGen.SUBDIVISION_MIN, TreeGen_MeshGen.SUBDIVISION_MAX);
        tree.mesh_file_name = EditorGUILayout.TextField("mesh file name", tree.mesh_file_name);

        if (GUILayout.Button("Generate tree")) {
            OnButtonClick();
        }
    }

    public void OnButtonClick()
    {
        SerializedObject s_tree = serializedObject;
        TreeData tree = (TreeData)target;

        step_ms.execute(ref tree);
        pl.execute(ref tree);
        step_mg.execute(ref tree);

        s_tree.ApplyModifiedProperties();

        string outputFilename = "Assets/Meshes/"+tree.mesh_file_name+".asset";
        AssetDatabase.CreateAsset(tree.mesh, outputFilename);

        TreeModelBehavior[] all_tmb = (TreeModelBehavior[])GameObject.FindObjectsOfType(typeof(TreeModelBehavior));
        foreach(TreeModelBehavior tmb in all_tmb)
        {
            GameObject gameObj = tmb.gameObject;
            if(tmb.treeModel == tree)
            {
                MeshFilter meshFilter = gameObj.GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = gameObj.GetComponent<MeshRenderer>();

                // reset -------
                if (!meshFilter)
                    meshFilter = gameObj.AddComponent<MeshFilter>();
                else if (meshFilter && meshFilter.sharedMesh)
                    DestroyImmediate(meshFilter.sharedMesh, true);

                if (!meshRenderer)
                    meshRenderer = gameObj.AddComponent<MeshRenderer>();
                // TODO : remove leaves into the scene (child gameobjects)
                // -------------
                
                meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(outputFilename);
            }
        }
    }
}
