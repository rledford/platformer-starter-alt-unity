using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData")]
public class PlayerData : ScriptableObject
{
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
    public float jumpCancelMultiplier = 0.3f;
    public float jumpTimeToApex = 1.5f;
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

    #region Computed
    public float gravityScale { get; private set; }
    public float jumpVelocity { get; private set; }
    #endregion

    void OnValidate() {
        gravityScale = 2 * jumpHeight / Mathf.Pow(jumpTimeToApex, 2);
        jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * gravityScale * jumpHeight);
        // player.SetGravityScale(gravityScale);
        // player.SetVelocityY(Mathf.Sqrt(-2 * Physics2D.gravity.y * gravityScale * playerData.jumpHeight));
    }
}
