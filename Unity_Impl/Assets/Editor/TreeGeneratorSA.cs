using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorSA : TreePipelineComponent
{
    public float epsilon;
    public float eta;
    private static Vector3 tropismVec = new Vector3(0,-1,0);

    public TreeGeneratorSA() {
        epsilon = 0.3f;
        eta = 0.3f;
    }

    public void execute(TreeModel tree)
    {
        List<Node<Bud>> leaves = tree.skeleton.leaves;

        for (int i = leaves.Count; i >= 0; i--)
        {
            Node<Bud> node = leaves[i];
            Bud bud = node.value;

            if (bud.state == BudState.NEW_METAMER)
                addMetamers(leaves, node);
            else
                leaves.RemoveAt(i);
        }
    }

    private void addMetamers(List<Node<Bud>> leaves, Node<Bud> currentNode)
    {
        Bud currentBud = currentNode.value;

        float internodeLength = currentBud.l;
        Vector3 newMainBudPosition = currentBud.pos + currentBud.dir * internodeLength;
        currentBud.dir = Vector3.Normalize(eta * tropismVec + epsilon * currentBud.optimalGrowth + currentBud.dir);
        Vector3 newLateralBudPosition = currentBud.pos + currentBud.dir * internodeLength;

        Bud newLateralBud = new Bud(newLateralBudPosition, true);
        Node<Bud> newLateralNode = new Node<Bud>(currentNode, newLateralBud);
        currentNode.lateral = newLateralNode;

        Bud newMainBud = new Bud(newMainBudPosition, true);
        Node<Bud> newMainNode = new Node<Bud>(currentNode, newMainBud);
        currentNode.main = newMainNode;

        leaves.Remove(currentNode);
        leaves.Add(newLateralNode);
        leaves.Add(newMainNode);
    }
}
