using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    void Start()
    {

    }


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
