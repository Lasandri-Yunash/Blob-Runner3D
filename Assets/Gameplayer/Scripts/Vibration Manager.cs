using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{

    [Header("Setting")]
    private bool haptics;
    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorsHit += Vibrate;
        Enemy.onRunnerDied += Vibrate;
        GameManager.onGameStateChanged += GameStateChangedCallBack;


    }
    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= Vibrate;
        Enemy.onRunnerDied -= Vibrate;

        GameManager.onGameStateChanged -= GameStateChangedCallBack;


    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelComplete)

            Vibrate();

        else if (gameState == GameManager.GameState.GameOver)
            Vibrate();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Vibrate()
    {
        if(haptics)
            Taptic.Light();
    }


    public void DisableVibration()
    {
        haptics = false;
    } 
    
    public void EnableVibration()
    {
        haptics = true;
    }
}
