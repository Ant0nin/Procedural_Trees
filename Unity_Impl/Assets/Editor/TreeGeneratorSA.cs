using UnityEngine;
using System.Collections;
using System;

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
        foreach (Node<Bud> node in tree.skeleton.leaves)
        {
            Bud bud = node.value;
            switch (bud.state)
            {
                case BudState.NEW_METAMER:
                    addMetamer(tree, node, true);
                    break;
                case BudState.FLOWER:
                    // TODO : Remove from leaves
                    break;
                case BudState.DORMANT:
                    addMetamer(tree, node, false);
                    break;
                case BudState.ABORT:
                    // TODO : Remove from leaves
                    break;
            }
        }
    }

    private void addMetamer(TreeModel tree, Node<Bud> currentNode, bool isNewAxis)
    {
        Bud currentBud = currentNode.value;

        Vector3 finalDirection = Vector3.Normalize(eta * tropismVec + epsilon * currentBud.optimalGrowth + currentBud.dir);
        float internodeLength = currentBud.l;
        Vector3 newBudPosition = currentBud.pos + finalDirection * internodeLength;

        Bud newBud = new Bud(newBudPosition, isNewAxis);
        Node<Bud> newNode = new Node<Bud>(currentNode, newBud);

        // TODO : lateral and main shouldn't be null
        if (isNewAxis)
            currentNode.lateral = newNode;
        else
            currentNode.main = newNode;

        tree.skeleton.leaves.Remove(currentNode);
        tree.skeleton.leaves.Add(newNode);
    }
}
