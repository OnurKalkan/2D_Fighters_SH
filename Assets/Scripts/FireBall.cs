using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1.0f;
    public Player.PlayerType fbPlayerType;
    public Vector2 fireBallDirection = Vector2.zero;

    void Update()
    {
        transform.Translate(fireBallDirection * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(fbPlayerType != collision.GetComponent<Player>().playerType)
            {
                this.gameObject.SetActive(false);
                collision.GetComponent<Player>().health -= 20;
                if (collision.GetComponent<Player>().health <= 0)
                {
                    collision.gameObject.GetComponent<Player>().Dying();
                }                    
                DestroyThis();
            }            
        }
        else if (collision.CompareTag("Wall"))
        {
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
