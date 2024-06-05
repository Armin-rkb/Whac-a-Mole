using UnityEngine;
using TMPro;

public class HighscoreElement : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetHighscore(string name, int score)
    {
        nameText.text = name;
        scoreText.text = "Score: " + score.ToString();
    } 
}
