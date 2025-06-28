using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardDatabaseSO cardDatabase;
    public PackDatabaseSO packDatabase;
    public Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = new Inventory(cardDatabase, packDatabase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
