using UnityEngine;
using TMPro;

public class GameScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString(); 
    }
}
