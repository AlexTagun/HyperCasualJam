using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private ConveyorMovement _conveyorMovement;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera.WorldToViewportPoint(transform.position).x < 0)
        {
            if (Object.ReferenceEquals(gameObject, _conveyorMovement.PartsConveyor[1]))
            {
                _conveyorMovement.MoveBoardToEnd();
            }
        }
    }
}
