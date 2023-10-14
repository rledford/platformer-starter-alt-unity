using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isRising;
    private int inputX;
    private bool hasInputX;
    private bool hasInputJump;
    private bool hasInputJumpCanceled;
    private bool hasCoyote;
    private bool hasAttackInput;
    private bool hasAttackModifier;
    public PlayerInAirState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("in air");
        player.Sprite.color = Color.gray;
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

        if (!isActive) {
            return;
        }

        isRising = player.CheckIsRising();
        inputX = player.InputHandler.InputX;
        hasInputX = inputX != 0;
        hasInputJump = player.InputHandler.JumpPressed;
        hasInputJumpCanceled = player.InputHandler.JumpCanceled;
        hasAttackInput = player.InputHandler.AttackPressed;
        hasAttackModifier = player.InputHandler.AttackModifierPressed;
        
        UpdateCoyoteTime();
        UpdateFall();
        player.UpdateAccelX();

        if (hasInputJump) {
            if (isTouchingWall && inputX == player.FacingDirection && player.WallRunState.CanWallRun()) {
                stateMachine.ChangeState(player.WallRunState);
            } else if (player.JumpState.CanJump()) {
                stateMachine.ChangeState(player.JumpState);
            }
        } else if (isGrounded && !isRising) {
            if (!hasInputX) {
                stateMachine.ChangeState(player.IdleState);
            } else {
                stateMachine.ChangeState(player.RunState);
            }
        } else if (hasAttackInput) {
            if (hasAttackModifier) {
                stateMachine.ChangeState(player.AttackSpecialState);
            }  else {
                stateMachine.ChangeState(player.AttackBasicState);
            }
        }
    }

    public void StartCoyoteTime() {
        hasCoyote = true;
    }

    private void UpdateCoyoteTime() {
        if (hasCoyote && Time.time > enterTime + playerData.jumpCoyoteTime) {
            hasCoyote = false;
            player.JumpState.DecrementRemainingJumps();
        }
    }

    private void UpdateFall() {
        if (!isRising) {
            player.SetGravityScale(playerData.fallGravityScale);
            player.SetVelocityY(Mathf.Max(player.CurrentVelocity.y, -playerData.maxFallSpeed));
        } else if (hasInputJumpCanceled) {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpCancelMultiplier);
        }
    }
}
