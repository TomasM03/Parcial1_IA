using UnityEngine;

public static class AIDetection
{
    public static bool PlayerInSight(Transform player, Transform npcTransform, float detectionDistance, float fieldOfViewAngle)
    {
        // Direccion en la que está mirando el NPC
        Vector3 directionToPlayer = player.position - npcTransform.position;
        float distance = directionToPlayer.magnitude;

        // Comprobar si el jugador está dentro del rango de detección
        if (distance <= detectionDistance)
        {
            // Calculamos el ángulo entre la dirección del NPC y la dirección hacia el jugador
            float angle = Vector3.Angle(npcTransform.forward, directionToPlayer);

            // Si el jugador está dentro del ángulo de visión del NPC
            if (angle <= fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                // Lanzamos un rayo desde la posición del NPC en la dirección en que mira
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
