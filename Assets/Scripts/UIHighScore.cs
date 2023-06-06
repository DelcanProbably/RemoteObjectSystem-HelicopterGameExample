using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighScore : MonoBehaviour
{
    Text text;
    private void Awake() {
        text = GetComponent<Text>();
    }
    private void OnEnable() {
        text.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
