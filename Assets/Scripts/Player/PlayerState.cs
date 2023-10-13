public class PlayerState : FiniteState
{
    protected Player player;
    protected PlayerData playerData;
    protected FiniteStateMachine stateMachine;
    protected float coyoteStartTime;

    public PlayerState(Player player, PlayerData playerData, FiniteStateMachine stateMachine) {
        this.player = player;
        this.playerData = playerData;
        this.stateMachine = stateMachine;
    }
}
