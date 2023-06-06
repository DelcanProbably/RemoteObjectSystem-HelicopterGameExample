using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounter : MonoBehaviour
{
    Text text;
    // Text to be added before this counter's value
    [SerializeField] string prefix = "";
    private void Awake() {
        text = GetComponent<Text>();
    }
    public void UpdateValue(int value) {
         text.text = prefix + value;
    }
}
