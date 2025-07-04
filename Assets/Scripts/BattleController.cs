using UnityEngine;

public class BattleController : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats enemy;

    public CardSO playerCard;
    public CardSO enemyCard;

    public BattleUI battleUI;
    
    public void StartBattle(PlayerStats p, PlayerStats e)
    {
        player = p;
        enemy = e;
        battleUI.StartBattle();
        battleUI.DisplayHands();
    }
    public void DrawStep()
    {
        player.DrawCard();
        enemy.DrawCard();
    }
    public void Combat()
    {
        Debug.Log("Combat phase started");

        // Decide quickcast order (could also check effect types directly)
        bool playerFirst = playerCard.quickCast && !enemyCard.quickCast;
        bool enemyFirst = enemyCard.quickCast && !playerCard.quickCast;

        if (playerFirst)
        {
            player.PlayCard(playerCard, enemy);
            if(CheckDead(enemy))
            {
                Debug.Log("Player wins");
            }
            enemy.PlayCard(enemyCard, player);
        }
        else if (enemyFirst)
        {
            enemy.PlayCard(enemyCard, player);
            if (CheckDead(player))
            {
                Debug.Log("Enemy wins");
            }
            player.PlayCard(playerCard, enemy);
        }
        else
        {
            // Simultaneous
            player.PlayCard(playerCard, enemy);
            enemy.PlayCard(enemyCard, player);
            bool playerDead = CheckDead(player);
            bool enemyDead = CheckDead(enemy);

            if (playerDead && enemyDead)
            {
                Debug.Log("Draw");
            }
            else if (playerDead)
            {
                Debug.Log("Enemy wins");
            }
            else if (enemyDead)
            {
                Debug.Log("Player wins");
            }
        }
    }

    public bool CheckDead(PlayerStats target)
    {
        return target.health <= 0;
    }


    public PlayerStats GetPlayer()
    {
        return player;
    }
    public PlayerStats GetEnemy()
    {
        return enemy;
    }

    public void SetPlayerCard(CardSO card)
    {
        playerCard = card;
    }
}
