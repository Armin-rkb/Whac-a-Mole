using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreUI scoreUI;
    private int score;
    
    public static ScoreManager Instance;
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

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreUI.UpdateScoreText(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        scoreUI.UpdateScoreText(score);
    }
}
