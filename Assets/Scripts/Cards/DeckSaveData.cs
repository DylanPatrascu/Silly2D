using System.Collections.Generic;

[System.Serializable]
public class DeckSaveData
{
    public string title;
    public List<string> cardIDs = new List<string>(); // store CardSO guids
}

[System.Serializable]
public class DeckSaveDataWrapper
{
    public List<DeckSaveData> decks = new List<DeckSaveData>();
}
