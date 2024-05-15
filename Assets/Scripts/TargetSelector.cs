using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TargetSelector : MonoBehaviour
{
    private Camera _sceneCamera;
    [SerializeField] private LayerMask _interactableLayerMask;

    private Target _selectedTarget;
    private Vector3 _mousePosition;
    private Ray _ray;
    private float _rayHitDistance = 200;
    private Mouse _mouse;

    public Target Target => _selectedTarget;

    private void Awake()
    {
        _sceneCamera = Camera.main;
        _mouse = Mouse.current;
    }

    private void Update()
    {
        TryDeselectTarget();
        TrySelectTarget();
    }

    private void TrySelectTarget()
    {
        if (_mouse.leftButton.wasPressedThisFrame && _selectedTarget == null)
        {
            _mousePosition = _mouse.position.ReadValue();
            _ray = _sceneCamera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, _rayHitDistance, _interactableLayerMask))
            {
                if (hit.collider.TryGetComponent(out Target target))
                {
                    _selectedTarget = target;
                    target.Select();
                }
            }
        }
    }

    private void TryDeselectTarget()
    {
        if (_mouse.rightButton.wasPressedThisFrame && _selectedTarget != null)
        {
            if (_selectedTarget.TryGetComponent(out Target target))
            {
                target.Deselect();
            }
            
            _selectedTarget = null;
        }
    }
}