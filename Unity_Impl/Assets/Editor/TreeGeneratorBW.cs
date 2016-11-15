using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorBW : TreePipelineComponent
{
    public float n;
    public float initialDiameter;

    public TreeGeneratorBW()
    {
        n = 2f;
        initialDiameter = 1f;
    }

    public void execute(ref TreeModel tree)
    {
        for(int i = tree.skeleton.levels.Count; i >= 0; i--)
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
