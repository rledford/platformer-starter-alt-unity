using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunState : PlayerAbilityState
{
    private bool isTouchingCeiling;
    private bool isTouchingWall;
    private Vector2 startPosition;
    private int remainingRuns;

    public PlayerWallRunState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("wall run");
        player.Sprite.color = Color.cyan;

        player.SetGravityScale(playerData.gravityScale);
        player.SetVelocityX(player.FacingDirection);
        player.JumpState.DecrementRemainingJumps();
        startPosition = player.transform.position;
        remainingRuns--;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isTouchingCeiling = player.CheckIsTouchingCeiling();
        isTouchingWall = player.CheckIsTouchingWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float runDistance = GetRunDistanceTraveled();
        bool canRun = isTouchingWall && runDistance < playerData.wallRunDistance;

        if (isTouchingCeiling) {
            stateMachine.ChangeState(player.InAirState);
        } else if (canRun) {
            Run();
        } else {
            Vault();
        }
    }

    public bool CanWallRun() {
        return remainingRuns > 0;
    }

    public void ResetWallRuns() {
        remainingRuns = 1;
    }

    private float GetRunDistanceTraveled () => Mathf.Abs(startPosition.y - player.transform.position.y);

    private void Vault() {
        player.SetVelocityX(0);
        player.SetVelocityY(Mathf.Sqrt(-2 * Physics2D.gravity.y * playerData.gravityScale * playerData.wallRunVaultHeight));
        stateMachine.ChangeState(player.InAirState);
    }

    private void Run() {
        player.SetVelocityY(playerData.wallRunSpeed);
    }
}
