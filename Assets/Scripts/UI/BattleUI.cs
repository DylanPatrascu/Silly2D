using NUnit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;
using static Unity.VisualScripting.Member;

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

    [SerializeField] private TMP_Text sentenceText;
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip talkingClip;

    private Queue<string> messageQueue = new Queue<string>();
    private bool isDisplayingMessage = false;



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

    public void DisplaySentence(string sentence)
    {
        messageQueue.Enqueue(sentence);
        if (!isDisplayingMessage)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isDisplayingMessage = true;

        while (messageQueue.Count > 0)
        {
            string currentSentence = messageQueue.Dequeue();
            yield return StartCoroutine(RenderSentence(currentSentence));

            // Small pause between messages
            yield return new WaitForSeconds(0.5f);
        }

        isDisplayingMessage = false;
    }

    private IEnumerator RenderSentence(string sentence)
    {
        //sentenceText.text = "";
        // Keep existing text, then append the new sentence on a new line.
        string previousText = sentenceText.text;
        sentenceText.text = previousText + (string.IsNullOrEmpty(previousText) ? "" : "\n");

        foreach (char letter in sentence)
        {
            sentenceText.text += letter;
            if (talkingClip && source && sentenceText.text.Length % 4 == 0)
                source.PlayOneShot(talkingClip);

            yield return new WaitForSeconds(textSpeed);
        }
    }

}
