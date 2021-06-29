[System.Serializable]
public class RecordsData
{   
    public float[] recordsLvlOne;
    public float[] recordsLvlTwo;
    public float[] recordsLvlThree;

    public RecordsData(GameManager gameManager)
    {
        recordsLvlOne = GameManager.RecordsLvlOne;
        recordsLvlTwo = GameManager.RecordsLvlTwo;
        recordsLvlThree = GameManager.RecordsLvlThree;
    }
}
