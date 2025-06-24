using UnityEngine;

public class AIPatrolState : AIState
{
    private int currentWaypointIndex = 0;

    public AIPatrolState(AIController ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        // Mover al siguiente waypoint
        Vector3 targetWaypoint = ai.waypoints[currentWaypointIndex].position;
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, targetWaypoint, ai.moveSpeed * Time.deltaTime);

        // Si llega al waypoint, cambia al siguiente
        if (Vector3.Distance(ai.transform.position, targetWaypoint) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % ai.waypoints.Length;
        }

        // Verificaci�n para cambiar de estado seg�n la detecci�n del jugador
        if (AIDetection.PlayerInSight(ai.player, ai.transform.position, 10f))  // Distancia de visi�n
        {
            ai.ChangeState(new AIAttackState(ai));
        }
    }

    public override void Exit() { }
}
