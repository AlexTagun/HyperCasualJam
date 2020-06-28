using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera = null;
    private Rigidbody2D _rigidbody2D = null;

    [SerializeField] private float moveSpeed = 0f;

    private Vector3 mousePosition = Vector3.zero;
    private Vector2 position = Vector2.zero;
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
