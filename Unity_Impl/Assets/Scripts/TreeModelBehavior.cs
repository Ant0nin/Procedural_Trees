using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeModelBehavior : MonoBehaviour
{
    public TreeModel treeModel;

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        drawNodeCircles(Color.red);
        drawMarkers(Color.yellow);
        drawSkeleton(Color.cyan);
        drawBoundingBox(Color.blue);
    }
    
    void drawNodeCircles(Color c)
    {
        Gizmos.color = c;

        if (treeModel.skeleton != null)
        {
            foreach (List<Node<Bud>> level in treeModel.skeleton.levels)
            {
                foreach (Node<Bud> node in level)
                {
                    Bud bud = node.value;
                    if (bud.mainVertices != null)
                    {
                        Vector3 position_start, position_end;
                        position_start = position_end = new Vector3();
                        for (int i = 1; i < bud.mainVertices.Count; i++)
                        {
                            position_start = transform.position + bud.mainVertices[i - 1];
                            position_end = transform.position + bud.mainVertices[i];
                            Gizmos.DrawLine(position_start, position_end);
                        }
                        position_start = transform.position + bud.mainVertices[bud.mainVertices.Count - 1];
                        position_end = transform.position + bud.mainVertices[0];
                        Gizmos.DrawLine(position_start, position_end);
                    }
                }
            }
        }

    }

    void drawMarkers(Color c)
    {
        Gizmos.color = c;
        foreach (Vector3 marker in treeModel.markers)
        {
            Gizmos.DrawSphere(transform.position + marker, 0.02f);
        }
    }

    void drawBoundingBox(Color c)
    {
        Gizmos.color = c;
        
        Vector3 pos = transform.position;
        Vector3 box = pos + treeModel.boundingBox;

        Vector3 front_top_left =        new Vector3(pos.x, box.y, pos.z);
        Vector3 front_top_right =       new Vector3(box.x, box.y, pos.z);
        Vector3 front_bottom_left =     new Vector3(pos.x, pos.y, pos.z);
        Vector3 front_bottom_right =    new Vector3(box.x, pos.y, pos.z);

        Vector3 back_top_left =         new Vector3(pos.x, box.y, box.z);
        Vector3 back_top_right =        new Vector3(box.x, box.y, box.z);
        Vector3 back_bottom_left =      new Vector3(pos.x, pos.y, box.z);
        Vector3 back_bottom_right =     new Vector3(box.x, pos.y, box.z);

        Gizmos.DrawLine(front_top_left, front_top_right);
        Gizmos.DrawLine(front_top_right, front_bottom_right);
        Gizmos.DrawLine(front_bottom_right, front_bottom_left);
        Gizmos.DrawLine(front_bottom_left, front_top_left);

        Gizmos.DrawLine(back_top_left, back_top_right);
        Gizmos.DrawLine(back_top_right, back_bottom_right);
        Gizmos.DrawLine(back_bottom_right, back_bottom_left);
        Gizmos.DrawLine(back_bottom_left, back_top_left);

        Gizmos.DrawLine(front_top_left, back_top_left);
        Gizmos.DrawLine(front_top_right, back_top_right);
        Gizmos.DrawLine(front_bottom_left, back_bottom_left);
        Gizmos.DrawLine(front_bottom_right, back_bottom_right);
    }

    void drawSkeleton(Color c)
    {
        Gizmos.color = c;
        TreeStructure<Bud> skeleton = treeModel.skeleton;

        if (skeleton == null)
            return;

        for (int i = 1; i < skeleton.levels.Count; i++) // la root est exclue car i initialisé à 1
        {
            List<Node<Bud>> list = skeleton.levels[i];
            for (int j = 0; j < list.Count; j++)
            {
                Node<Bud> node = list[j];
                drawBranch(node);
            }
        }

    }


    void drawBranch(Node<Bud> node)
    {
        Vector3 startPos = node.value.pos;
        Vector3 endPos = node.parent.value.pos;

        Vector3 position_start = startPos + transform.position;
        Vector3 position_end = endPos + transform.position;

        Gizmos.DrawLine(position_start, position_end);
    }
#endif
}
