using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject SettingsPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    private PAnimationController _animationController;

    void Start()
    {
        //_animationController.DisableAnimator();
        //GetComponent<PController>().StopMovement();


        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        SettingsPanel.SetActive(false);

        levelText.text = "Level "+ (ChunkManager.Instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallBack;

        if (PController.instance != null)
        {
            _animationController = PController.instance.GetComponent<PAnimationController>();
        }


    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;

    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        
            ShowGameOver();

        else if (gameState == GameManager.GameState.LevelComplete)
            ShowLevelComplete();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        // Activate player animations
        if (_animationController != null)
        {
            _animationController.ActivateAnimator();  // Start animations here
            //GetComponent<PController>().CanMove();

        }
        else
        {
            Debug.LogError("PAnimationController is not assigned.");
        }

        if (PController.instance != null)
        {
            PController.instance.EnableMovement();
        }
       
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

    }

    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false );
        levelCompletePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        if (PController.instance == null || ChunkManager.Instance == null)
        {
            Debug.LogError("PController or ChunkManager is null.");
            return;
        }

        float progress = PController.instance.transform.position.z / ChunkManager.Instance.GetFinishZ();
        progressBar.value = progress;
    }

    public void ShowSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    } 
    public void HideSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }
}
