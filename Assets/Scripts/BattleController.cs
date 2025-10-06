using UnityEngine;

public enum BattleState
{
    NoCombat = 0,
    DrawStep = 1,
    Combat = 2,
    EndStep = 3,
    BattleOver = 4
}
public class BattleController : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats enemy;

    public CardSO playerCard;
    public CardSO enemyCard;

    public BattleUI battleUI;
    public int turnCounter = -1;
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
            EndStep();
        }
    }

    public void StartBattle(PlayerStats p, PlayerStats e)
    {
        Debug.Log("BattleController instance: " + this.gameObject.name);
        Debug.Log("BattleUI reference: " + (battleUI != null ? "Assigned" : "NULL"));
        if (battleUI == null)
        {
            Debug.LogError("BattleUI is null at runtime!");
            return;
        }
        if (!battleUI.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("BattleUI object was inactive at battle start.");
        }
        player = p;
        p.StartBattle();
        enemy = e;
        e.StartBattle();
        
        player.DrawCard(5);
        enemy.DrawCard(5);
        battleUI.StartBattle();
        turnCounter = 0;
        battleState = BattleState.DrawStep;
        battleUI.DisplaySentence("Battle Started!");
    }
    public void DrawStep()
    {
        Debug.Log("Draw step");
        if (turnCounter > 0) // Only draw if not the first turn
        {
            player.DrawCard(1);
            enemy.DrawCard(1);
        }

        battleUI.DisplayHands();
        battleUI.DisplayStats();
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
            player.PlayCard(playerCard, enemy, this);
            if(CheckDead(enemy))
            {
                Debug.Log("Player wins");
            } 
            else
            {
                enemy.PlayCard(enemyCard, player, this);
            }
        }
        else if (enemyFirst)
        {
            enemy.PlayCard(enemyCard, player, this);
            if (CheckDead(player))
            {
                Debug.Log("Enemy wins");
            } 
            else
            {
                player.PlayCard(playerCard, enemy, this);

            }
        }
        else
        {
            // Simultaneous
            player.PlayCard(playerCard, enemy, this);
            enemy.PlayCard(enemyCard, player, this);
        }

        battleUI.DisplayHands();
        battleUI.DisplayStats();
        battleState = BattleState.EndStep;
        playerCard = null;
        enemyCard = null;
        
    }

    public void EndStep()
    {
        bool playerDead = CheckDead(player);
        bool enemyDead = CheckDead(enemy);

        if (playerDead && enemyDead)
        {
            player.SetHealth(1);
            battleUI.EndBattle();
            player.EndBattle();

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.EndBattleDraw();
            battleState = BattleState.NoCombat;
        }
        else if (enemyDead)
        {
            Debug.Log("Player Wins");
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.GiveRewards(player);
            battleUI.EndBattle();
            player.EndBattle();
            battleState = BattleState.NoCombat;

        }
        else if (playerDead)
        {
            Debug.Log("Enemy Wins");
            battleState = BattleState.NoCombat;
            //open death screen
            //

        }
        else
        {
            turnCounter++;
            battleState = BattleState.DrawStep;
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

        int randomIndex = Random.Range(0, enemy.hand.Count);
        enemyCard = enemy.hand[randomIndex];
    }
}
