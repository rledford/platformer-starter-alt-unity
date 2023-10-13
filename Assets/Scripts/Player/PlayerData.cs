using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    public float gravityScale = 10f;
    public float fallGravityScale = 25f;

    [Header("Checks")]
    public float ceilingCheckRange = 0.1f;
    public float groundCheckRange = 0.1f;
    public float wallCheckRange = 0.1f;

    [Header("Move State")]
    public float moveAccel = 5f;
    public float moveDecel = 5f;
    public float maxMoveSpeed = 10f;
    
    [Header("In Air State")]
    public float maxFallSpeed = 25f;

    [Header("Jump State")]
    public int maxJumps = 1;
    public float jumpHeight = 6f;
    public float jumpCoyoteTime = 0.2f;

    [Header("Wall Run State")]
    public float wallRunSpeed = 10f;
    public float wallRunDistance = 3f;
    public float wallRunVaultHeight = 3f;

    [Header("Basic Attack State")]
    public float attackDuration = 0.1f;

    [Header("Special Attack State")]
    public float specialAttackDistance = 5f;
    public float specialAttackDuration = 1f;
}
