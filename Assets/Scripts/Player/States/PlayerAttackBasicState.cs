using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackBasicState : PlayerAbilityState
{
    public PlayerAttackBasicState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseAttackInput();
        player.Sprite.color = Color.white;
        player.EnableAttack();

        Debug.Log("basic attack");
    }

    public override void Exit()
    {
        base.Exit();

        player.DisableAttack();
    }

    public override void OnHitboxCollision(Collider2D hit)
    {
        base.OnHitboxCollision(hit);
        Debug.Log("Hit detected");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // do damage

        player.UpdateAccelX();

        if (Time.time > enterTime + playerData.attackDuration) {
            isAbilityDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        // TODO: check attack hitbox collisions
    }
}
