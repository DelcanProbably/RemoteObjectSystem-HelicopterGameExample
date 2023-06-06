using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionText : MonoBehaviour
{
    [SerializeField] float shakeAmount;
    [SerializeField] string[] words;
    [SerializeField] float chance = 0.5f;
    float timer;
    const float deathTime = 1f;
    private void Awake() {
        if (Random.Range(0, 1.0f) > chance) Destroy(gameObject);
        else GetComponent<TextMesh>().text = words[Random.Range(0, words.Length)];
    }
    private void FixedUpdate() {
        Vector2 shake = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        transform.localPosition = shake * shakeAmount;
        timer += Time.fixedDeltaTime;
        if (timer >= deathTime) Destroy(gameObject);
    }
}
