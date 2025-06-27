using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<CardSO, int> cardInv = new Dictionary<CardSO, int>();
    private Dictionary<CardSO, bool> cardDiscovered = new Dictionary<CardSO, bool>();


    public Inventory(CardDatabaseSO cardDatabase)
    {
        for (int i = 0; i < cardDatabase.cards.Length; i++)
        {
            cardInv[cardDatabase.cards[i]] = 0;
            cardDiscovered[cardDatabase.cards[i]] = false;

        }
    }

    public void AddCard(CardSO card)
    {
        if (cardInv[card] == 0 & !cardDiscovered[card])
        {
            cardDiscovered[card] = true;
        }
        cardInv[card]++;
        Debug.Log($"Added {card.title} - {card.rarity.ToString()}. Count: {cardInv[card]}");
    }

    public int GetCardCount(CardSO card)
    {
        return cardInv.TryGetValue(card, out int count) ? count : 0;
    }

    public bool GetCardDiscovered(CardSO card)
    {
        return cardDiscovered.TryGetValue(card, out bool discovered) ? discovered : false;
    }

    public IEnumerable<CardSO> GetAllCards()
    {
        return cardInv.Keys;
    }
}
