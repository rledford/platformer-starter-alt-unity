using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpsRemaining;

    public PlayerJumpState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
        jumpsRemaining = playerData.maxJumps;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("jump");
        player.Sprite.color = Color.green;

        player.InputHandler.UseJumpInput();
        jumpsRemaining--;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        base.LogicUpdate();

        player.SetGravityScale(playerData.gravityScale);
        player.SetVelocityY(Mathf.Sqrt(-2 * Physics2D.gravity.y * playerData.gravityScale * playerData.jumpHeight));

        isAbilityDone = true;

        // Calculate gravity scale based on a predefined jump time
        // float gravityScale = 2 * playerData.jumpHeight / Mathf.Pow(playerData.jumpTimeToApex, 2);
        // player.SetGravityScale(gravityScale);
        // player.SetVelocityY(Mathf.Sqrt(-2 * Physics2D.gravity.y * gravityScale * playerData.jumpHeight));

        isAbilityDone = true;
    }

    public bool CanJump() {
        return jumpsRemaining > 0;
    }

    public void ResetJumps() {
        jumpsRemaining = playerData.maxJumps;
    }

    public void DecrementRemainingJumps() {
        jumpsRemaining--;
    }
}
