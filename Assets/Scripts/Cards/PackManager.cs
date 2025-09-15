using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public CardManager cardManager;
    public InventoryUI inv;

    public void AddPack(PackSO pack)
    {
        cardManager.GetInventory().AddPack(pack);
        inv.DisplayInventory();
    }

    public void OpenPack(PackSO pack)
    {
        if(cardManager.inventory.GetPackCount(pack) == 0)
        {
            return;
        }
        // Common
        List<CardSO> commonCards = pack.cards.Where(card => card.rarity == Rarity.Common).ToList();

        Shuffle(commonCards);

        for (int i = 0; i < Mathf.Min(pack.numCommon, commonCards.Count); i++)
        {
            cardManager.GetInventory().AddCard(commonCards[i]);
        }

        // Uncommon
        List<CardSO> uncommonCards = pack.cards.Where(card => card.rarity == Rarity.Uncommon).ToList();

        Shuffle(uncommonCards);

        for (int i = 0; i < Mathf.Min(pack.numUncommon, uncommonCards.Count); i++)
        {
            cardManager.GetInventory().AddCard(uncommonCards[i]);
        }

        // Rare/Mythic
        for (int i = 0; i < pack.numRare; i++)
        {
            if (Random.value <= pack.mythicOdds)
            {
                // Mythic
                List<CardSO> mythicCards = pack.cards.Where(card => card.rarity == Rarity.Mythic).ToList();

                Shuffle(mythicCards);
                
                cardManager.GetInventory().AddCard(mythicCards[i]);
                
            }
            else
            {
                // Rare
                List<CardSO> rareCards = pack.cards.Where(card => card.rarity == Rarity.Rare).ToList();

                Shuffle(rareCards);

                cardManager.GetInventory().AddCard(rareCards[i]);
            }
        }
        cardManager.GetInventory().RemovePack(pack);
        inv.DisplayInventory();

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
