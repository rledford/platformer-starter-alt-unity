using UnityEngine;

public class PlayerAttackSpecialState : PlayerAbilityState
{
    private Vector2 startPosition;
    private float xVelocity;
    private bool isTouchingWall;

    public PlayerAttackSpecialState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startPosition = player.transform.position;
        xVelocity = playerData.specialAttackDistance / playerData.specialAttackDuration * player.FacingDirection;
        player.SetGravityScale(0);
        player.SetVelocityX(xVelocity);
        player.SetVelocityY(0);
        player.InputHandler.UseAttackInput();
        player.Sprite.color = Color.white;

        Debug.Log("special attack");
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocityX(playerData.maxMoveSpeed * player.FacingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isTouchingWall || Time.time > enterTime + playerData.specialAttackDuration) {
            isAbilityDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isTouchingWall = player.CheckIsTouchingWall();

        // TODO: check attack hitbox collisions and do damage
    }

    private float GetAttackDistanceTraveled () => Mathf.Abs(startPosition.x - player.transform.position.x);
}
