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
        player.Sprite.color = Color.red;

        Debug.Log("basic attack");
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
