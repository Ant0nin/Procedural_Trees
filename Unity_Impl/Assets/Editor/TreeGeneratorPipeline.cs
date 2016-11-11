using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TreeGeneratorPipeline
{
    public int nb_it;
    public const int NB_IT_MIN = 1;
    public const int NB_IT_MAX = 100;

    private List<TreePipelineComponent> m_steps;

    public TreeGeneratorPipeline(params TreePipelineComponent[] steps_)
    {
        m_steps = new List<TreePipelineComponent>();
        foreach (TreePipelineComponent step in steps_)
            m_steps.Add(step);
    }

    public Mesh execute(TreeModel tree) {

        Vector3 boundingBox = tree.boundingBox;
        tree = ScriptableObject.CreateInstance<TreeModel>(); // Reset

        foreach (TreePipelineComponent step in m_steps)
            step.execute(tree);

        // just a simple test => create a quad : ----
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        vertices[3] = new Vector3(1, 1, 0);
        mesh.vertices = vertices;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;
        mesh.triangles = triangles;

        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        mesh.uv = uv;
        // ------------------------------------------

        return mesh;
    }

}
