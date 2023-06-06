using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{

    private void Start() {
        GameManager.AddSoldier();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Helicopter helicopter = other.gameObject.GetComponent<Helicopter>();
        if (helicopter) {
            helicopter.SoldierCollision(this);
        }
    }
}
