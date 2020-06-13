using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneComponent : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float throwRangeFrom;
    [SerializeField] private float throwRangeTo;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

    private bool _isMouseDown = false;
    private Vector2 _mouseDelta;

    private Vector2 CurPosition => new Vector2(transform.position.x, transform.position.y);
    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Start() {
        Debug.Log("Push");
        StartCoroutine(StartPushTimer());
    }

    private void OnMouseDown() {
        _isMouseDown = true;
        _mouseDelta = CurPosition - MousePosition;
    }
    
    private void OnMouseUp() {
        _isMouseDown = false;
        rigidbody.velocity = Vector2.zero;
        StartCoroutine(StartPushTimer());
    }

    private void Update() {
        if(!_isMouseDown) return;
        rigidbody.MovePosition(MousePosition + _mouseDelta);
    }

    private void Push() {
        Debug.Log("Push");
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private IEnumerator StartPushTimer() {
        Debug.Log("Push");
        var timeToPush = Random.Range(throwRangeFrom, throwRangeTo);
        yield return new WaitForSeconds(timeToPush);
        // yield return new WaitForSeconds(0f);
        Debug.Log("Push");
        Push();
    }
}