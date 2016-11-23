using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TreeGeneratorBH : TreePipelineComponent
{
    public float lambda = 0.5f;
    public float alpha = 2f;
    public float Q_leaf = 1;

    // Evalue la lumière recue pour un bourgeon de l'arbre
    private void evaluateLight(ref Node<Bud> N)
    {
        Bud bud = N.value;
        Node<Bud> lateral = N.lateral;
        Node<Bud> main = N.main;

        if (N.isLeaf())
            bud.Q = Q_leaf;
        else
            bud.Q = (lateral != null ? lateral.value.Q : 0f) + (main != null ? main.value.Q : 0f);
    }

    // Evalue l'énergie pour les éventuelles branches adjacentes (main et lateral) du bourgeon traité
    private void evaluateEnergy(ref Node<Bud> N)
    {
        Bud bud = N.value;
        Node<Bud> lateral = N.lateral;
        Node<Bud> main = N.main;
        bool hasLateral = (lateral != null);
        bool hasMain = (main != null);
        float Ql = 0f;
        float Qm = 0f;

        if(hasLateral)
            Ql = lateral.value.Q;
        if(hasMain)
            Qm = main.value.Q;

        float d = (lambda * Qm) + (1 - lambda) * Ql;

        if (hasLateral)
        {
            float e = (bud.v * (1 - lambda) * Ql) / d;
            lateral.value.setEnergy(e);
        }
        if (hasMain)
        {
            float e = (bud.v * lambda * Qm) / d;
            main.value.setEnergy(e);
        }
    }

    private void evaluateState(ref Bud bud)
    {
        if (bud.v > 0)
            bud.state = BudState.NEW_METAMER;
    }

    // Accumule la lumière de l'arbre, des feuilles vers la base
    private void accumulateLight(ref TreeStructure<Bud> skeleton)
    {
        for (int i = skeleton.levels.Count - 1; i >= 0; i--)
        {
            List<Node<Bud>> list = skeleton.levels[i];
            for (int j = 0; j < list.Count; j++)
            {
                Node<Bud> node = list[j];
                evaluateLight(ref node);
            }
        }
    }

    // Accumule l'énergie de l'arbre, de la base vers les feuilles
    private void distributeEnergy(ref TreeStructure<Bud> skeleton)
    {
        // On évalue d'abord la quantité d'énergie à la base de l'arbre
        Bud rootBud = skeleton.root.value;
        float vBase = alpha * rootBud.Q;
        rootBud.setEnergy(vBase);

        for (int i = 0; i < skeleton.levels.Count ; i++)
        {
            List<Node<Bud>> list = skeleton.levels[i];
            for (int j = 0; j < list.Count; j++)
            {
                Node<Bud> node = list[j];
                Bud bud = node.value;
                evaluateState(ref bud);
                evaluateEnergy(ref node);
            }
        }
    }

    public void execute(ref TreeModel tree)
    {
        //Première passe
        accumulateLight(ref tree.skeleton);

        //Seconde passe
        distributeEnergy(ref tree.skeleton);
    }
}
