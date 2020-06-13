using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneComponent : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float throwRangeFrom;
    [SerializeField] private float throwRangeTo;
    [SerializeField] private Transform directionPoint1;
    [SerializeField] private Transform directionPoint2;
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
        rigidbody.AddForce(GetRandomDirection() * speed, ForceMode2D.Impulse);
    }

    private IEnumerator StartPushTimer() {
        Debug.Log("Push");
        var timeToPush = Random.Range(throwRangeFrom, throwRangeTo);
        yield return new WaitForSeconds(timeToPush);
        // yield return new WaitForSeconds(0f);
        Debug.Log("Push");
        Push();
    }

    private Vector2 GetRandomDirection() {
        var direction1 = (directionPoint1.position - transform.position).normalized;
        var direction2 = (directionPoint2.position - transform.position).normalized;
        return new Vector2(Random.Range(direction1.x, direction2.x), Random.Range(direction1.y, direction2.y));
    }
}