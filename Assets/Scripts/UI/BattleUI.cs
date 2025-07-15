using NUnit;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public BattleController battleController;

    public GameObject menu;

    public GameObject backCardPrefab;
    public GameObject handCardPrefab;

    public GameObject playerPanel;
    public GameObject enemyPanel;

    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text playerHP;
    [SerializeField] private TMP_Text playerArmor;
    [SerializeField] private TMP_Text playerDeck;
    [SerializeField] private TMP_Text playerHand;
    [SerializeField] private TMP_Text playerGraveyard;

    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private TMP_Text enemyHP;
    [SerializeField] private TMP_Text enemyArmor;
    [SerializeField] private TMP_Text enemyDeck;
    [SerializeField] private TMP_Text enemyHand;
    [SerializeField] private TMP_Text enemyGraveyard;

    public void StartBattle()
    {
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisplayHands();
        DisplayStats();
    }

    public void EndBattle()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisplayHands()
    {
        //Player
        foreach (Transform child in playerPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (CardSO card in battleController.GetPlayer().GetHand())
        {

            GameObject handGO = Instantiate(handCardPrefab, playerPanel.transform);
            CardHandUI handCardUI = handGO.GetComponent<CardHandUI>();
            handCardUI.Setup(card, battleController);
        }

        //Enemy
        foreach (Transform child in enemyPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (CardSO card in battleController.GetEnemy().GetHand())
        {

            GameObject handGO = Instantiate(backCardPrefab, enemyPanel.transform);
        }
    }

    public void DisplayStats()
    {
        PlayerStats p = battleController.GetPlayer();
        PlayerStats e = battleController.GetEnemy();

        playerName.text = p.GetPlayerName();
        playerHP.text = "HP: " + p.GetHealth().ToString();
        playerArmor.text = "Armor: " + p.GetArmor().ToString();
        playerHand.text = "Hand: " + p.GetHandSize().ToString();
        playerDeck.text = "Deck: " + p.GetDeckSize().ToString();
        playerGraveyard.text = "Graveyard: " + p.GetGraveyardSize().ToString();

        enemyName.text = e.GetPlayerName();
        enemyHP.text = "HP: " + e.GetHealth().ToString();
        enemyArmor.text = "Armor: " + e.GetArmor().ToString();
        enemyHand.text = "Hand: " + e.GetHandSize().ToString();
        enemyDeck.text = "Deck: " + e.GetDeckSize().ToString();
        enemyGraveyard.text = "Graveyard: " + e.GetGraveyardSize().ToString();
    }



}
