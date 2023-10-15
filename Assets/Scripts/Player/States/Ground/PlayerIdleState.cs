using UnityEngine;

public class PlayerIdleState : PlayerBaseGroundState
{
    public PlayerIdleState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Sprite.color = Color.black;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isActive) {
            return;
        }

        if (hasJumpInput) {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (!isGrounded) {
            stateMachine.ChangeState(player.InAirState);
        } else if (hasInputX) {
            stateMachine.ChangeState(player.RunState);
        }
    }
}
