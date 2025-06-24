using UnityEngine;

public static class AIDetection
{
    public static bool PlayerInSight(Transform player, Transform npcTransform, float detectionDistance, float fieldOfViewAngle)
    {
        Vector3 directionToPlayer = player.position - npcTransform.position;
        float distance = directionToPlayer.magnitude;

        if (distance <= detectionDistance)
        {
            float angle = Vector3.Angle(npcTransform.forward, directionToPlayer);

            if (angle <= fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(npcTransform.position, directionToPlayer.normalized, out hit, detectionDistance))
                {
                    // Si el rayo golpea al jugador
                    if (hit.transform == player)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
