using UnityEngine;
using System.Collections;
using System;

public class TreeGeneratorBW : TreePipelineComponent
{
    public float n;
    public float initialDiameter;

    public TreeGeneratorBW()
    {
        n = 2f;
        initialDiameter = 1f;
    }

    public void execute(TreeModel tree)
    {
        Node<Bud> rootNode = tree.skeleton.root;
        foreach (Node<Bud> leaf in tree.skeleton.leaves) {
            Bud bud = leaf.value;
            bud.branchWidth = initialDiameter;
        }
        // TODO : parcours du graph-tree en largeur, des feuilles jusqu'à la base
    }

    private void updateBranchWidth(Node<Bud> baseNode)
    {
        if (baseNode.isLeaf())
            return;

        Bud currentBud = baseNode.value;
        float lateralBudWidth = baseNode.lateral.value.branchWidth;
        float mainBudWidth = baseNode.main.value.branchWidth;
        currentBud.branchWidth = (float)( Math.Pow(lateralBudWidth, this.n) + Math.Pow(mainBudWidth, this.n) );
    }
}
