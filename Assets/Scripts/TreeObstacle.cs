using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObstacle : MonoBehaviour
{
    // When the helicopter enters this trigger, it should die.
    private void OnTriggerEnter2D(Collider2D other) {
        Helicopter helicopter = other.gameObject.GetComponent<Helicopter>();
        if (helicopter) {
            helicopter.Die();
            Destroy(gameObject);
        }
    }
}
