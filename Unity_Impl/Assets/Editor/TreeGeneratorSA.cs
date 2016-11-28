using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorSA : TreePipelineComponent
{
    public float epsilon = 0.9f;    // pour direction optimale
    public float eta = 0.0f;        // pour vecteur de tropisme
    private static Vector3 tropismVec = new Vector3(0f,-1f,0f);

    public void execute(ref TreeModel tree)
    {
        List<Node<Bud>> leaves = tree.skeleton.leaves;

        for (int i = leaves.Count-1; i >= 0; i--)
        {
            Node<Bud> node = leaves[i];
            Bud bud = node.value;

            switch (bud.state)
            {
                case BudState.NEW_METAMER:
                    growBranch(ref tree.skeleton, ref leaves, ref node);
                    addMetamer(ref tree.skeleton, ref leaves, ref node);
                    break;
                case BudState.DORMANT:
                    growBranch(ref tree.skeleton, ref leaves, ref node);
                    break;
            }

            leaves.Remove(node);
        }
    }

    private void addMetamer(ref TreeStructure<Bud> skeleton, ref List<Node<Bud>> leaves, ref Node<Bud> currentNode)
    {
        Bud currentBud = currentNode.value;
        float internodeLength = currentBud.l;

        Vector3 dir = Vector3.Normalize(eta * tropismVec + epsilon * currentBud.optimalGrowth + (1-eta-epsilon) * currentBud.dir);
        Vector3 newLateralBudPosition = currentBud.pos + dir * internodeLength;

        Bud newLateralBud = new Bud(newLateralBudPosition, true);
        Node<Bud> newLateralNode = new Node<Bud>(ref skeleton, currentNode, newLateralBud);

        currentNode.lateral = newLateralNode;
        leaves.Add(newLateralNode);
    }

    private void growBranch(ref TreeStructure<Bud> skeleton, ref List<Node<Bud>> leaves, ref Node<Bud> currentNode)
    {
        Bud currentBud = currentNode.value;
        Vector3 dir = currentBud.dir;
        float length = currentBud.l;
        int it_number = currentBud.n;

        Node<Bud> node = currentNode;

        for (int i = 1; i < it_number; i++)
        {
            Node<Bud> previousNode = node;
            Bud previousBud = previousNode.value;
            Vector3 position = previousBud.pos + dir * length;
            Bud bud = new Bud(position, false);
            node = new Node<Bud>(ref skeleton, previousNode, bud);
            previousNode.main = node;
        }

        leaves.Add(node);
    }
}
