using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        // Debug.Log("Estado: Walk");
    }

    public override void Update()
    {
        // Movimiento b�sico
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveY).normalized;
        player.transform.position += move * player.moveSpeed * Time.deltaTime;

        // Si no hay input, cambia a Idle
        if (moveX == 0 && moveY == 0)
        {
            player.ChangeState(new PlayerIdleState(player));
        }
    }

    public override void Exit() { }
}
