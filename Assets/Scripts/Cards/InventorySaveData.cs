using System.Collections.Generic;

[System.Serializable]
public class DeckSaveData
{
    public string title;
    public List<string> cardIDs = new List<string>(); // store CardSO guids
}

[System.Serializable]
public class CardSaveData
{
    public string cardID;
    public int count;
    public bool discovered;
}

[System.Serializable]
public class PackSaveData
{
    public string packID;
    public int count;
    public bool discovered;
}

[System.Serializable]
public class InventorySaveData
{
    public List<CardSaveData> cards = new List<CardSaveData>();
    public List<PackSaveData> packs = new List<PackSaveData>();
    public List<DeckSaveData> decks = new List<DeckSaveData>();
}
