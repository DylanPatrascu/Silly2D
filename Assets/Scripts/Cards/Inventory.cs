using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private Dictionary<CardSO, int> cardInv = new Dictionary<CardSO, int>();
    private Dictionary<CardSO, bool> cardDiscovered = new Dictionary<CardSO, bool>();
    private Dictionary<PackSO, int> packInv = new Dictionary<PackSO, int>();
    private Dictionary<PackSO, bool> packDiscovered = new Dictionary<PackSO, bool>();

    private List<DeckSO> deckInv = new List<DeckSO>();
    private DeckDatabaseSO deckDatabase;


    public Inventory(CardDatabaseSO cardDatabase, PackDatabaseSO packDatabase, DeckDatabaseSO deckDatabase)
    {
        this.deckDatabase = deckDatabase;
        for (int i = 0; i < cardDatabase.cards.Length; i++)
        {
            cardInv[cardDatabase.cards[i]] = 0;
            cardDiscovered[cardDatabase.cards[i]] = false;
        }
        for (int i = 0; i < packDatabase.packs.Length; i++)
        {
            packInv[packDatabase.packs[i]] = 0;
            packDiscovered[packDatabase.packs[i]] = false;
        }
        deckInv = new List<DeckSO>(deckDatabase.decks);
    }

    public void AddCard(CardSO card)
    {
        if (cardInv[card] == 0 && !cardDiscovered[card])
        {
            cardDiscovered[card] = true;
            Debug.Log($"Discovered {card.title}!");
        }
        cardInv[card]++;
        Debug.Log($"Added {card.title} - {card.rarity.ToString()}. Count: {cardInv[card]}");
    }

    public void AddPack(PackSO pack)
    {
        if (packInv[pack] == 0 && !packDiscovered[pack])
        {
            packDiscovered[pack] = true;
            Debug.Log($"Discovered {pack.title}!");

        }
        packInv[pack]++;
        Debug.Log($"Added {pack.title}. Count: {packInv[pack]}");
    }

    public void RemoveCard(CardSO card)
    {
        if (cardInv[card] == 0)
        {
            Debug.Log($"{card.title} Quantity is 0, cannot remove card");
            return;
        }
        cardInv[card]--;
        Debug.Log($"Removed {card.title}. Count: {cardInv[card]}");
    }

    public void RemovePack(PackSO pack)
    {
        if (packInv[pack] == 0)
        {
            Debug.Log($"{pack.title} Quantity is 0, cannot remove pack");
            return;
        }
        packInv[pack]--;
        Debug.Log($"Removed {pack.title}. Count: {packInv[pack]}");

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

    public int GetPackCount(PackSO pack)
    {
        return packInv.TryGetValue(pack, out int count) ? count : 0;
    }

    public bool GetPackDiscovered(PackSO pack)
    {
        return packDiscovered.TryGetValue(pack, out bool discovered) ? discovered : false;
    }

    public IEnumerable<PackSO> GetAllPacks()
    {
        return packInv.Keys;
    }

    public IEnumerable<DeckSO> GetAllDecks()
    {
        return deckInv;
    }

    public void AddCardToDeck(DeckSO deck, CardSO card)
    {
        if(deck.deckList.Count >= deck.deckSize)
        {
            Debug.Log("Deck is max size");
            return;
        }
        //add constraints here
        deck.deckList.Add(card);
    }

    public void RemoveCardFromDeck(DeckSO deck, CardSO card)
    {
        deck.deckList.Remove(card);
    }

    public void AddDeck()
    {

    }

    public void RemoveDeck(DeckSO deck)
    {
        deckInv.Remove(deck);
    }
}
