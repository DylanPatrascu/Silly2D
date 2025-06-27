using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public CardManager cardManager;
    public InventoryUI inv;

    public void OpenPack(PackSO pack)
    {
        // Common
        List<CardSO> commonCards = pack.cards.Where(card => card.rarity == Rarity.Common).ToList();

        Shuffle(commonCards);

        for (int i = 0; i < Mathf.Min(pack.numCommon, commonCards.Count); i++)
        {
            cardManager.inventory.AddCard(commonCards[i]);
        }

        // Uncommon
        List<CardSO> uncommonCards = pack.cards.Where(card => card.rarity == Rarity.Uncommon).ToList();

        Shuffle(uncommonCards);

        for (int i = 0; i < Mathf.Min(pack.numUncommon, uncommonCards.Count); i++)
        {
            cardManager.inventory.AddCard(uncommonCards[i]);
        }

        // Rare/Mythic
        for (int i = 0; i < pack.NumRare; i++)
        {
            if (Random.value <= pack.mythicOdds)
            {
                // Mythic
                List<CardSO> mythicCards = pack.cards.Where(card => card.rarity == Rarity.Mythic).ToList();

                Shuffle(mythicCards);
                
                cardManager.inventory.AddCard(mythicCards[i]);
                
            }
            else
            {
                // Rare
                List<CardSO> rareCards = pack.cards.Where(card => card.rarity == Rarity.Rare).ToList();

                Shuffle(rareCards);

                cardManager.inventory.AddCard(rareCards[i]);
            }
        }
        inv.DisplayInventory(cardManager.inventory);
    }

    // Fisher–Yates shuffle
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
