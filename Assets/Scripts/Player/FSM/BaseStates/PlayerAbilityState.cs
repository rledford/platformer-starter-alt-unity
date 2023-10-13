public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    protected bool isRising;
    protected bool hasInputX;
    protected bool hasAttackInput;
    protected bool hasAttackModifier;

    public PlayerAbilityState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) : base(player, playerData, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isActive) return;

        isRising = player.CheckIsRising();
        hasInputX = player.InputHandler.InputX != 0;
        hasAttackInput = player.InputHandler.AttackPressed;
        hasAttackModifier = player.InputHandler.AttackModifierPressed;

        if (isAbilityDone) {
            if (hasAttackInput) {
                if (hasAttackModifier) {
                    stateMachine.ChangeState(player.AttackSpecialState);
                } else {
                    stateMachine.ChangeState(player.AttackBasicState);
                }
            } else if (isGrounded && !isRising) {
                if (hasInputX) {
                    stateMachine.ChangeState(player.RunState);
                } else {
                    stateMachine.ChangeState(player.IdleState);
                }
            } else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
