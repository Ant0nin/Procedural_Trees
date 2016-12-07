using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorMG : TreePipelineComponent
{
    public const int SUBDIVISION_MIN = 5;
    public const int SUBDIVISION_MAX = 8;
    public int subdivision_qty = 8;

    public void execute(ref TreeModel tree)
    {
        tree.mesh = generateMesh(ref tree);
    }

    private Mesh generateMesh(ref TreeModel tree)
    {
        // Prepare circles foreach node
        foreach (List<Node<Bud>> l in tree.skeleton.levels)
        {
            for(int i=0; i<l.Count; i++)
            {
                Node<Bud> node = l[i];
                circleSampling(ref node, true); // main axis
                circleSampling(ref node, false); // lateral branchs
            }
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> normales = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        int countTri = 0;

        // Build strips
        for(int i = 0; i < (tree.skeleton.levels.Count-1); i++)
        {
            List<Node<Bud>> l = tree.skeleton.levels[i];
            for (int j = 0; j < l.Count; j++)
            {
                Node<Bud> node = l[j];
                buildStrip(ref node, ref vertices, ref triangles, ref normales, ref uv, ref countTri, false); // main axis
                buildStrip(ref node, ref vertices, ref triangles, ref normales, ref uv, ref countTri, true); // lateral branchs
            }
        }

        // Give data to mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normales.ToArray();
        mesh.uv = uv.ToArray();
        
        return mesh; 
    }
    
    void buildStrip(
        ref Node<Bud> src, 
        ref List<Vector3> vertices, 
        ref List<int> triangles, 
        ref List<Vector3> normales, 
        ref List<Vector2> uv, 
        ref int countTri,
        bool lateralMode)
    {
        Node<Bud> target = null;
        List<Vector3> srcVertices = null;
        List<Vector3> srcNormales = null;
        List<Vector3> targetVertices = null;
        List<Vector3> targetNormales = null;

        if (lateralMode && src.lateral != null)
        {
            target = src.lateral;
            srcVertices = src.value.lateralVertices;
            srcNormales = src.value.lateralNormales;
            targetVertices = target.value.lateralVertices;
            targetNormales = target.value.lateralNormales;
        }
        else if (src.main != null)
        {
            target = src.main;
            srcVertices = src.value.mainVertices;
            srcNormales = src.value.mainNormales;
            targetVertices = target.value.mainVertices;
            targetNormales = target.value.mainNormales;
        }
        else
            return;

        for(int i=0; i<srcVertices.Count; i++)
        {
            int k = (i == 0 ? (srcVertices.Count - 1) : (i - 1));

            Vector3 first_srcPoint = srcVertices[k];
            Vector3 first_targetPoint = targetVertices[k];
            Vector3 second_srcPoint = srcVertices[i];
            Vector3 second_targetPoint = targetVertices[i];

            Vector3 first_srcNormale = srcNormales[k];
            Vector3 first_targetNormale = targetNormales[k];
            Vector3 second_srcNormale = srcNormales[i];
            Vector3 second_targetNormale = targetNormales[i];

            vertices.Add(first_srcPoint);
            vertices.Add(second_srcPoint);
            vertices.Add(first_targetPoint);
            vertices.Add(second_targetPoint);

            normales.Add(first_srcNormale);
            normales.Add(second_srcNormale);
            normales.Add(first_targetNormale);
            normales.Add(second_targetNormale);

            uv.Add(new Vector2(0, 0));
            uv.Add(new Vector2(1, 0));
            uv.Add(new Vector2(0, 1));
            uv.Add(new Vector2(1, 1));

            triangles.Add(countTri + 0);
            triangles.Add(countTri + 2);
            triangles.Add(countTri + 1);

            triangles.Add(countTri + 2);
            triangles.Add(countTri + 3);
            triangles.Add(countTri + 1);

            countTri +=4;
        }
    }

    void circleSampling(ref Node<Bud> node, bool lateralMode)
    {
        Bud currentBud = node.value;
        
        Vector3 axis = Vector3.down;
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
            normale = Quaternion.AngleAxis(stepAngle, axis) * normale;
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
