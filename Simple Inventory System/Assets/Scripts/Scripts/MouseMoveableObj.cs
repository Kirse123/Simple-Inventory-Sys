using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveableObj : MonoBehaviour
{
    // Distance between object's "plane" and camera 
    private float distance;

    private Camera mainCamera;
    private Vector3 mousePos;

    private Collider _collider;
    private Rigidbody _rb;

    private void Start()
    {
        mainCamera = Camera.main;
        mousePos = new Vector3();
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        // Current distance from camera to object
        distance = mainCamera.WorldToViewportPoint(gameObject.transform.position).z;

        // Stop item rotaion and set speed to 0
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        Vector3 power = (Input.mousePosition - mousePos);
        
        // Obtain mouse position
        mousePos = Input.mousePosition;
        
        _rb.MovePosition(mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)));
    }

    private void OnMouseUp()
    {
        _rb.isKinematic = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void Toggle(bool value)
    {
        _collider.enabled = value;
    }
}
