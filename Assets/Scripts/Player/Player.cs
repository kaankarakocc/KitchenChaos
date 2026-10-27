using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [Header("Elements")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private KitchenGameManager kitchenGameManager;
    private BaseCounter selectedCounter;
    #region Player Pyhsicsals
    float playerHeight = 2f;
    float playerRadius = .7f;
    float distance;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTransformRationSpeed = 10f;
    private Vector3 direction;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    #endregion
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenObject kitchenObject;
    public event EventHandler OnPickedSomething;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameInput.OnInteractAction += GameInput_InteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void OnDestroy()
    {
        gameInput.OnInteractAction -= GameInput_InteractAction;
        gameInput.OnInteractAlternateAction -= GameInput_OnInteractAlternateAction;
    }

    void Update()
    {
        HandlePlayerMovement();
        HandleInteractions();
    }

    #region Player GameInput Interaction Action
    private void GameInput_InteractAction(object sender, EventArgs e)
    {
        if (kitchenGameManager.IsGamePaused())
        {
            return;

        }
        if (kitchenGameManager.IsGamePlaying())
        {
            if (this.selectedCounter != null)
            {
                this.selectedCounter.Interact(this);
            }
        }
    }
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (kitchenGameManager.IsGamePaused())
        {
            return;

        }
        if (kitchenGameManager.IsGamePlaying())
        {
            if (this.selectedCounter != null)
            {
                this.selectedCounter.InteractAlternate(this);
            }
        }
    }

    #endregion

    #region Selected Counter Event Class
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    #endregion

    #region Set Selected Counter and Invoke
    private void SetSelectedCounter(BaseCounter counter)
    {
        if (this.selectedCounter == counter) return;
        this.selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });
    }
    #endregion

    #region Player Handle Interactions
    public void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();

        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);

        float distanceRayCast = 2f;

        if (direction != Vector3.zero)
        {
            lastInteractDirection = direction;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHitInfo, distanceRayCast, layerMask))
        {
            if (raycastHitInfo.transform.TryGetComponent(out BaseCounter counter))
            {
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    #endregion

    #region Player Walk Check
    public bool IsWalking()
    {
        if (direction.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        return isWalking;
    }

    #endregion

    #region PlayerMovement
    public void HandlePlayerMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();

        direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        // Player smoothly movement transforms
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTransformRationSpeed * Time.deltaTime);
        }

        // Player x = v.t => x = distance
        distance = moveSpeed * Time.deltaTime;

        // Check collions on around for Player moving
        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + (Vector3.up * playerHeight),
            playerRadius, direction,
            out RaycastHit hit,
            distance);

        // if player cant move check X and Z coordinates can moveable ? 
        if (!canMove)
        {
            // Check Player can only move X
            Vector3 directionX = new Vector3(inputVector.x, 0f, 0f).normalized;

            canMove = directionX != Vector3.zero && !Physics.CapsuleCast(
                transform.position,
                transform.position + (Vector3.up * playerHeight),
                playerRadius,
                directionX,
                distance);

            if (!canMove)
            {
                // Check only move Z
                Vector3 directonZ = new Vector3(0f, 0f, inputVector.y).normalized;

                canMove = directonZ != Vector3.zero && !Physics.CapsuleCast(
                    transform.position,
                    transform.position + (Vector3.up * playerHeight),
                    playerRadius,
                    directonZ,
                    distance);

                if (!canMove)
                {

                }
                else
                {
                    // Check Player can only move Z
                    transform.position += directonZ * Time.deltaTime * moveSpeed;
                }
            }
            else
            {
                // Player can move only X
                transform.position += directionX * Time.deltaTime * moveSpeed;
            }

        }
        else
        {
            // Player can move all coordinates
            transform.position += direction * Time.deltaTime * moveSpeed;

        }


    }
    #endregion

    #region Interface Standart Methods
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return holdPoint;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    #endregion
}
