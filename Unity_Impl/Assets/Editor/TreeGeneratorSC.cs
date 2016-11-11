using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorSC : TreePipelineComponent
{
    public const float THETA_MIN = 60;
    public const float THETA_MAX = 120;
    public const float R_MIN = 4;
    public const float R_MAX = 6;
    public const float PHI_MIN = 1.9f;
    public const float PHI_MAX = 2.1f;

    public float r;
    public float phi;
    private float theta_rad; // radian
    private float cos_theta;

    public float theta // degree
    {
        get { return (float)(180 * theta_rad / Math.PI); }
        set {
            theta_rad = (float)(Math.PI * value / 180f);
            cos_theta = (float)Math.Cos(theta_rad);
        }
    }

    public TreeGeneratorSC() {
        theta = 90;
        r = 5;
        phi = 0.2f;
    }

    public void execute(TreeModel tree)
    {
        foreach (Node<Bud> node in tree.skeleton.leaves)
        {
            Bud bud = node.value;
            List<Vector3> directions = new List<Vector3>();

            for (int i = (tree.markers.Count - 1); i >= 0; i--)
            {
                Vector3 marker = tree.markers[i];
                Vector3 vecBetween = (marker - bud.pos);
                float distanceBetween = vecBetween.magnitude;

                if (distanceBetween <= phi) { // in bud sphere
                    tree.markers.RemoveAt(i);
                }
                else if(distanceBetween <= r) // maybe in bud cone
                {
                    Vector3 dirToMarker = Vector3.Normalize(vecBetween);
                    float angleCosBetween = Vector3.Dot(dirToMarker, bud.dir);
                    if (angleCosBetween > cos_theta) { // in bud cone
                        bud.targetMarkers.Add(marker);
                        directions.Add(dirToMarker);
                    }
                }
            }

            // optimal growth = average of directions to markers which are inside the bud cone
            Vector3 sum = new Vector3(0, 0, 0);
            foreach (Vector3 dir in directions)
                sum += dir;
            bud.optimalGrowth = sum / directions.Count;
        }
    }
}