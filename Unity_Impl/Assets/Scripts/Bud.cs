using UnityEngine;
using System.Collections.Generic;
using System;

public enum BudState {
    NEW_METAMER,
    FLOWER,
    DORMANT,
    ABORT
}

public class Bud
{
    public Vector3 pos;
    public Vector3 dir;
    public List<Vector3> targetMarkers;
    public Vector3 optimalGrowth;
    public bool isNewAxis; // TODO : plus besoin car arbre binaire
    public BudState state;

    //Ext. BH
    public float Q; //Quantité de lumière
    public float v; //Valeur d'énergie
    public int n; //Nombre de métamers produit par le bourgeon
    public float l; //Longeur des internodes (= v/n)

    public Bud(Vector3 position, bool isNewAxis_) {
        isNewAxis = isNewAxis_;
        this.pos = position;
        targetMarkers = new List<Vector3>();
        dir = new Vector3(0, 1, 0);
        state = BudState.DORMANT;
    }

    public void setEnergy(float e)
    {
        v = e;
        n = (int)Math.Truncate(v);
        l = v / n;
    }
}
