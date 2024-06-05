using System.Collections.Generic;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private Transform highscoreContentList;
    [SerializeField] private HighscoreElement HighscoreElementPrefab;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Start()
    {
        HideHighScores();
    }

    private void HideHighScores()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowHighScores()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        CreateHighScoreList();
    }

    private void CreateHighScoreList()
    {
        if (highscoreContentList != null && highscoreContentList.childCount > 0)
        {
            for (int i = highscoreContentList.childCount - 1; i >= 0; i--)
            {
                Destroy(highscoreContentList.GetChild(i).gameObject);
            }
        }

        List<HighScoreEntry> highScores = HighScoreManager.Instance.GetHighScores();
        foreach (HighScoreEntry entry in highScores)
        {
            HighscoreElement newHighscoreElement = Instantiate(HighscoreElementPrefab, highscoreContentList);
            newHighscoreElement.SetHighscore(entry.playerName, entry.score);
        }
    }
}
