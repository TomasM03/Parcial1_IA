using UnityEngine;

public class AIPatrolState : AIState
{
    private int currentWaypointIndex = 0;

    public AIPatrolState(AIController ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        if (ai.alertMode != AlertMode.None)
        {
            ai.ChangeState(ai.GetChaseState());
            return;
        }

        ai.fatigue = Mathf.Min(10, ai.fatigue + 0.1f * Time.deltaTime);

        Vector3 targetWaypoint = ai.waypoints[currentWaypointIndex].position;
        Vector3 dir = (targetWaypoint - ai.transform.position).normalized;

        Vector3 moveDir = AIMovementHelper.GetAvoidanceDirection(ai.transform, dir);

        Vector3 flockingForce = ai.CalculateFlockingForce();
        moveDir = (moveDir + flockingForce).normalized;

        ai.transform.position += moveDir * ai.moveSpeed * Time.deltaTime;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(ai.transform.position, targetWaypoint) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % ai.waypoints.Length;
        }
    }

    public override void Exit() { }
}
