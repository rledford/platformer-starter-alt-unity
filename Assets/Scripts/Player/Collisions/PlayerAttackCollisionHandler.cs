using UnityEngine;

public class PlayerAttackCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private Player player;

    void OnTriggerEnter2D(Collider2D hit) {
        player.StateMachine.CurrentState.OnHitboxCollision(hit);
    }
}
