using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerName;
    public int health = 100;
    public int armor = 0;

    private bool playedCard = false;

    public List<CardSO> deck = new List<CardSO>();
    public List<CardSO> hand = new List<CardSO>();
    public List<CardSO> graveyard = new List<CardSO>();

    public void TakeDamage(int amount, bool piercing)
    {
        int damage = piercing ? amount : Mathf.Max(amount - armor, 0);
        health -= damage;
        Debug.Log($"{playerName} took {damage} damage. Health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"{playerName} healed for {amount}. Health: {health}");
    }

    public void AddArmor(int amount)
    {
        armor += amount;
        Debug.Log($"{playerName} gained {armor}. Armor: {armor}");
    }

    public void DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log($"{playerName}'s deck is empty");
            return;
        }
        CardSO cardDrawn = deck[0];
        deck.RemoveAt(0);
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



}
