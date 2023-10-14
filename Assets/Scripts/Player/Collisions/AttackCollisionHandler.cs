using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private Player player;

    void OnTriggerEnter2D(Collider2D hit) {
        player.StateMachine.CurrentState.OnHitboxCollision(hit);
    }
}
