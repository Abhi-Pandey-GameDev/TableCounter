using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool hasLeftTable = false;
    public string currentTable = ""; 

    public void OnPicked()
    {
        hasLeftTable = false;
    }

    public void OnLeftTable()
    {
        hasLeftTable = true;
    }

    public void OnPlaced(string tableTag)
    {
        currentTable = tableTag;
        hasLeftTable = false;
    }
}
