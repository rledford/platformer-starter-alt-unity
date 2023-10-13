using UnityEngine;

public class FiniteState
{
    protected float enterTime;
    protected bool isActive;

    public virtual void Enter() {
        enterTime = Time.time;
        isActive = true;
        DoChecks();
    }

    public virtual void Exit() {
        isActive = false;
    }

    public virtual void LogicUpdate() {}
    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() {}
}
