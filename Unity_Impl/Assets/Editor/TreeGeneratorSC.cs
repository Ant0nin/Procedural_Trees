using UnityEngine;
using System.Collections;
using System;

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
        phi = 2;
    }

    public void execute(TreeModel tree)
    {
        foreach (Node<Bud> node in tree.skeleton.leaves)
        {
            foreach (Vector3 marker in tree.markers)
            {
                Bud bud = node.value;
                Vector3 vecBetween = (marker - bud.pos);
                float distanceBetween = vecBetween.magnitude;

                if (distanceBetween <= phi) { // in bud sphere
                    tree.markers.Remove(marker);
                }
                else if(distanceBetween <= r) // maybe in bud cone
                {
                    Vector3 dirToMarker = Vector3.Normalize(vecBetween);
                    float angleCosBetween = Vector3.Dot(dirToMarker, bud.dir);
                    if (angleCosBetween > cos_theta) { // in bud cone
                        // ...
                    }
                }
            }
        }
    }
}