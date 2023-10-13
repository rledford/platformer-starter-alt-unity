using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isMovingX;
    protected bool hasInputX;
    protected bool hasAttackInput;
    protected bool hasAttackModifier;
    protected bool hasJumpInput;

    public PlayerGroundedState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetGravityScale(playerData.gravityScale);
        player.JumpState.ResetJumps();
        player.WallRunState.ResetWallRuns();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
        isTouchingWall = player.CheckIsTouchingWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.UpdateAccelX();

        isMovingX = player.CheckIsMovingX();
        hasInputX = player.InputHandler.InputX != 0;
        hasJumpInput = player.InputHandler.JumpPressed;
        hasAttackInput = player.InputHandler.AttackPressed;
        hasAttackModifier = player.InputHandler.AttackModifierPressed;

        if (hasAttackInput && !hasJumpInput) {
            if (hasAttackModifier) {
                stateMachine.ChangeState(player.AttackSpecialState);
            } else {
                stateMachine.ChangeState(player.AttackBasicState);    
            }
        }
    }
}
