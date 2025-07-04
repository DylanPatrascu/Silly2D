using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CardSO[] cardRewards;
    public PackSO[] packRewards;
    public PlayerStats enemyStats;
    public BattleController battleController;

    private void Awake()
    {
        enemyStats = GetComponent<PlayerStats>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
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
