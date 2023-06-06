using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    static Store Singleton;
    static int coins = 0;

    [SerializeField] Helicopter helicopter;

    // this is very gross but i'm kinda rushing to just get this system working
    [SerializeField] int capacityCost;
    [SerializeField] Button capacityBtn;

    [SerializeField] int lifeCost;
    [SerializeField] Button lifeBtn;

    [SerializeField] int speedCost;
    [SerializeField] Button speedBtn;
    [SerializeField] int speedUpgrades = 3;

    [SerializeField] AudioClip saleClip;

    [SerializeField] GameObject storePanel;

    private void Awake() {
        Singleton = this;
        coins = 0;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!Pause.paused) {
                storePanel.SetActive(!storePanel.activeInHierarchy);
            
                if (storePanel.activeInHierarchy) {
                    Time.timeScale = 0;
                } else {
                    Time.timeScale = 1;
                }
            }
        }
        if (storePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            storePanel.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public static void AddCoins (int val) {
        coins += val;
        UIManager.UpdateCoins(coins);

        if (Singleton.speedBtn) Singleton.speedBtn.interactable = coins >= Singleton.speedCost;
        Singleton.capacityBtn.interactable = coins >= Singleton.capacityCost;
        Singleton.lifeBtn.interactable = coins >= Singleton.lifeCost;
    }

    public void BuyCapacity () {
        if (capacityCost > coins) {
            AddCoins(0);
            return;
        }
        AddCoins(-capacityCost);
        helicopter.UpgradeCapacity();
        PlaySound();
    }

    public void BuyLife () {
        if (lifeCost > coins) {
            AddCoins(0);
            return;
        }
        AddCoins(-lifeCost);
        helicopter.EarnLife();
        PlaySound();
    }

    public void BuySpeed () {
        if (speedCost > coins) {
            AddCoins(0);
            return;
        }
        AddCoins(-speedCost);
        helicopter.UpgradeSpeed();
        PlaySound();
        speedUpgrades--;
        if (speedUpgrades <= 0) {
            Destroy(speedBtn.gameObject);
        }
    }

    void PlaySound () {
        SoundManager.PlayClip(saleClip);
    }


}
