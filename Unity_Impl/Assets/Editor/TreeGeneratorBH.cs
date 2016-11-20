using UnityEngine;
using System.Collections;
using System;

public class TreeGeneratorBH : TreePipelineComponent
{
    public float lambda;
    public float alpha;

    public float Q = 10;

    //Accumule la lumière recue par les bourgeons de l'arbre dans chaque noeud puis à la base de celui ci.
    private float accumulateLight(ref Node<Bud> N) {
        float lightQtt = 0;

        if (N.isLeaf())
            lightQtt = Q;
        else {
            if (N.main!=null)
                lightQtt += accumulateLight(ref N.main);
            if (N.lateral!=null)
                lightQtt += accumulateLight(ref N.lateral);
        }

        N.value.Q = lightQtt;
        return lightQtt;
    }

    private void distributeEnergy(ref Node<Bud> N, float v) {
        if(N.lateral != null && N.main != null) {
            float Qm = N.main.value.Q;
            float Ql = N.lateral.value.Q;
            float d = (lambda * Qm) + (1 - lambda) * Ql;

            N.main.value.setEnergy(v * lambda * Qm / d);
            N.lateral.value.setEnergy(v * (1 - lambda) * Ql / d);
            if (N.main.value.v > 0) {
                N.main.value.state = BudState.NEW_METAMER;
            }
            if (N.lateral.value.v > 0) {
                N.lateral.value.state = BudState.NEW_METAMER;
            }

            distributeEnergy(ref N.main, N.main.value.v);
            distributeEnergy(ref N.lateral, N.lateral.value.v);

        } else if(N.lateral != null && N.lateral == null) { //Continuité de la branche -> transmission simple
            N.main.value.setEnergy(v); // TODO : runtime error here !
            distributeEnergy(ref N.main, N.main.value.v);
        }
    }

    public void execute(ref TreeModel tree)
    {
        //Première passe
        float vBase = alpha * accumulateLight(ref tree.skeleton.root);
        //Seconde passe
        distributeEnergy(ref tree.skeleton.root, vBase);
    }
}
