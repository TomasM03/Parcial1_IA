using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    public Transform player;

    private AIState currentState;

    void Start()
    {
        // Inicializamos el estado con Patrol o Idle, según necesites
        ChangeState(new AIPatrolState(this));
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(AIState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }
}
