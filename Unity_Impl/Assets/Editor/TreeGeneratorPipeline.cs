using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TreeGeneratorPipeline
{
    public int nb_it = 8;
    public const int NB_IT_MIN = 5;
    public const int NB_IT_MAX = 10;

    private List<TreePipelineComponent> m_steps;

    public TreeGeneratorPipeline(params TreePipelineComponent[] steps_)
    {
        m_steps = new List<TreePipelineComponent>();
        foreach (TreePipelineComponent step in steps_)
            m_steps.Add(step);
    }

    public void execute(ref TreeModel tree)
    {
        for(int i = 0; i < nb_it; i++)
            foreach (TreePipelineComponent step in m_steps)
                step.execute(ref tree);
    }

}
