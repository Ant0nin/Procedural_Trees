using UnityEngine;
using System.Collections;
using System;

public class TreeGeneratorBH : TreePipelineComponent
{
    public float lambda;
    public float alpha;

    public float Q = 10;

    //Accumule la lumière recue par les bourgeons de l'arbre à la base de celui ci.
    private float accumulateLight(Node<Bud> N)
    {
        float lightQtt = 0;
        foreach(Node<Bud> child in N.childs)
        {
            if (!child.isLeaf())
                lightQtt += accumulateLight(child);
            else
                lightQtt += Q;
        }
        N.Q = lightQtt;
        return lightQtt;
    }

    public void execute(TreeModel tree)
    {
        //Première passe
        float vBase = alpha * accumulateLight(tree.skeleton.root);

        throw new NotImplementedException();
    }
}
