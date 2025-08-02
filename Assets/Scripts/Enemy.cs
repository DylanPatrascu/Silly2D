using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CardSO[] cardRewards;
    public PackSO[] packRewards;
    public PlayerStats enemyStats;
    public BattleController battleController;
    public bool beaten = false;

    private bool playerInRange = false;
    private PlayerMovement nearbyPlayerMovement;
    private PlayerStats nearbyPlayerStats;
    public DialogueTree tree;

    private void Awake()
    {
        enemyStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (playerInRange && !beaten && nearbyPlayerMovement != null && nearbyPlayerMovement.interact)
        {
            nearbyPlayerMovement.interact = false;

            Debug.Log("Battle triggered by interact!");

            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            dialogueManager.OnDialogueEnd = () =>
            {
                battleController.StartBattle(nearbyPlayerStats, enemyStats);
            };

            dialogueManager.StartDialogue(tree.nodes[0]);
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !beaten)
        {
            nearbyPlayerMovement = other.GetComponent<PlayerMovement>();
            nearbyPlayerStats = other.GetComponent<PlayerStats>();

            if (nearbyPlayerMovement == null || nearbyPlayerStats == null)
            {
                Debug.LogWarning("Player is missing PlayerMovement or PlayerStats component.");
                return;
            }

            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement exitingMovement = other.GetComponent<PlayerMovement>();
            if (exitingMovement == nearbyPlayerMovement)
            {
                playerInRange = false;
                nearbyPlayerMovement = null;
                nearbyPlayerStats = null;
            }
        }
    }
}
