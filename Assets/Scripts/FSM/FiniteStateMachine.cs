public class FiniteStateMachine
{
    public FiniteState CurrentState { get; private set; }

    public void ChangeState(FiniteState state) {
        if (CurrentState != null) {
            CurrentState.Exit();
        }
        CurrentState = state;
        CurrentState.Enter();
    }
}
