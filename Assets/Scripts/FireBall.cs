using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1.0f;
    public Player.PlayerType fbPlayerType;
    public Vector2 fireBallDirection = Vector2.zero;
    public bool isHit = false;

    void Update()
    {
        if (!isHit)
            transform.Translate(fireBallDirection * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            if(fbPlayerType != collision.GetComponent<Player>().playerType)
            {
                isHit = true;
                GetComponent<Animator>().SetBool("FireBallEnd", true);
                if (collision.GetComponent<Player>().health > 0)
                {
                    if (!collision.GetComponent<Player>().block)
                    {
                        collision.GetComponent<Player>().health -= 20;
                        collision.GetComponent<Player>().GetHurt();
                        collision.GetComponent<Player>().fireBallHitSound.Play();
                    }
                    else
                    {
                        collision.GetComponent<Player>().health -= 2;
                        collision.GetComponent<Animator>().SetTrigger("BlockHurt");
                        collision.GetComponent<Player>().blockSound.Play();
                    }                        
                    if (collision.GetComponent<Player>().health <= 0)
                    {
                        collision.gameObject.GetComponent<Player>().Dying();
                    }
                    Invoke(nameof(DestroyThis), 0.5f);
                }                
            }            
        }
        else if (collision.CompareTag("Wall"))
        {
            isHit = true;
            GetComponent<Animator>().SetBool("FireBallEnd", true);
            Invoke(nameof(DestroyThis), 0.5f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
