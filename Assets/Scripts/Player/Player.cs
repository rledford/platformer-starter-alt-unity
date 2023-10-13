using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Rigidbody2D RB { get; private set; }
    public SpriteRenderer Sprite { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] ceilingChecks;

    [SerializeField]
    private Transform[] groundChecks;

    [SerializeField]
    private Transform[] wallChecks;
    #endregion

    #region Colliders
    private RaycastHit2D[] ceilingCheckHits = new RaycastHit2D[1];
    private RaycastHit2D[] groundCheckHits = new RaycastHit2D[1];
    private RaycastHit2D[] wallCheckHits = new RaycastHit2D[1];
    #endregion

    #region Status
    private Vector2 velocity;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    #endregion

    #region States
    public FiniteStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallRunState WallRunState { get; private set; }
    public PlayerAttackBasicState AttackBasicState { get; private set; }
    public PlayerAttackSpecialState AttackSpecialState { get; private set; }
    #endregion

    #region Mutations
    public void SetGravityScale(float gravityScale) {
        RB.gravityScale = gravityScale;
    }

    public void SetVelocityX(float vx) {
        velocity.Set(vx, CurrentVelocity.y);
        RB.velocity = velocity;
        CurrentVelocity = velocity;
    }

    public void SetVelocityY(float vy) {
        velocity.Set(CurrentVelocity.x, vy);
        RB.velocity = velocity;
        CurrentVelocity = velocity;
    }

    public void AccelX() {
        float vx = Mathf.Clamp(
            CurrentVelocity.x + FacingDirection * playerData.maxMoveSpeed * playerData.moveAccel * Time.deltaTime,
            -playerData.maxMoveSpeed,
            playerData.maxMoveSpeed
        );
        SetVelocityX(vx);
    }

    public void DecelX() {
        float vx = Mathf.Abs(CurrentVelocity.x) - playerData.maxMoveSpeed * playerData.moveDecel * Time.deltaTime;

        if (vx < 0.1f) {
            SetVelocityX(0f);
        } else {
            SetVelocityX(vx * FacingDirection);
        }
    }

    public void UpdateAccelX() {
        if (InputHandler.InputX == 0) {
            if (CurrentVelocity.x != 0f) {
                DecelX();
            }
        } else {
            if (InputHandler.InputX != FacingDirection) {
                Flip();
            }
            AccelX();
        }
    }

    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion

    #region Checks
    public bool CheckIsRising() {
        return CurrentVelocity.y > 0f;
    }

    public bool CheckIsFalling() {
        return CurrentVelocity.y < 0.1f;
    }

    public bool CheckIsMovingX() {
        return Mathf.Abs(CurrentVelocity.x) > 0.1;
    }

    public bool CheckIsTouchingCeiling() {
        int hits = 0;
        for (int i = 0; i < ceilingChecks.Length; i++) {
            hits += Physics2D.RaycastNonAlloc(ceilingChecks[i].position, Vector2.up, ceilingCheckHits, playerData.ceilingCheckRange, whatIsGround);
        }
        
        return hits > 0;
    }
    public bool CheckIsGrounded() {
        int hits = 0;
        for (int i = 0; i < groundChecks.Length; i++) {
            hits += Physics2D.RaycastNonAlloc(groundChecks[i].position, Vector2.down, groundCheckHits, playerData.groundCheckRange, whatIsGround);
        }
        
        return hits > 0;
    }

    public bool CheckIsTouchingWall() {
        int hits = 0;
        for (int i = 0; i < wallChecks.Length; i++) {
            hits += Physics2D.RaycastNonAlloc(wallChecks[i].position, Vector2.right * FacingDirection, wallCheckHits, playerData.wallCheckRange, whatIsGround);
        }

        return hits > 0;
    }
    #endregion

    #region Unity
    void Awake() {
        StateMachine = new FiniteStateMachine();
        IdleState = new PlayerIdleState(this, playerData, StateMachine);
        RunState = new PlayerRunState(this, playerData, StateMachine);
        InAirState = new PlayerInAirState(this, playerData, StateMachine);
        JumpState = new PlayerJumpState(this, playerData, StateMachine);
        WallRunState = new PlayerWallRunState(this, playerData, StateMachine);
        AttackBasicState = new PlayerAttackBasicState(this, playerData, StateMachine);
        AttackSpecialState = new PlayerAttackSpecialState(this, playerData, StateMachine);
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        InputHandler = GetComponent<PlayerInputHandler>();
        FacingDirection = 1;
        StateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
        for (int i = 0; i < ceilingChecks.Length; i++) {
            Gizmos.DrawRay(ceilingChecks[i].position, Vector2.up * playerData.groundCheckRange);
        }
        for (int i = 0; i < groundChecks.Length; i++) {
            Gizmos.DrawRay(groundChecks[i].position, Vector2.down * playerData.groundCheckRange);
        }
        for (int i = 0; i < wallChecks.Length; i++) {
            Gizmos.DrawRay(wallChecks[i].position, Vector2.right * playerData.wallCheckRange);
        }
        // Gizmos.DrawRay(wallCheck.position, Vector2.right * playerData.wallCheckRange * FacingDirection);
		// Gizmos.DrawSphere(groundCheck.position, playerData.groundCheckRadius);
	}
    #endregion
}
