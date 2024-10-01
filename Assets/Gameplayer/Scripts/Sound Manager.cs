using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDietSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameovertSound;


    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangedCallBack;

        Enemy.onRunnerDied += PlayRunnerDieSound;

    }


    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallBack;

        Enemy.onRunnerDied -= PlayRunnerDieSound;
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelComplete)

            levelCompleteSound.Play();

        else if (gameState == GameManager.GameState.GameOver)
            gameovertSound.Play();

    }
    void Update()
    {
        
    }

    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        runnerDietSound.Play();
    }

    public void DisableSounds()
    {

        doorHitSound.volume = 0;
        runnerDietSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameovertSound.volume = 0;
        buttonSound.volume = 0;

    }

    public void EnableSounds()
    {
        doorHitSound.volume = 1;
        runnerDietSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameovertSound.volume = 1;
        buttonSound.volume = 1;
    }
}
