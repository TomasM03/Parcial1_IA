using UnityEngine;

public class AI2Controller : AIController
{
    void Start()
    {
        ChangeState(new AIPatrolState(this));  // Inicia patrullando
    }
}
