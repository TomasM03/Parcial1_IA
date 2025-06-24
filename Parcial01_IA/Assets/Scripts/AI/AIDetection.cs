using UnityEngine;

public static class AIDetection
{
    public static bool PlayerInSight(Transform player, Transform npcTransform, float detectionDistance, float fieldOfViewAngle)
    {
        // Direccion en la que est� mirando el NPC
        Vector3 directionToPlayer = player.position - npcTransform.position;
        float distance = directionToPlayer.magnitude;

        // Comprobar si el jugador est� dentro del rango de detecci�n
        if (distance <= detectionDistance)
        {
            // Calculamos el �ngulo entre la direcci�n del NPC y la direcci�n hacia el jugador
            float angle = Vector3.Angle(npcTransform.forward, directionToPlayer);

            // Si el jugador est� dentro del �ngulo de visi�n del NPC
            if (angle <= fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                // Lanzamos un rayo desde la posici�n del NPC en la direcci�n en que mira
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
