using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(AIController ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        if (ai.alertMode != AlertMode.Attack)
        {
            ai.ChangeState(new AIPatrolState(ai));
            return;
        }

        Vector3 targetPosition = ai.player.position;

        if (ai.player.TryGetComponent<Rigidbody>(out var playerRb))
        {
            float predictionTime = 1f;
            targetPosition += playerRb.linearVelocity * predictionTime;
        }

        Vector3 direction = (targetPosition - ai.transform.position).normalized;

        float avoidDistance = 1f;
        float avoidStrength = 1f;

        Vector3 moveDir = AIMovementHelper.GetAvoidanceDirection(ai.transform, direction, avoidDistance, avoidStrength);
        ai.transform.position += ai.moveSpeed * Time.deltaTime * moveDir;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(ai.transform.position, ai.player.position) < 1.5f)
        {
            Debug.Log($"{ai.name} está atacando al jugador.");
        }
    }

    public override void Exit() { }
}
