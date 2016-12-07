using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TreeGen
{
    public int nb_it = 8;
    public const int NB_IT_MIN = 5;
    public const int NB_IT_MAX = 10;

    private List<ITreeGenComp> m_steps;

    public TreeGen(params ITreeGenComp[] steps_)
    {
        m_steps = new List<ITreeGenComp>();
        foreach (ITreeGenComp step in steps_)
            m_steps.Add(step);
    }

    public void execute(ref TreeData tree)
    {
        for(int i = 0; i < nb_it; i++)
            foreach (ITreeGenComp step in m_steps)
                step.execute(ref tree);
    }

}
