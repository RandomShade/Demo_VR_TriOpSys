using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TelportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private XRRayInteractor _rayInteractor;
    [SerializeField] private TeleportationProvider _TPProvider;
    private InputAction _thumbStick;
    private bool _isActive;

    void Start()
    {
        _rayInteractor.enabled = false;

        InputAction activate = _actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        InputAction cancel = _actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbStick = _actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        _thumbStick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive) return;

        if (_thumbStick.triggered) return;

        if (!_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            _rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        _TPProvider.QueueTeleportRequest(request);
        _isActive = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        _rayInteractor.enabled = true;
        _isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        _rayInteractor.enabled = false;
        _isActive = false;
    }
}
