using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMoveInput { get; private set; }
    public int InputX { get; private set; }
    public int InputY { get; private set; }
    public bool JumpPressed { get; private set; }
    private float jumpInputStartTime;
    private float jumpInputBufferTime = 0.2f;
    public bool AttackPressed { get; private set; }
    public bool AttackModifierPressed { get; private set; }
    public float attackInputStartTime;
    public float attackBufferTime = 0.2f;

    void Awake() {
        RawMoveInput = Vector2.zero;
        InputX = 0;
        InputY = 0;
    }

    void Update() {
        CheckJumpInputBuffer();
        CheckBasicAttackInputBuffer();
    }

    public void OnMoveInput(InputAction.CallbackContext ctx) {
        RawMoveInput = ctx.ReadValue<Vector2>();
        InputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        InputY = (int)(RawMoveInput * Vector2.up).normalized.y;
    }

    public void OnAttackInput(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            attackInputStartTime = Time.time;
            AttackPressed = true;
        }
    }

    public void OnModifierInput(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            AttackModifierPressed = true;
        } else if (ctx.canceled) {
            AttackModifierPressed = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            jumpInputStartTime = Time.time;
            JumpPressed = true;
        } else if (ctx.canceled) {
            // TODO: add logic for jump-cut
            JumpPressed = false;
        }
    }

    public void UseJumpInput() {
        JumpPressed = false;
    }


    private void CheckJumpInputBuffer() {
        if (JumpPressed && Time.time > jumpInputStartTime + jumpInputBufferTime) {
            JumpPressed = false;
        }
    }

    private void CheckBasicAttackInputBuffer() {
        if (AttackPressed && Time.time > attackInputStartTime + attackBufferTime) {
            AttackPressed = false;
        }
    }

    public void UseAttackInput() {
        AttackPressed = false;
    }
}
