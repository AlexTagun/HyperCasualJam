using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneComponent : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float throwRangeFrom;
    [SerializeField] private float throwRangeTo;
    [SerializeField] private Transform directionPoint1;
    [SerializeField] private Transform directionPoint2;
    [SerializeField] private float speed;
    [SerializeField] private float accuracy;
    
    private bool _isMouseDown = false;
    private Vector2 _mouseDelta;
    private Vector2 _startLocalPosition;

    public bool IsMoving => _isMoving;
    private bool _isMoving;
    public bool CanMove = true;

    private Vector2 CurPosition => new Vector2(transform.position.x, transform.position.y);
    private Vector2 CurLocalPosition => new Vector2(transform.localPosition.x, transform.localPosition.y);
    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Awake() {
        _startLocalPosition = CurLocalPosition;
        
        StartCoroutine(StartPushTimer());
    }

    private void Start() {
        // if(gameObject.name == "wing_down") Debug.Log($"{gameObject.name} : {CanMove}");
        Debug.DrawLine(transform.position, transform.position + Vector3.left * accuracy, Color.red, 500);
    }

    private void OnMouseDown() {
        if(!_isMoving) return;
        _isMouseDown = true;
        _mouseDelta = CurPosition - MousePosition;
    }
    
    private void OnMouseUp() {
        _isMouseDown = false;
        if((_startLocalPosition - CurPosition).magnitude > accuracy) return;
        Debug.Log(_isMoving);
        if(!_isMoving) return;
        Debug.Log("stop");
        
        _isMoving = false;
        var pos = transform.parent.TransformPoint(_startLocalPosition);
        rigidbody.MovePosition( pos);
        
        rigidbody.velocity = Vector2.zero;
        Debug.Log("start coroutine");
        StartCoroutine(StartPushTimer());
    }

    private void Update() {
        if(!_isMouseDown) return;
        rigidbody.MovePosition(MousePosition + _mouseDelta);
    }

    private void Push() {
        Debug.Log("Push");
        // if(gameObject.name == "wing_down") Debug.Log($"{gameObject.name} : {CanMove}");
        if (CanMove) {
            _isMoving = true;
            rigidbody.AddForce(GetRandomDirection() * speed, ForceMode2D.Impulse);
        } else {
            StartCoroutine(StartPushTimer());
        }
    }

    private IEnumerator StartPushTimer() {
        var timeToPush = Random.Range(throwRangeFrom, throwRangeTo);
        yield return new WaitForSeconds(timeToPush);
        Push();
    }

    private Vector2 GetRandomDirection() {
        var direction1 = (directionPoint1.position - transform.position).normalized;
        var direction2 = (directionPoint2.position - transform.position).normalized;
        return new Vector2(Random.Range(direction1.x, direction2.x), Random.Range(direction1.y, direction2.y));
    }
}