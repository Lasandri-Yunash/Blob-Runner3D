using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayButtonPressed()
    {
       // GameManager.Instance.SetGameState(GameManager.GameState.Game);
        SceneManager.LoadScene(1);

    }
}
