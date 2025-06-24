using UnityEngine;

public class AIController : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public float actionSpeed = 5f;
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    public Transform player;
    public float fatigue = 10f;
    public float decisionCooldown = 3f;
    private float decisionTimer = 0f;

    public AlertMode alertMode = AlertMode.None;

    private AIState currentState;
    private AIDecisionTree decisionTree;

    public virtual AIState GetChaseState()
    {
        return new AIAttackState(this);
    }

    public static class AIMovementHelper
    {
        public static Vector3 GetAvoidanceDirection(Transform aiTransform, Vector3 desiredDirection, float avoidDistance = 5f, float avoidStrength = 7f)
        {
            RaycastHit hit;
            Vector3 avoidanceDir = Vector3.zero;

            if (Physics.Raycast(aiTransform.position, aiTransform.forward, out hit, avoidDistance))
            {
                avoidanceDir = Vector3.Reflect(aiTransform.forward, hit.normal) * avoidStrength;
            }

            Vector3 finalDir = (desiredDirection + avoidanceDir).normalized;
            return finalDir;
        }
    }

    void Awake()
    {
        decisionTree = new AIDecisionTree(this);
    }

    void Start()
    {
        moveSpeed = patrolSpeed;
        ChangeState(new AIPatrolState(this));
    }

    void Update()
    {
        decisionTimer -= Time.deltaTime;

        if (decisionTimer <= 0f)
        {
            var newAlertMode = decisionTree.Evaluate();

            if (newAlertMode != alertMode)
            {
                alertMode = newAlertMode;

                switch (alertMode)
                {
                    case AlertMode.Attack:
                    case AlertMode.Flee:
                        ChangeState(GetChaseState());
                        break;
                    case AlertMode.None:
                        ChangeState(new AIPatrolState(this));
                        break;
                }
            }

            decisionTimer = decisionCooldown;
        }

        currentState.Update();
    }

    public void ChangeState(AIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
