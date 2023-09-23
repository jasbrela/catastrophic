using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandRotation : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private Transform hand;
    
    [SerializeField] private Transform flip;
    private Vector3 _defaultScale;

    void Start()
    {
        input.actions["Rotation"].performed += Rotate;
        _defaultScale = flip.localScale;

    }

    private void Rotate(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = ctx.ReadValue<Vector2>();
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        var dir = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        Debug.Log(angle);
        bool reverse = angle > 90 || angle < -90;
        
        if (reverse) {
            // Reverse the angle for the flip effect
            angle += 180f;
        }
        
        hand.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        flip.localScale = new Vector3(_defaultScale.x * (reverse ? -1 : 1), _defaultScale.y, _defaultScale.z);

        //transform.right = worldPos - transform.position;

        //hand.rotation = Quaternion.LookRotation(worldPos);
    }
}
