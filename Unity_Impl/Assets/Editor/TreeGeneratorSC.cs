using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorSC : TreePipelineComponent
{
    public const float THETA_MIN = 1.0f;
    public const float THETA_MAX = 1.5f;
    public const float R_MIN = 4;
    public const float R_MAX = 6;
    public const float PHI_MIN = 0.2f;
    public const float PHI_MAX = 0.3f;

    public float r = 4f;
    public float phi = 0.25f;
    public float theta_rad = 1.3f; // radian

    public void execute(ref TreeModel tree)
    {
        float cos_theta = (float)Math.Cos(theta_rad);

        foreach (Node<Bud> node in tree.skeleton.leaves)
        {
            Bud bud = node.value;
            List<Vector3> directions = new List<Vector3>();

            for (int i = (tree.markers.Count - 1); i >= 0; i--)
            {
                Vector3 marker = tree.markers[i];
                Vector3 vecBetween = (marker - bud.pos);
                float distanceBetween = vecBetween.magnitude;

                if (distanceBetween <= phi) // in bud sphere
                { 
                    tree.markers.RemoveAt(i);
                }
            }

            foreach (Vector3 marker in tree.markers)
            {
                Vector3 vecBetween = (bud.pos - marker);
                float distanceBetween = vecBetween.magnitude;

                if(distanceBetween <= r) // maybe in bud cone
                {
                    Vector3 dirToMarker = Vector3.Normalize(vecBetween);
                    float angleCosBetween = Vector3.Dot(bud.dir, dirToMarker);
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