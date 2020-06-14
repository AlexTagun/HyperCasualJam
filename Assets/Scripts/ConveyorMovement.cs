using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    
    [SerializeField] private GenerationObjectOnBoard _generationObjectOnBoard;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private List<GameObject> _partsConveyor = new List<GameObject>();
    [SerializeField] private List<GameObject> _objectsOnConveyoe = new List<GameObject>();
    [SerializeField] private AnimationCurve _speedConveyorOnTimeCurve = null;
    [SerializeField] private Vector2 _directionMove;
    //[SerializeField] private float _speedMove;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera.WorldToViewportPoint(_partsConveyor[1].transform.position).x < 0)
        {
            MoveBoardToEnd();
        }
        if (_camera.WorldToViewportPoint(_objectsOnConveyoe[0].transform.position).x < 0)
        {
            GameObject objectBehindScreen = _objectsOnConveyoe[0];
            _objectsOnConveyoe.Remove(_objectsOnConveyoe[0]);

            var theNail = objectBehindScreen.GetComponent<NailObject>();
            if (theNail)
                theNail.finalize();

            Destroy(objectBehindScreen);
        }
        if (_camera.WorldToViewportPoint(_objectsOnConveyoe[_objectsOnConveyoe.Count - 1].transform.position).x < 1)
        {
            _objectsOnConveyoe.Add(_generationObjectOnBoard.GenerationRandomObjectAtRandomInterval(_objectsOnConveyoe[_objectsOnConveyoe.Count - 1].transform));
        }
    }

    private void FixedUpdate()
    {
        MoveConveyor();
    }

    private void MoveConveyor()
    {
        _rigidbody2D.velocity = _directionMove *  _speedConveyorOnTimeCurve.Evaluate(Time.time); ;
    }
    public void MoveBoardToEnd()
    {
        var boardBehindScreen = _partsConveyor[0];
        _partsConveyor.Remove(boardBehindScreen);
        boardBehindScreen.transform.position = new Vector2(_partsConveyor[_partsConveyor.Count - 1].transform.position.x + boardBehindScreen.transform.localScale.x * boardBehindScreen.GetComponent<BoxCollider2D>().size.x, boardBehindScreen.transform.position.y);
        _partsConveyor.Add(boardBehindScreen);
    }
    private void SpawnObject()
    {
        var LastObject = _objectsOnConveyoe[_objectsOnConveyoe.Count - 1];

        _objectsOnConveyoe.Add(_generationObjectOnBoard.GenerationRandomObjectAtRandomInterval(_objectsOnConveyoe[_objectsOnConveyoe.Count - 1].transform));
        _objectsOnConveyoe.Remove(LastObject);
    }
}
