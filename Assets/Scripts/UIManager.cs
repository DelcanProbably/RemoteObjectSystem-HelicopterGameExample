using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager Singleton;
    [SerializeField] UICounter helicopterSoldiersText;
    [SerializeField] UICounter soldiersText;

    [SerializeField] GameObject winScreenPanel;
    [SerializeField] GameObject loseScreenPanel;

    UILives lives;
    UIPoints points;
    UIHeldSoldiers heldSoldiers;

    [SerializeField] Text coins;

    [SerializeField] Text endGameScore;

    private void Awake() {
        if (Singleton) Destroy(Singleton);
        Singleton = this;
        lives = GetComponentInChildren<UILives>();
        points = GetComponentInChildren<UIPoints>();
        heldSoldiers = GetComponentInChildren<UIHeldSoldiers>();
    }

    // Set the helicopter soldier count
    public static void UpdateHelicopterSoldiers (int soldiers) {
        Singleton.helicopterSoldiersText.UpdateValue(soldiers);
        Singleton.heldSoldiers.SetHeldSoldiers(soldiers);
    }

    // Set the rescued soldier count
    public static void UpdateSoldiers (int score) {
        Singleton.soldiersText.UpdateValue(score);
    } 

    public static void UpdatePoints (int val) {
        Singleton.points.UpdatePoints(val);
    }


    public static void UpdateLives (int nLives) {
        Singleton.lives.UpdateLives(nLives);
    }

    public static void UpdateCoins (int coins) {
        Singleton.coins.text = coins.ToString();
    }

    // Called from GameManager.EndGame(). 
    public static void EndGame(int points) {
        Singleton.Lose();
        Singleton.endGameScore.text = points.ToString();
    }

    public static void HighScore() {
        Singleton.endGameScore.text = Singleton.endGameScore.text + " - HIGH SCORE!";
    }

    void Win() {
        winScreenPanel.SetActive(true);
    }

    void Lose() {
        loseScreenPanel.SetActive(true);
    }

}
