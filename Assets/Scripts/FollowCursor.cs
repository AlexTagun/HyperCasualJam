using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float moveSpeed;

    private Vector3 mousePosition;
    private Vector2 position;
    void Start()
    {
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(position);
    }
}
