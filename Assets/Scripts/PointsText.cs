using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsText : MonoBehaviour
{
    [SerializeField] float dieTime = 2.0f;
    void Start() {
        StartCoroutine(Sequence());
    }

    public void SetText (int i) {
        string prefix = "+";
        if (i < 0) prefix = "-"; 
        GetComponentInChildren<TextMesh>().text = prefix + Mathf.Abs(i);
    }
    
    IEnumerator Sequence () {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
