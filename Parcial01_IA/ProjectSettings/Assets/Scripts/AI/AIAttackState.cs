using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(AIController ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        // Perseguir al jugador
        Vector3 targetPosition = ai.player.position;
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, targetPosition, ai.moveSpeed * Time.deltaTime);

        // Si está lo suficientemente cerca, ataque
        if (Vector3.Distance(ai.transform.position, targetPosition) < 1.5f)  // Distancia de ataque
        {
            // Aquí puedes poner el código para "atacar" al jugador o finalizar el juego
            Debug.Log("Atacando al jugador");
        }
        else
        {
            // Si el jugador se escapa, se vuelve al patrullaje
            ai.ChangeState(new AIPatrolState(ai));
        }
    }

    public override void Exit() { }
}
