using UnityEngine;

public class PlayerRunState : PlayerBaseGroundState
{
    public PlayerRunState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("run");
        player.Sprite.color = Color.yellow;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isActive) {
            return;
        }

        if (hasJumpInput) {
            if (isTouchingWall) {
                stateMachine.ChangeState(player.WallRunState);
            } else {
                stateMachine.ChangeState(player.JumpState);
            }
            return;
        }

        if (!isGrounded) {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        } else if (!hasInputX) {
            if (!isMovingX) {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
