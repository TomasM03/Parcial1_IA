using UnityEngine;

public class AIFleeState : AIState
{
    public AIFleeState(AIController ai) : base(ai) { }

    public override void Enter()
    {
        ai.moveSpeed = ai.actionSpeed;
    }

    public override void Update()
    {
        if (ai.alertMode != AlertMode.Flee)
        {
            ai.ChangeState(new AIPatrolState(ai));
            return;
        }

        Vector3 fleeDirection = (ai.transform.position - ai.player.position).normalized;

        if (ai.player.TryGetComponent<Rigidbody>(out var playerRb))
        {
            float predictionTime = 1f;
            Vector3 futurePlayerPos = ai.player.position + playerRb.linearVelocity * predictionTime;
            fleeDirection = (ai.transform.position - futurePlayerPos).normalized;
        }

        float avoidDistance = 1f;
        float avoidStrength = 1f;

        Vector3 moveDir = AIMovementHelper.GetAvoidanceDirection(ai.transform, fleeDirection, avoidDistance, avoidStrength);

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        ai.transform.position += ai.moveSpeed * Time.deltaTime * moveDir;
    }

    public override void Exit()
    {
        ai.moveSpeed = ai.patrolSpeed;
    }
}
