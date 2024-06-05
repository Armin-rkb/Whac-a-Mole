using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private HighScoreUI highScoreUI; 
    [SerializeField] private TMP_InputField nameInputField; 
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private Image backgroundImage;

    private void Awake()
    {
        nextButton.onClick.AddListener(ContinueToHighscores);
        HideEndScreen();
    }
    
    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
    }

    public void HideEndScreen()
    {
        backgroundImage.enabled = false;
        gameEndScreen.SetActive(false);
    }
    
    public void ShowEndScreen()
    {
        scoreText.text = GameScoreManager.Instance.GetScore().ToString();
        backgroundImage.enabled = true;
        gameEndScreen.SetActive(true);
    }

    public void ContinueToHighscores()
    {
        HighScoreManager.Instance.SaveHighScore(nameInputField.text, GameScoreManager.Instance.GetScore());
        highScoreUI.ShowHighScores();

        gameObject.SetActive(false);
    }
}
