using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    
    public event EventHandler OnBulletChange_Left;
    public event EventHandler OnBulletChange_Right;
    public event Action OnClicked;
    public event Action OnExit;


    private PlayerInputActions playerInputActions;
    private bool isFire;
    private Vector3 lastMousePosition;
    [SerializeField]
    private LayerMask placementLayermask;
    


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.BulletChangeLeft.started += ChangeBulletLeftPerformed;
        playerInputActions.Player.BulletChangeRight.started += ChangeBulletRightPerformed;


        playerInputActions.Player.Fire.started += FirePerformed;
        playerInputActions.Player.Fire.canceled += FireCanceled;

    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public bool GetIsAttack()
    {
        return isFire;
    }
    private void FirePerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Peformed");
        isFire = true;
    }

    private void FireCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("Canceled");
        isFire = false;
    }

    private void ChangeBulletLeftPerformed(InputAction.CallbackContext context)
    {
        OnBulletChange_Left?.Invoke(context, EventArgs.Empty);
        //Debug.Log("Change weapon Left");
    }

    private void ChangeBulletRightPerformed(InputAction.CallbackContext context)
    {
        OnBulletChange_Right?.Invoke(context, EventArgs.Empty);
        //Debug.Log("Change weapon Right");
    }

    public Vector2 GetMovementVectorNormailized()
    {
        Vector2 inputVector = playerInputActions.Player.MoveView.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        
        Vector3 mousePosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;      
        

        if(Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastMousePosition = hit.point;
        }


        return lastMousePosition;
    }


}
