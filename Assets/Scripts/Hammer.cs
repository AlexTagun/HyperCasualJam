using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private GameObject centerOfMass;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CircularSaw")
        {
           EventManager.HandleOnEndGame();
        }
    }
}
