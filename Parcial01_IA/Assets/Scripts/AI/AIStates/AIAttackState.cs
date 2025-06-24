using UnityEngine;
using UnityEngine.SceneManagement;

public class AIAttackState : AIState
{
    public AIAttackState(AIController ai) : base(ai) { }

    public override void Enter()
    {
        ai.moveSpeed = ai.actionSpeed;
    }

    public override void Update()
    {
        if (ai.alertMode != AlertMode.Attack)
        {
            ai.ChangeState(new AIPatrolState(ai));
            return;
        }

        // ?? Fatiga disminuye solo mientras ataca
        ai.fatigue = Mathf.Max(1f, ai.fatigue - 0.1f * Time.deltaTime);

        Vector3 targetPosition = ai.player.position;

        if (ai.player.TryGetComponent<Rigidbody>(out var playerRb))
        {
            float predictionTime = 1f;
            targetPosition += playerRb.linearVelocity * predictionTime;
        }

        Vector3 direction = (targetPosition - ai.transform.position).normalized;

        float avoidDistance = 1f;
        float avoidStrength = 1f;

        // Movimiento básico con avoidance
        Vector3 moveDir = AIMovementHelper.GetAvoidanceDirection(ai.transform, direction, avoidDistance, avoidStrength);

        // 🔥 Agregamos flocking force
        Vector3 flockingForce = ai.CalculateFlockingForce();
        moveDir = (moveDir + flockingForce).normalized;

        // Movimiento final
        ai.transform.position += ai.moveSpeed * Time.deltaTime * moveDir;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(ai.transform.position, ai.player.position) < 1.5f)
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }

    public override void Exit()
    {
        ai.moveSpeed = ai.patrolSpeed;
    }
}
