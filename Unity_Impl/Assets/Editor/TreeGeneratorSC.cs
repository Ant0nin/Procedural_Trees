using UnityEngine;
using System.Collections;
using System;

public class TreeGeneratorSC : TreePipelineComponent {

    public float theta;
    public float r;
    public float phi;
    public const float THETA_MIN = 60;
    public const float THETA_MAX = 120;
    public const float R_MIN = 4;
    public const float R_MAX = 6;
    public const float PHI_MIN = 1.9f;
    public const float PHI_MAX = 2.1f;

    public TreeGeneratorSC() {
        theta = 90;
        r = 5;
        phi = 2;
    }

    public void execute(TreeModel tree)
    {
        throw new NotImplementedException();
    }
}
