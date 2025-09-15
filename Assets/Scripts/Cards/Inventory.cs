using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    private string savePath => Path.Combine(Application.persistentDataPath, "inventory.json");

    public Inventory(CardDatabaseSO cardDatabase, PackDatabaseSO packDatabase, DeckDatabaseSO deckDatabase)
    {
        this.cardDatabase = cardDatabase;
        this.packDatabase = packDatabase;
        this.deckDatabase = deckDatabase;

        // initialize
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

        LoadInventory();
    }

    #region Card/Pack Methods
    public void AddCard(CardSO card)
    {
        if (cardInv[card] == 0 && !cardDiscovered[card])
        {
            cardDiscovered[card] = true;
            Debug.Log($"Discovered {card.title}!");
        }
        cardInv[card]++;
        SaveInventory();
    }

    public void RemoveCard(CardSO card)
    {
        if (cardInv[card] == 0)
        {
            Debug.Log($"{card.title} Quantity is 0, cannot remove card");
            return;
        }
        cardInv[card]--;
        SaveInventory();
    }

    public int GetCardCount(CardSO card) =>
        cardInv.TryGetValue(card, out int count) ? count : 0;

    public bool GetCardDiscovered(CardSO card) =>
        cardDiscovered.TryGetValue(card, out bool discovered) ? discovered : false;

    public IEnumerable<CardSO> GetAllCards() => cardInv.Keys;

    public void AddPack(PackSO pack)
    {
        if (packInv[pack] == 0 && !packDiscovered[pack])
        {
            packDiscovered[pack] = true;
            Debug.Log($"Discovered {pack.title}!");
        }
        packInv[pack]++;
        SaveInventory();
    }

    public void RemovePack(PackSO pack)
    {
        if (packInv[pack] == 0)
        {
            Debug.Log($"{pack.title} Quantity is 0, cannot remove pack");
            return;
        }
        packInv[pack]--;
        SaveInventory();
    }

    public int GetPackCount(PackSO pack) =>
        packInv.TryGetValue(pack, out int count) ? count : 0;

    public bool GetPackDiscovered(PackSO pack) =>
        packDiscovered.TryGetValue(pack, out bool discovered) ? discovered : false;

    public IEnumerable<PackSO> GetAllPacks() => packInv.Keys;
    #endregion

    #region Deck Methods
    public IEnumerable<DeckSO> GetAllDecks() => deckInv;

    public int GetCardCountInDeck(DeckSO deck, CardSO card) =>
        deck.deckList.Count(c => c == card);

    public void AddCardToDeck(DeckSO deck, CardSO card)
    {
        if (deck.deckList.Count >= deck.deckSize) { Debug.Log("Deck is max size"); return; }
        if (GetCardCountInDeck(deck, card) >= deck.maxCopies) { Debug.Log("Max copies reached"); return; }
        if (GetCardCountInDeck(deck, card) >= GetCardCount(card)) { Debug.Log("Not enough copies in inventory"); return; }

        deck.deckList.Add(card);
        SaveInventory();
    }

    public void RemoveCardFromDeck(DeckSO deck, CardSO card)
    {
        deck.deckList.Remove(card);
        SaveInventory();
    }

    public DeckSO AddDeck()
    {
        DeckSO newDeck = ScriptableObject.CreateInstance<DeckSO>();
        newDeck.title = "Deck " + deckInv.Count;
        newDeck.deckList = new List<CardSO>();

        deckInv.Add(newDeck);
        SaveInventory();
        return newDeck;
    }

    public void RemoveDeck(DeckSO deck)
    {
        if (deckInv.Contains(deck))
        {
            deckInv.Remove(deck);
            SaveInventory();
        }
    }
    #endregion

    #region Save/Load
    public void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();

        // cards
        foreach (var kvp in cardInv)
        {
            saveData.cards.Add(new CardSaveData
            {
                cardID = kvp.Key.guid,
                count = kvp.Value,
                discovered = cardDiscovered[kvp.Key]
            });
        }

        // packs
        foreach (var kvp in packInv)
        {
            saveData.packs.Add(new PackSaveData
            {
                packID = kvp.Key.guid,
                count = kvp.Value,
                discovered = packDiscovered[kvp.Key]
            });
        }

        // decks
        foreach (var deck in deckInv)
        {
            DeckSaveData deckData = new DeckSaveData
            {
                title = deck.title,
                cardIDs = deck.deckList.Select(c => c.guid).ToList()
            };
            saveData.decks.Add(deckData);
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Saved inventory to {savePath}");
    }

    public void LoadInventory()
    {
        deckInv.Clear();

        if (!File.Exists(savePath))
        {
            // fallback: default decks
            deckInv = new List<DeckSO>(deckDatabase.decks);
            Debug.Log("No save found, starting fresh.");
            return;
        }

        string json = File.ReadAllText(savePath);
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

        // cards
        foreach (var data in saveData.cards)
        {
            CardSO card = cardDatabase.cards.FirstOrDefault(c => c.guid == data.cardID);
            if (card != null)
            {
                cardInv[card] = data.count;
                cardDiscovered[card] = data.discovered;
            }
        }

        // packs
        foreach (var data in saveData.packs)
        {
            PackSO pack = packDatabase.packs.FirstOrDefault(p => p.guid == data.packID);
            if (pack != null)
            {
                packInv[pack] = data.count;
                packDiscovered[pack] = data.discovered;
            }
        }

        // decks
        foreach (var deckData in saveData.decks)
        {
            DeckSO deck = ScriptableObject.CreateInstance<DeckSO>();
            deck.title = deckData.title;
            foreach (string cardID in deckData.cardIDs)
            {
                CardSO card = cardDatabase.cards.FirstOrDefault(c => c.guid == cardID);
                if (card != null) deck.deckList.Add(card);
            }
            deckInv.Add(deck);
        }

        Debug.Log($"Loaded inventory: {cardInv.Values.Sum()} cards, {packInv.Values.Sum()} packs, {deckInv.Count} decks");
    }
    #endregion
}
