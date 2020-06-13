using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneComponent : MonoBehaviour {
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _isMouseDown = false;
    private Vector2 _mouseDelta;

    private Vector2 CurPosition => new Vector2(transform.position.x, transform.position.y);
    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    


    private void OnMouseDown() {
        _isMouseDown = true;
        _mouseDelta = CurPosition - MousePosition;
    }
    
    private void OnMouseUp() {
        _isMouseDown = false;
    }

    private void Update() {
        if(!_isMouseDown) return;
        _rigidbody.MovePosition(MousePosition + _mouseDelta);
    }
}