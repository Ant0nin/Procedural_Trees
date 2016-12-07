using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorBW : TreePipelineComponent
{
    public const float INITIAL_DIAMETER_MIN = 0.001f;
    public const float INITIAL_DIAMETER_MAX = 0.003f;
    public const float N_MIN = 0.980f;
    public const float N_MAX = 1.020f;

    public float n = 0.99f;
    public float initialDiameter = 0.002f;

    public void execute(ref TreeModel tree)
    {
        for(int i = tree.skeleton.levels.Count - 1; i >= 0; i--)
        {
            List<Node<Bud>> list = tree.skeleton.levels[i];
            for(int j = 0; j < list.Count; j++)
            {
                Node<Bud> node = list[j];
                updateBranchWidth(ref node);
            }
        }
    }

    private void updateBranchWidth(ref Node<Bud> baseNode)
    {
        Bud currentBud = baseNode.value;

        if (baseNode.isLeaf())
            currentBud.branchWidth = initialDiameter;
        else
        {
            float lateralBudWidth = (baseNode.lateral != null ? baseNode.lateral.value.branchWidth : 0f);
            float mainBudWidth = (baseNode.main != null ? baseNode.main.value.branchWidth : 0f);
            currentBud.branchWidth = (float)(Math.Pow(lateralBudWidth, this.n) + Math.Pow(mainBudWidth, this.n));
        }

    }
}
