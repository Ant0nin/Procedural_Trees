using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections;

[CustomEditor(typeof(TreeModel))]
public class TreeGenerator : Editor
{
    public bool display_markers;
    public bool display_skeleton;
    public bool display_leaves;
    public bool display_texture;

    int    param_nb_it;
    const int NB_IT_MIN = 1;
    const int NB_IT_MAX = 100;

    // space colonization ------
    AnimBool show_space_colonization;
    int      param_nb_markers;
    float    param_theta;
    float    param_r;
    float    param_phi;
    const int NB_MARKERS_MIN = 10;
    const int NB_MARKERS_MAX = 1000;
    const float THETA_MIN = 60;
    const float THETA_MAX = 120;
    const float R_MIN = 4;
    const float R_MAX = 6;
    const float PHI_MIN = 1.9f;
    const float PHI_MAX = 2.1f;
    // -------------------------

    float   param_lambda;
    float   param_alpha;
    float   param_epsilon;
    float   param_eta;
    int    param_n;

    public override void OnInspectorGUI()
    {
        SerializedObject s_tree = serializedObject;
        TreeModel tree = (TreeModel)target;

        param_nb_it = EditorGUILayout.IntSlider("Number of iterations", param_nb_it, NB_IT_MIN, NB_IT_MAX);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider); // ---

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
    
    public void OnSceneGUI()
    {
       // TODO : Donner un aperçu de l'arbre dans la scène
    }

    public void OnButtonClick()
    {
        // TODO : (Re)Build tree model + refresh
    }
}
