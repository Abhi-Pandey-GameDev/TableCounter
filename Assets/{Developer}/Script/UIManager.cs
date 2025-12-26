using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject gamePlayPanel;

    [Header("Game Root")]
    [SerializeField] private GameObject gameRoot;

    [Header("Table UI")]
    [SerializeField] private TMP_Text table1Text;
    [SerializeField] private TMP_Text table2Text;

    private int table1Count = 0;
    private int table2Count = 0;

    private void Start()
    {
        ShowHome();
    }

    public void ShowHome()
    {
        homePanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        gameRoot.SetActive(false);
    }

    public void ShowGame()
    {
        homePanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        gameRoot.SetActive(true);
    }


    public void PlayBtn()
    {
        ShowGame();
    }

    public void BackBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddToTable(string tableTag, int value)
    {
        if (tableTag == "Table1")
        {
            table1Count += value;
        }
        else if (tableTag == "Table2")
        {
            table2Count += value;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        table1Text.text = "Table1: " + table1Count;
        table2Text.text = "Table2: " + table2Count;
    }

}
