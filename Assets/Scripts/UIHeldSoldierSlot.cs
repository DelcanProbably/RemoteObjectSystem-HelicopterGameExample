using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeldSoldierSlot : MonoBehaviour
{
    [SerializeField] GameObject fill;
    public void SetFill(bool b) {
        fill.SetActive(b);
    }
}
