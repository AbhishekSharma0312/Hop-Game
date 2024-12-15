using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] CameraFollow cameraFollowScript;
    [SerializeField] PlayerController playerController;
    [SerializeField] Ball ballScript;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GUIManager guiManager;
    [SerializeField] Vector3 startPos, offset;

    public delegate void SetComboAnimation(bool isCombo);
    public event SetComboAnimation updateComboAnimation;

    private int score, highScore;
    private int combo;
    private bool isCombo;
    public bool IsCombo
    {
        get
        {
            return isCombo;
        }

        set
        {
            isCombo = value;
            updateComboAnimation?.Invoke(isCombo);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        score = 0;
        combo = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;

        for (int i = 0; i < 4; i++)
        {
            SpawnBlock();
        }

        IsCombo = false;

    }

    public void SpawnBlock()
    {
        startPos += offset;
        GameObject tempBlock = Instantiate(blockPrefab);
        float xpos = Random.Range(-8, 8f);
        tempBlock.transform.position = startPos + new Vector3(xpos, 0, 0);
    }

    public void UpdateScore(GameObject currentBlock)
    {
        Vector3 playerpos = playerController.transform.position;
        Vector3 blockpos = currentBlock.transform.position;

        if (Mathf.Abs(playerpos.x - blockpos.x) < 1.5f)
        {
            combo++;
            IsCombo = true;
        }
        else
        {
            combo = 1;
            IsCombo = false;
        }

        score += combo;
        guiManager.UpdateInGameScore(score);
    }

    public void GameStart()
    {
        playerController.isGameStarted = true;
        ballScript.isGameStarted = true;
    }

    public void GameOver()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        guiManager.UpdateGameResultScores(score, highScore);

        cameraFollowScript.canFollow = false;
    }

    public void QuitGame()
    {

    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
}