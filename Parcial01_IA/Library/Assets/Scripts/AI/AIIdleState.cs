using UnityEngine;

public class AIIdleState : AIState
{
    private readonly float idleTime = 2f;
    private float timer = 0f;

    public AIIdleState(AIController ai) : base(ai) { }

    public override void Enter()
    {
        Debug.Log(ai.name + " entró en estado Idle.");
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (AIDetection.PlayerInSight(ai.player, ai.transform.position, 10f))
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
        Debug.Log(ai.name + " salió del estado Idle.");
    }
}
