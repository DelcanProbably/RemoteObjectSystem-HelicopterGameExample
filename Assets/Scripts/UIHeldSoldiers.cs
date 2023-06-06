using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeldSoldiers : MonoBehaviour
{
    static UIHeldSoldiers Singleton;

    [SerializeField] List<UIHeldSoldierSlot> slots = new List<UIHeldSoldierSlot>();
    [SerializeField] GameObject slotPrefab;

    private void Awake() {
        Singleton = this;
    }

    public void SetHeldSoldiers(int val) {
        for (int i = 0; i < slots.Count; i++) {
            slots[i].SetFill(i < val);
        }
    }

    public static void IncreaseSlots() {
        Singleton.slots.Add(Instantiate(Singleton.slotPrefab, Singleton.transform).GetComponent<UIHeldSoldierSlot>());
    }
}
