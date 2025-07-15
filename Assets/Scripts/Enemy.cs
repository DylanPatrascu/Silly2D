using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CardSO[] cardRewards;
    public PackSO[] packRewards;
    public PlayerStats enemyStats;
    public BattleController battleController;
    public bool beaten = false;

    private void Awake()
    {
        enemyStats = GetComponent<PlayerStats>();
    }

    public void GiveRewards(PlayerStats player)
    {
        CardManager playerInventory = player.GetComponent<CardManager>();
        
        foreach (var card in cardRewards)
        {
            playerInventory.GetInventory().AddCard(card);
        }

        foreach (var pack in packRewards)
        {
            playerInventory.GetInventory().AddPack(pack);
        }
        Debug.Log("Enemy Defeated, Rewards granted");
        beaten = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !beaten)
        {
            Debug.Log("Battle");
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            if(player == null)
            {
                Debug.Log("player no playerstats");
                return;
            }
            battleController.StartBattle(player, enemyStats);
        }
    }
}
