using UnityEngine;

public static class AIDetection
{
    public static bool PlayerInSight(Transform player, Vector3 npcPosition, float detectionRadius)
    {
        Vector3 directionToPlayer = player.position - npcPosition;
        float distance = directionToPlayer.magnitude;

        if (distance <= detectionRadius)
        {
            RaycastHit hit;
            if (Physics.Raycast(npcPosition, directionToPlayer.normalized, out hit, detectionRadius))
            {
                if (hit.transform == player)
                    return true;
            }
        }
        return false;
    }
}
