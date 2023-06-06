using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Helicopter helicopter = other.gameObject.GetComponent<Helicopter>();
        if (helicopter) {
            helicopter.DepositSoldiers();
        }
    }
}
