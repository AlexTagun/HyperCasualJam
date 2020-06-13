using UnityEngine;

public class Hammer : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "CircularSaw")
           EventManager.HandleOnEndGame();
    }
}
