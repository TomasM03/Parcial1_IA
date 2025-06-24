using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class AIState
{
    protected AIController ai;

    public AIState(AIController ai)
    {
        this.ai = ai;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
