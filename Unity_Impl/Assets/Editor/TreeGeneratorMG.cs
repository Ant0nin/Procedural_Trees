using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorMG : TreePipelineComponent
{
    public int SUBDIVISION_MIN = 5;
    public int SUBDIVISION_MAX = 20;
    public int subdivision_qty = 5;

    private List<Vector3> vertices;
    private List<Vector3> normales;

    public TreeGeneratorMG() { }

    public void execute(ref TreeModel tree)
    {
        //Mesh mesh = generateMesh(ref tree);
        //circleSampling(ref tree, tree.skeleton.root);

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

        tree.mesh = mesh;
    }

    private Mesh generateMesh(ref TreeModel tree)
    {
        return null; // TODO
    }

    void circleSampling(ref TreeModel tree, Node<Bud> node)
    {
        if (node.isRoot())
        {
            Vector3 vec1 = Vector3.left;
            Vector3 axis = node.value.dir;
            Vector3 dir = Vector3.Cross(vec1, axis);
            Vector3 budPos = node.value.pos;
            float angle = 0f;
            float stepAngle = 360f / (float)subdivision_qty;

            for(int i = 0; i < subdivision_qty; i++)
            {
                angle+= stepAngle;
                Vector3 normale = Quaternion.Euler(0, angle, 0) * dir;
                Vector3 pt = budPos + normale * node.value.branchWidth;

                vertices.Add(pt);
                normales.Add(normale);
            }
        }
        else if(node.isLeaf())
        {

        }
        else
        {

        }
    }
}
