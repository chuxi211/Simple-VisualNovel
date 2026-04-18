[System.Serializable]

public class DataOfSave
{
    public string currentID { get; set; } = null;//需要从外界传入，所以public
    public bool hasData { get; private set; } = false;
    public DataOfSave(string cI)
    {
        currentID = cI;
        hasData = true;
    }
    public void DeleteData( int tI)
    {
        currentID = null;
        hasData = false;
    }
}
