using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;

public class Inventory
{
    private Dictionary<CardSO, int> cardInv = new Dictionary<CardSO, int>();
    private Dictionary<CardSO, bool> cardDiscovered = new Dictionary<CardSO, bool>();
    private Dictionary<PackSO, int> packInv = new Dictionary<PackSO, int>();
    private Dictionary<PackSO, bool> packDiscovered = new Dictionary<PackSO, bool>();

    private List<DeckSO> deckInv = new List<DeckSO>();
    private CardDatabaseSO cardDatabase;
    private PackDatabaseSO packDatabase;
    private DeckDatabaseSO deckDatabase;

    private string savePath => Path.Combine(Application.persistentDataPath, "decks.json");


    public Inventory(CardDatabaseSO cardDatabase, PackDatabaseSO packDatabase, DeckDatabaseSO deckDatabase)
    {
        this.cardDatabase = cardDatabase;
        this.packDatabase = packDatabase;
        this.deckDatabase = deckDatabase;

        foreach (var card in cardDatabase.cards)
        {
            cardInv[card] = 0;
            cardDiscovered[card] = false;
        }
        foreach (var pack in packDatabase.packs)
        {
            packInv[pack] = 0;
            packDiscovered[pack] = false;
        }

        LoadDecks();
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
        SaveDecks();
    }

    public void RemoveCardFromDeck(DeckSO deck, CardSO card)
    {
        deck.deckList.Remove(card);
        SaveDecks();
    }

    public DeckSO AddDeck()
    {
        DeckSO newDeck = ScriptableObject.CreateInstance<DeckSO>();
        newDeck.title = "Deck " + deckInv.Count;
        newDeck.deckList = new List<CardSO>();

        deckInv.Add(newDeck);
        SaveDecks();

        Debug.Log($"Created deck {newDeck.title}");
        //AddCardToDeck(newDeck, cardDatabase.cards[0]);
        return newDeck;
    }

    public void RemoveDeck(DeckSO deck)
    {
        if (deckInv.Contains(deck))
        {
            deckInv.Remove(deck);
            SaveDecks();
        }
    }

    public void SaveDecks()
    {
        List<DeckSaveData> saveData = new List<DeckSaveData>();

        foreach (var deck in deckInv)
        {
            DeckSaveData data = new DeckSaveData();
            data.title = deck.title;
            data.cardIDs = deck.deckList.Select(card => card.guid).ToList();
            saveData.Add(data);
        }

        DeckSaveDataWrapper wrapper = new DeckSaveDataWrapper { decks = saveData };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);

        Debug.Log($"Saved decks to {savePath}");
    }

    public void LoadDecks()
    {
        deckInv.Clear();

        if (!File.Exists(savePath))
        {
            // fallback: load default decks from database
            deckInv = new List<DeckSO>(deckDatabase.decks);
            Debug.Log("No save file found, loaded default decks.");
            return;
        }

        string json = File.ReadAllText(savePath);
        DeckSaveDataWrapper wrapper = JsonUtility.FromJson<DeckSaveDataWrapper>(json);

        foreach (var data in wrapper.decks)
        {
            DeckSO deck = ScriptableObject.CreateInstance<DeckSO>();
            deck.title = data.title;

            foreach (string cardID in data.cardIDs)
            {
                CardSO card = cardDatabase.cards.FirstOrDefault(c => c.guid == cardID);
                if (card != null)
                    deck.deckList.Add(card);
            }

            deckInv.Add(deck);
        }

        Debug.Log($"Loaded {deckInv.Count} decks from save file.");
    }
}
