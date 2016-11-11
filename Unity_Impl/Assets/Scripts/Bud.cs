using UnityEngine;
using System.Collections.Generic;

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
    public bool isNewAxis;
    public BudState state;

    public Bud(Vector3 position, bool isNewAxis_ = false) {
        isNewAxis = isNewAxis_;
        this.pos = position;
        targetMarkers = new List<Vector3>();
        dir = new Vector3(0, 1, 0);
        state = BudState.DORMANT;
    }
}
