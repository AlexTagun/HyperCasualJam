using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private List<GameObject> _partsConveyor = new List<GameObject>();
    public List<GameObject> PartsConveyor => _partsConveyor;
    [SerializeField] private Vector2 _directionMove;
    [SerializeField] private float _speedMove;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveConveyor();
    }

    private void MoveConveyor()
    {
        _rigidbody2D.velocity = _directionMove * _speedMove;
    }
    public void MoveBoardToEnd()
    {
        var boardBehindScreen = _partsConveyor[0];
        _partsConveyor.Remove(boardBehindScreen);
        boardBehindScreen.transform.position = new Vector2(_partsConveyor[_partsConveyor.Count - 1].transform.position.x + boardBehindScreen.transform.localScale.x * boardBehindScreen.GetComponent<BoxCollider2D>().size.x, boardBehindScreen.transform.position.y);
        _partsConveyor.Add(boardBehindScreen);










    }
}
