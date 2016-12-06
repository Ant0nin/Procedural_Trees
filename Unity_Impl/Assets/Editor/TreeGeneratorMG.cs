using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorMG : TreePipelineComponent
{
    public const int SUBDIVISION_MIN = 5;
    public const int SUBDIVISION_MAX = 30;
    public int subdivision_qty = 15;

    public TreeGeneratorMG() { }

    public void execute(ref TreeModel tree)
    {
        generateMesh(ref tree); // TODO

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
        foreach (List<Node<Bud>> l in tree.skeleton.levels)
        {
            for(int i=0; i<l.Count; i++)
            {
                Node<Bud> node = l[i];
                circleSampling(ref node, true); // main axis
                circleSampling(ref node, false); // lateral branchs

            }
        }

        /*Node<Bud> node = tree.skeleton.root;
        circleSampling(ref node, false); // main
        //circleSampling(ref node, true);*/
        
        return null; // TODO
    }

    void circleSampling(ref Node<Bud> node, bool lateralMode)
    {
        Bud currentBud = node.value;
        
        Vector3 axis = Vector3.up;
        if (!node.isRoot())
        {
            if (lateralMode && currentBud.lateralDir != null)
                axis = currentBud.lateralDir;
            else
            {
                Vector3 previousBudPos = node.parent.value.pos;
                Vector3 currentBudPos = currentBud.pos;
                axis = Vector3.Normalize(previousBudPos - currentBudPos);
            }
        }

        Vector3 side = Quaternion.Euler(90, 0, 90) * axis;

        Vector3 budPos = currentBud.pos;
        float angle = 0f;
        float stepAngle = 360f / (float)subdivision_qty;
        Vector3 normale = side;

        if(lateralMode) {
            currentBud.lateralVertices = new List<Vector3>();
            currentBud.lateralNormales = new List<Vector3>();
        }
        else {
            currentBud.mainVertices = new List<Vector3>();
            currentBud.mainNormales = new List<Vector3>();
        }

        for (int i = 0; i < subdivision_qty; i++)
        {
            angle += stepAngle;
            //Quaternion lookRotation = Quaternion.LookRotation(Vector3.left);
            normale = Quaternion.AngleAxis(stepAngle, axis) * normale;//* side;
            Vector3 pt = budPos + normale * node.value.branchWidth;

            if(lateralMode)
            {
                currentBud.lateralVertices.Add(pt);
                currentBud.lateralNormales.Add(normale);
            }
            else
            {
                currentBud.mainVertices.Add(pt);
                currentBud.mainNormales.Add(normale);
            }
        }
    }
}
