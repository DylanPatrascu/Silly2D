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


    public void TakeDamage(int amount, bool piercing)
    {
        int damage = piercing ? amount : Mathf.Max(amount - armor, 0);
        health -= damage;
        Debug.Log($"{playerName} took {damage} damage. Health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log($"{playerName} healed for {amount}. Health: {health}");
    }

    public void AddArmor(int amount)
    {
        armor += amount;
        Debug.Log($"{playerName} gained {armor}. Armor: {armor}");
    }

    public void DrawCard()
    {
        if (runtimeDeck.Count == 0)
        {
            Debug.Log($"{playerName}'s deck is empty");
            return;
        }
        CardSO cardDrawn = runtimeDeck[0];
        runtimeDeck.RemoveAt(0);
        hand.Add(cardDrawn);
        Debug.Log($"{playerName} drew {cardDrawn.title}");
    }

    public void PlayCard(CardSO card, PlayerStats target = null)
    {
        if (!hand.Contains(card))
        {
            Debug.Log($"{playerName}'s hand is empty or doesn't contain selected card");
            return;
        }

        foreach (var effect in card.effects)
        {
            var context = new CardRuntimeContext
            {
                caster = this,
                target = target ?? this,
                card = card
            };

            effect.ApplyEffect(context);
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

    // Fisher–Yates shuffle
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        Debug.Log($"{playerName} shuffled their deck.");

    }



}
