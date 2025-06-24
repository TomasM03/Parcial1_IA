using UnityEngine;

public class AIFleeState : AIState
{
    public AIFleeState(AIController ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        // Huye del jugador
        Vector3 fleeDirection = ai.transform.position - ai.player.position;
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, ai.transform.position + fleeDirection, ai.moveSpeed * Time.deltaTime);

        // Si se aleja lo suficiente, regresa al patrullaje
        if (Vector3.Distance(ai.transform.position, ai.player.position) > 10f)
        {
            ai.ChangeState(new AIPatrolState(ai));
        }
    }

    public override void Exit() { }
}
