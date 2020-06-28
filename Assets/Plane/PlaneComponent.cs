using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneComponent : MonoBehaviour {
    [SerializeField] private Rigidbody2D planeRigidbody = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip putSound = null;
    [SerializeField] private AudioClip pushSound = null;
    [SerializeField] private float throwRangeFrom = 0f;
    [SerializeField] private float throwRangeTo = 0f;
    [SerializeField] private float angularVelocityRangeFrom = 0f;
    [SerializeField] private float angularVelocityRangeTo = 0f;
    [SerializeField] private Transform directionPoint1 = null;
    [SerializeField] private Transform directionPoint2 = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float accuracy = 0f;
    
    private bool _isMouseDown = false;
    private Vector2 _mouseDelta = Vector2.zero;
    private Vector2 _startLocalPosition = Vector2.zero;

    public bool IsMoving => _isMoving;
    private bool _isMoving = false;
    public bool CanMove = true;
    public bool CanPut = true;

    private Vector2 CurPosition => new Vector2(transform.position.x, transform.position.y);
    private Vector2 CurLocalPosition => new Vector2(transform.localPosition.x, transform.localPosition.y);
    // private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private Vector2 MousePosition => GetMousePosition();

    private Vector2 GetMousePosition() {
        var z = 0;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        var res = ray.GetPoint(distance);
        return new Vector2(res.x, res.y);
    }

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
        if(!CanPut) return;
        if(!_isMoving) return;
        Debug.Log("stop");
        
        _isMoving = false;
        audioSource.PlayOneShot(putSound);
        // Airplane.OnComponentStopMoving?.Invoke(this);
        Airplane.Instance.CurHeightValue += 0.75f;
        var pos = transform.parent.TransformPoint(_startLocalPosition);
        planeRigidbody.MovePosition( pos);
        planeRigidbody.MoveRotation(0);

        planeRigidbody.velocity = Vector2.zero;
        planeRigidbody.angularVelocity = 0;
        Debug.Log("start coroutine");
        StartCoroutine(StartPushTimer());
    }

    private void Update() {
        if(!_isMouseDown) return;
        planeRigidbody.MovePosition(MousePosition + _mouseDelta);
    }

    private void Push() {
        Debug.Log("Push");
        // if(gameObject.name == "wing_down") Debug.Log($"{gameObject.name} : {CanMove}");
        if (CanMove) {
            _isMoving = true;
            audioSource.PlayOneShot(pushSound);
            // Airplane.OnComponentStartMoving?.Invoke(this);
            Airplane.Instance.CurHeightValue -= 1;
            planeRigidbody.AddForce(GetRandomDirection() * speed, ForceMode2D.Impulse);
            planeRigidbody.angularVelocity = GetRandomAngularVelocity();
            Camera.main.transform.DOShakePosition(3, 0.3f, 3, 90);
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

    private float GetRandomAngularVelocity() {
        float k = Random.value > 0.5f ? 1 : -1;
        return Random.Range(angularVelocityRangeFrom, angularVelocityRangeTo) * k;
    }
}