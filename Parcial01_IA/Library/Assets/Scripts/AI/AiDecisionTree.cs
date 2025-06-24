using UnityEngine;

public class AIDecisionTree
{
    private readonly AIController ai;

    public AIDecisionTree(AIController ai)
    {
        this.ai = ai;
    }

    public AlertMode Evaluate()
    {
        if (!AIDetection.PlayerInSight(ai.player, ai.transform.position, 10f))
        {
            return AlertMode.None;
        }

        float randomValue = Random.value;

        if (ai is AI2Controller)
        {
            if (randomValue < 0.7f)
                return AlertMode.Flee;
            else
                return AlertMode.Attack;
        }
        else
        {
            if (randomValue < 0.8f)
                return AlertMode.Attack;
            else
                return AlertMode.Flee;
        }
    }
}
