using UnityEngine;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoresKey = "highscores";

    public static HighScoreManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Create instance!");
        }
        else
        {
            Instance = this;
        }
    }

    // All highscores are saved in a single long string. 
    public void SaveHighScore(string playerName, int score)
    {
        List<HighScoreEntry> highScores = GetHighScores();
        HighScoreEntry newEntry = new HighScoreEntry(playerName, score);
        highScores.Add(newEntry);

        // Sort high scores in descending order
        highScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Convert high scores list to a string
        string highScoresString = "";
        foreach (HighScoreEntry entry in highScores)
        {
            highScoresString += entry.playerName + ":" + entry.score + ";";
        }

        // Save the high scores string to PlayerPrefs
        PlayerPrefs.SetString(HighScoresKey, highScoresString);
        PlayerPrefs.Save();
    }

    // Retrieve the long highscore string, split it to get every individual user with score to a list.
    public List<HighScoreEntry> GetHighScores()
    {
        List<HighScoreEntry> highScores = new List<HighScoreEntry>();

        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            string highScoresString = PlayerPrefs.GetString(HighScoresKey);
            string[] entries = highScoresString.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string entry in entries)
            {
                string[] entryData = entry.Split(':');
                if (entryData.Length == 2)
                {
                    string playerName = entryData[0];
                    int score = int.Parse(entryData[1]);
                    highScores.Add(new HighScoreEntry(playerName, score));
                }
            }
        }

        return highScores;
    }
}

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;

    public HighScoreEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}