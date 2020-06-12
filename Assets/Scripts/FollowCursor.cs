using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
