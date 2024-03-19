using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _AllowedToClickLayers;

    public event Action<Vector3> PositionClicked;

    public event Action<Vector3> MouseHoverPositionChanged;

    private Camera _mainCam;

   
    private bool ShootRayAtPosition(Vector2 positionInput, out Vector3 worldPosition)
    {
        Vector3 worldPos=  _mainCam.ScreenToWorldPoint(positionInput);

        if (Physics2D.OverlapCircle(worldPos, _radius, _AllowedToClickLayers) != null)
        {
            worldPosition = worldPos;

            return true;
        }
        else
        {

            worldPosition = Vector3.zero;

            return false;
        }
    }

    private void Start()
    {
        _mainCam = Camera.main;

       
    }

    private void OnEnable()
    {
        _playerInput.actions["Click"].performed += ClickPosition;
    }

    private void OnDisable()
    {
        _playerInput.actions["Click"].performed -= ClickPosition;
    }
    private void Update()
    {
     
       MouseHoverPositionChanged?.Invoke( _mainCam.ScreenToWorldPoint(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));
       
    }

    private void ClickPosition(InputAction.CallbackContext context)
    {
        Vector3 worldPosition;

        if (ShootRayAtPosition(_playerInput.actions["SetPosition"].ReadValue<Vector2>(), out worldPosition))
        {
            PositionClicked?.Invoke(worldPosition);
        }

    }
}
