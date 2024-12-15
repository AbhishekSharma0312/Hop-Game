using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject inGameHUDScreen;
    [SerializeField] GameObject gameResultScreen;

    [SerializeField] TextMeshProUGUI currentScoreValue;
    [SerializeField] TextMeshProUGUI finalScoreValue;
    [SerializeField] TextMeshProUGUI highScoreValue;


    // send score value here to update ingame score.
    public void UpdateInGameScore(int value)
    {
        currentScoreValue.text = value.ToString();
    }

    public void UpdateGameResultScores(int _finalScoreValue, int _highScoreValue)
    {
        inGameHUDScreen.SetActive(false);
        gameResultScreen.SetActive(true);

        finalScoreValue.text = _finalScoreValue.ToString();
        highScoreValue.text = _highScoreValue.ToString();
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit button clicked");
        GameManager.Instance.GameRestart();
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button clicked");
        GameManager.Instance.GameStart();
    }
}