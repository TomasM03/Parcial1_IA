using System.Collections.Generic;
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
        float detectionDistance = 10f;
        float fieldOfViewAngle = 60f;

        if (!AIDetection.PlayerInSight(ai.player, ai.transform, detectionDistance, fieldOfViewAngle))
        {
            return AlertMode.None;
        }

        float fatigue = ai.fatigue;
        float distanceToPlayer = Vector3.Distance(ai.transform.position, ai.player.position);
        float maxDistance = 15f;

        float attackWeight = 1.5f; // mucho más alto de base
        if (distanceToPlayer < maxDistance * 0.6f) attackWeight += 0.3f;

        float fleeWeight = 0f;
        if (fatigue <= 3f) fleeWeight += 0.4f;
        if (fatigue <= 1f) fleeWeight += 0.6f;

        float noneWeight = 0.2f;
        if (distanceToPlayer > maxDistance) noneWeight += 0.5f;

        var options = new List<(AlertMode option, float weight)>
    {
        (AlertMode.Attack, attackWeight),
        (AlertMode.Flee, fleeWeight),
        (AlertMode.None, noneWeight)
    };

        return RouletteWheelSelector.Select(options);
    }

}
