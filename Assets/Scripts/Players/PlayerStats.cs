using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerName;
    public int health = 100;
    public int maxHealth = 100;
    public int armor = 0;

    private bool playedCard = false;

    public DeckSO deck;
    public List<CardSO> hand = new List<CardSO>();
    public List<CardSO> graveyard = new List<CardSO>();
    public List<CardSO> runtimeDeck = new List<CardSO>();

    public void StartBattle()
    {
        runtimeDeck = new List<CardSO>(deck.deckList);
        Shuffle(runtimeDeck);
    }

    public void EndBattle()
    {
        hand = null;
        graveyard = null;
        runtimeDeck = null;
    }


    public string TakeDamage(int amount, bool piercing)
    {
        int damage = piercing ? amount : Mathf.Max(amount - armor, 0);
        health -= damage;
        return $"{playerName} took {damage} damage.";
    }

    public string Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        return $"{playerName} healed for {amount}.";
    }

    public string AddArmor(int amount)
    {
        armor += amount;
        return $"{playerName} gained {armor}.";
    }

    public void DrawCard(int num)
    {
        for(int i = 0; i < num; i++)
        {
            if (runtimeDeck.Count == 0)
            {
                Debug.Log($"{playerName}'s deck is empty");
                RecycleGraveyard();
            }
            CardSO cardDrawn = runtimeDeck[0];
            runtimeDeck.RemoveAt(0);
            hand.Add(cardDrawn);
            Debug.Log($"{playerName} drew {cardDrawn.title}");
        }
    }

    public void RecycleGraveyard()
    {
        runtimeDeck = graveyard;
        graveyard.Clear();
        Shuffle(runtimeDeck);
        Debug.Log($"{playerName} reshuffled their graveyard into their deck.");
    }

    public void PlayCard(CardSO card, PlayerStats target = null, BattleController battleController = null)
    {
        if (!hand.Contains(card))
        {
            Debug.Log($"{playerName}'s hand is empty or doesn't contain selected card");
            battleController?.battleUI.DisplaySentence($"{playerName}'s hand doesn't contain {card.title}.");
            return;
        }

        battleController?.battleUI.DisplaySentence($"{playerName} played {card.title}!");

        foreach (var effect in card.effects)
        {
            var context = new CardRuntimeContext
            {
                caster = this,
                target = target ?? this,
                card = card
            };

            string effectResult = effect.ApplyEffect(context); // Modify ApplyEffect to return a description string
            if (!string.IsNullOrEmpty(effectResult))
                battleController?.battleUI.DisplaySentence(effectResult);
        }

        hand.Remove(card);
        graveyard.Add(card);
        playedCard = true;
    }

    public bool CardPlayed()
    {
        return playedCard;
    }

    public IEnumerable<CardSO> GetHand()
    {
        return hand;
    }
    public int GetGraveyardSize()
    {
        return graveyard.Count;
    }
    public int GetHandSize()
    {
        return hand.Count;
    }
    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetArmor()
    {
        return armor;
    }

    public int GetDeckSize()
    {
        return runtimeDeck.Count;
    }

    // Fisher–Yates shuffle
    private string Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return $"{playerName} shuffled their deck.";

    }



}
