using UnityEngine;

public class AI1Controller : AIController
{
    void Start()
    {
        ChangeState(new AIPatrolState(this));
    }
}
