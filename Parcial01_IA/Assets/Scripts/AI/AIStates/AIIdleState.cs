using UnityEngine;

public class AIIdleState : AIState
{
    private float idleTime = 2f;
    private float timer = 0f;

    public AIIdleState(AIController ai) : base(ai) { }

    public override void Enter()
    {
        Debug.Log(ai.name + " entr� en estado Idle.");
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        float detectionDistance = 10f;
        float fieldOfViewAngle = 60f;
        if (AIDetection.PlayerInSight(ai.player, ai.transform, detectionDistance, fieldOfViewAngle))
        {
            if (ai is AI1Controller)
                ai.ChangeState(new AIAttackState(ai));
            else if (ai is AI2Controller)
                ai.ChangeState(new AIFleeState(ai));
        }

        if (timer >= idleTime)
        {
            ai.ChangeState(new AIPatrolState(ai));
        }
    }

    public override void Exit()
    {
        Debug.Log(ai.name + " sali� del estado Idle.");
    }
}
