using NUnit;
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

    public void StartBattle()
    {
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisplayHands();
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



}
