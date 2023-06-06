using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedyText : MonoBehaviour
{
    static SpeedyText Singleton;
    Animator animator;
    private void Awake() {
        Singleton = this;
        animator = GetComponent<Animator>();
    }

    public static void SpeedBonus() {
        Singleton.animator.SetTrigger("SpeedBonus");
    }
}
