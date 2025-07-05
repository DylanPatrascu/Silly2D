using UnityEngine;

public enum BattleState
{
    NoCombat = 0,
    DrawStep = 1,
    Combat = 2,
    EndStep = 3
}
public class BattleController : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats enemy;

    public CardSO playerCard;
    public CardSO enemyCard;

    public BattleUI battleUI;
    public int turnCounter = 0;
    public BattleState battleState = BattleState.NoCombat;

    private void Update()
    {
        if(battleState == BattleState.NoCombat)
        {
            return;
        }
        if (battleState == BattleState.DrawStep)
        {
            DrawStep();
        }
        else if (battleState == BattleState.Combat)
        {
            Combat();
        }
        else if (battleState == BattleState.EndStep)
        {
            DrawStep();
        }



    }
    public void StartBattle(PlayerStats p, PlayerStats e)
    {
        player = p;
        enemy = e;
        for(int i = 0; i < 4; i++)
        {
            player.DrawCard();
            enemy.DrawCard();
        }
        battleUI.StartBattle();
        turnCounter = 0;
        battleState = BattleState.DrawStep;
    }
    public void DrawStep()
    {
        Debug.Log("Draw step");
        player.DrawCard();
        enemy.DrawCard();
        battleUI.DisplayHands();
        
        battleState = BattleState.Combat;
    }
    public void Combat()
    {
        if (playerCard == null)
        {
            return;
        }
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
            else
            {
                enemy.PlayCard(enemyCard, player);
            }
        }
        else if (enemyFirst)
        {
            enemy.PlayCard(enemyCard, player);
            if (CheckDead(player))
            {
                Debug.Log("Enemy wins");
            } 
            else
            {
                player.PlayCard(playerCard, enemy);

            }
        }
        else
        {
            // Simultaneous
            player.PlayCard(playerCard, enemy);
            enemy.PlayCard(enemyCard, player);
            bool playerDead = CheckDead(player);
            bool enemyDead = CheckDead(enemy);

            if(playerDead && enemyDead)
            {
                Debug.Log("Draw");
            }
            else if (enemyDead)
            {
                Debug.Log("Player Wins");
            }
            else if (playerDead)
            {
                Debug.Log("Enemy Wins");
            }
        }

        battleUI.DisplayHands();
        battleState = BattleState.EndStep;
        playerCard = null;
        enemyCard = null;
        
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

        int randomIndex = Random.Range(0, enemy.hand.Count);
        enemyCard = enemy.hand[randomIndex];
    }
}
