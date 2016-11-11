using UnityEngine;
using System.Collections.Generic;

public class Bud
{
    public Vector3 pos;
    public Vector3 dir;
    public List<Vector3> targetMarkers;
    public Vector3 optimalGrowth;

    public Bud(Vector3 position) {
        this.pos = position;
        targetMarkers = new List<Vector3>();
        dir = new Vector3(0, 1, 0);
    }
}
