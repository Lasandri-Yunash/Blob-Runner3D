using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PController : MonoBehaviour
{
    public static PController instance;

    [SerializeField] private float speed = 2f;  // Movement speed
    [SerializeField] private float rotationSpeed = 5f;  // Rotation speed

    [SerializeField] private Vector2 movementLimit = Vector2.one;  // Movement limit (X-axis)


    [SerializeField] private GameObject model = null;  // Model reference

    private PAnimationController _animationController = null;
    private MeshRenderer _renderer = null;
    private Player _player = null;

    [SerializeField] private UIManager uiManager;


    private bool _canMove = false;  // Control movement
    private Vector2 _direction = Vector2.zero;  // Movement direction


    private Vector3 _lastPosition;
    private float _timeSinceLastMove = 0f;
    private float _movementCheckDelay = 5f;


    public PlayerState currentState { get; private set; } = PlayerState.OnStandRun;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // Get reference to Cinemachine virtual camera and follow the player
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera not found in the scene.");
        }
    }

    private void Start()
    {
        // Ensure player and animation controller are correctly assigned
        _player = GetComponent<Player>();
        _animationController = GetComponent<PAnimationController>();
        _renderer = GetComponentInChildren<MeshRenderer>();

        if (_player == null)
        {
            Debug.LogError("Player component not found on the object.");
        }

        if (_animationController == null)
        {
            Debug.LogError("PAnimationController component not found.");
        }

        _lastPosition = transform.position;
        // Begin the coroutine to determine animation state based on broken body parts
        StartCoroutine(DetermineAnimationStateViaBrokenParts());
    }

    private void FixedUpdate()
    {
        if (CanMove())
        {
            // Handle Movement
            var direction = new Vector3(_direction.x * speed, 0, speed);
            transform.position += direction * Time.fixedDeltaTime;

            // Handle Rotation
            var currRotation = transform.rotation;
            var targetRotation = Quaternion.Euler(0, Mathf.Atan2(_direction.x, _direction.y) * 90 / Mathf.PI, 0);
            transform.rotation = Quaternion.Lerp(currRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }

        ValidateLocation();
        CheckIfPlayerIsMoving();
    }

    private void CheckIfPlayerIsMoving()
    {
        if (transform.position == _lastPosition)
        {
            // Player hasn't moved, start counting time
            _timeSinceLastMove += Time.fixedDeltaTime;

            if (_timeSinceLastMove >= _movementCheckDelay)
            {
                TriggerGameOver(); // Trigger game over if player hasn't moved
            }
        }
        else
        {
            // Player has moved, reset the timer
            _timeSinceLastMove = 0f;
            _lastPosition = transform.position;
        }
    }


    private IEnumerator DetermineAnimationStateViaBrokenParts()
    {
        if (_player == null) yield break;

        var leftLegUpper = _player.FindBodyPart(BodyPartState.LeftLegUpper);
        var rightLegUpper = _player.FindBodyPart(BodyPartState.RightLegUpper);

        while (true)
        {
            if (_player.HasDead) break;

            yield return new WaitForSeconds(0.1f);

            if (leftLegUpper.HasBroken && rightLegUpper.HasBroken)
            {
                UpdateState(PlayerState.OnCrawlRun);
                _animationController.StartCrawlWalkAnimation();
            }
            else if (leftLegUpper.HasBroken && !rightLegUpper.HasBroken)
            {
                UpdateState(PlayerState.OnRightRun);
                _animationController.StartRightWalkAnimation();
            }
            else if (!leftLegUpper.HasBroken && rightLegUpper.HasBroken)
            {
                UpdateState(PlayerState.OnLeftRun);
                _animationController.StartLeftWalkAnimation();
            }
            else
            {
                UpdateState(PlayerState.OnStandRun);
                _animationController.StartStandAnimation();
            }
        }
    }

    private void TriggerGameOver()
    {
        // Call the GameOver method in the UIManager to activate the game over panel
        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }

        Debug.Log("Game Over: Player did not move.");
    }


    private void UpdateState(PlayerState newState)
    {
        currentState = newState;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    public void StopMovement()
    {
        _canMove = false;
    }

    public bool CanMove()
    {
        return _canMove;
    }

    private void OnDragged(Vector2 direction)
    {
        _direction = direction;
    }

    private void OnReleased()
    {
        _direction = Vector2.zero;
    }

   


    private void ValidateLocation()
    {
        var currentLocation = transform.position;

        if (currentLocation.x >= movementLimit.y)
        {
            currentLocation.x = movementLimit.y;
            _direction = Vector2.zero;
        }
        else if (currentLocation.x <= movementLimit.x)
        {
            currentLocation.x = movementLimit.x;
            _direction = Vector2.zero;
        }

        transform.position = currentLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AFinish"))
        {
            JellyGameManager.Instance.RestartGame(0);
        }
    }

    private void OnEnable()
    {
        Joystick.OnJoystickDrag += OnDragged;
        Joystick.OnJoystickPress += OnPressed;
        Joystick.OnJoystickRelease += OnReleased;
    }

    private void OnDisable()
    {
        Joystick.OnJoystickDrag -= OnDragged;
        Joystick.OnJoystickPress -= OnPressed;
        Joystick.OnJoystickRelease -= OnReleased;
    }

    private void OnPressed()
    {
        // Implement joystick press behavior if necessary
    }
}
