using UnityEngine;

public class AIController : MonoBehaviour
{
    //Variables Basicas
    public float patrolSpeed = 3f;
    public float actionSpeed = 5f;
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    public Transform player;
    public float fatigue = 10f;

    //DecisionTree
    public float decisionCooldown = 3f;
    private float decisionTimer = 0f;
    private AIDecisionTree decisionTree;

    public AlertMode alertMode = AlertMode.None;

    private AIState currentState;

    public float flockingRadius = 5f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 0.5f;
    public float separationWeight = 1.5f;

    public delegate void OnDeathHandler(AIController ai);
    public static event OnDeathHandler OnDeath;
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

            // Evitar que AI2 entre en Attack
            if (this is AI2Controller && newAlertMode == AlertMode.Attack)
                newAlertMode = AlertMode.Flee;

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
    public Vector3 CalculateFlockingForce()
    {
        Collider[] neighbors = Physics.OverlapSphere(transform.position, flockingRadius);
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int neighborCount = 0;

        foreach (var col in neighbors)
        {
            if (col.gameObject == gameObject) continue;

            AIController otherAI = col.GetComponent<AIController>();
            if (otherAI != null)
            {
                Vector3 toNeighbor = otherAI.transform.position - transform.position;

                // Alineación: suma de direcciones
                alignment += otherAI.transform.forward;

                // Cohesión: suma de posiciones
                cohesion += otherAI.transform.position;

                // Separación: alejarse de los vecinos cercanos
                separation -= toNeighbor / toNeighbor.sqrMagnitude;

                neighborCount++;
            }
        }

        if (neighborCount < 2) return Vector3.zero;

        alignment = (alignment / neighborCount).normalized;
        cohesion = ((cohesion / neighborCount) - transform.position).normalized;
        separation = (separation / neighborCount).normalized;

        Vector3 flockingForce =
            alignment * alignmentWeight +
            cohesion * cohesionWeight +
            separation * separationWeight;

        return flockingForce.normalized;
    }
    public void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void ChangeState(AIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
