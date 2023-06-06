using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    Text txt;
    private void Awake() {
        txt = GetComponent<Text>();
    }
    public void UpdatePoints (int points) {
        txt.text = Format(points);
    }
    
    public static string Format (int val) {
        string s = "";
        int dec = 100000;
        while (dec > 10) {
            if (val < dec) s += "0";
            dec /= 10;
        }
        if (val == 0) s += "0";
        s += Mathf.Abs(val).ToString();
        if (val < 0) s = "-" + s;
        return s;
    }
}
