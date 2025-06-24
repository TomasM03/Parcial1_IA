using UnityEngine;

public class AI2Controller : AIController
{
    void Start()
    {
        ChangeState(new AIPatrolState(this));
    }

    public override AIState GetChaseState()
    {
        return new AIFleeState(this);
    }
}
