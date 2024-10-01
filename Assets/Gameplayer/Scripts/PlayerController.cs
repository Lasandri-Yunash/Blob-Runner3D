using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float roadWith;
    private bool canMove ;

    [Header("Control")]
    [SerializeField] private float _slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAminator playerAminator;


    private void Awake()
    {
        if (Instance != null)
        
            Destroy(gameObject);
        
        else
        
            Instance = this;
        
    }

    void Start()
    {

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;

    }
    void Update()
    {

        if (canMove)
        {
            MoveForward();
            ManageControll();

        }
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }

        else if(gameState == GameManager.GameState.GameOver || gameState == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }


    private void StartMoving()
    {
        canMove = true;
        playerAminator.Run();

    }

    private void StopMoving()
    {
        canMove = false;
        playerAminator.Idel();

    }



    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * _moveSpeed;

    }

    private void ManageControll()
    {
       if(Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;

        }

       else if(Input.GetMouseButton(0))
        {
            float xScreenDiffernce = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDiffernce /= Screen.width;
            xScreenDiffernce *= _slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDiffernce;

            position.x = Mathf.Clamp(position.x, -roadWith / 2 + crowdSystem.GetCrowdRadius(),  roadWith / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;

           // transform.position = clickedPlayerPosition + Vector3.right * xScreenDiffernce;
        }

    }
}
