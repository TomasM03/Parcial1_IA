using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        // Debug.Log("Estado: Idle");
    }

    public override void Update()
    {
        // Si hay input, cambia a Walk
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            player.ChangeState(new PlayerWalkState(player));
        }
    }

    public override void Exit() { }
}
