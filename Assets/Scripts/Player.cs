using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 1.0f, jumpForce = 1.0f, health = 100;
    Animator animCont;
    string[] animNames = new string[] { "Run", "Idle", "Block", "Death" };
    public bool block = false, death = false, goingRight = false, goingLeft = false, idle = true, roll = false, onGround = false, meleeRange = false;
    [HideInInspector]
    public float redStart = 0.25f, redWait = 0.4f, whiteTurn = 0.1f;
    public KeyCode leftButton, rightButton, jumpButton, blockButton, rollButton, meleeButton, specialAttackButton;//oyun ici kullandigim tuslar
    public GameObject fireBall;
    public PlayerType playerType;
    public StandingSide standingSide;
    public TextMeshPro healthText;
    public Image miniHealthBar, bigHealthBar;
    GameManager gameManager;
    public GameObject enemy;
    public AudioSource swordSound, fireBallSound, blockSound, dieSound, fireBallHitSound;

    public enum StandingSide
    {
        Left,
        Right
    }

    public enum PlayerType
    {
        PlayerOne,
        PlayerTwo
    }

    private void Awake()
    {
        animCont = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PlayAnimation(string animName)
    {
        for (int i = 0; i < animNames.Length; i++)
        {
            if (animNames[i] != animName)
                animCont.SetBool(animNames[i], false);
        }
        animCont.SetBool(animName, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStart)
        {            
            healthText.text = health.ToString();
            miniHealthBar.fillAmount = health / 100;
            bigHealthBar.fillAmount = health / 100;
            if (standingSide == StandingSide.Left)
                healthText.transform.localScale = new Vector3(1, 1, 1);
            else
                healthText.transform.localScale = new Vector3(-1, 1, 1);
            if (!gameManager.gameEnd)
            {
                if (!death)
                {
                    if (Input.GetKey(rightButton) && !block && !death && !roll)//Go Right
                    {
                        GoingRight();
                    }
                    if (Input.GetKey(leftButton) && !block && !death && !roll)//Go Left
                    {
                        GoingLeft();
                    }
                    if (Input.GetKeyDown(meleeButton))//Melee Attack
                    {
                        MeleeAttack();
                    }
                    if (Input.GetKeyDown(rollButton) && !roll)//Rolling
                    {
                        Rolling();
                    }
                    if (Input.GetKeyDown(jumpButton) && !death && !block && onGround)//Jump
                    {
                        Jumping();
                    }
                    if (Input.GetKey(blockButton) && !death)//Block
                    {
                        Blocking();
                    }
                    else
                    {
                        block = false;
                    }
                    if (Input.GetKeyDown(specialAttackButton))//Range Attack
                    {
                        FireBall();
                    }
                    if (!Input.anyKey)
                    {
                        PlayAnimation("Idle");
                    }
                }
            }
        }                                
    }

    void FireBall()//fireball spawn etme
    {
        animCont.SetTrigger("Range");
        GameObject newFireBall = Instantiate(fireBall, transform.position + new Vector3(0.5f,2,0), Quaternion.identity);
        newFireBall.GetComponent<FireBall>().fbPlayerType = playerType;
        newFireBall.SetActive(true);
        newFireBall.transform.parent = null;
        if (standingSide == StandingSide.Left)
            newFireBall.GetComponent<FireBall>().fireBallDirection = Vector2.right;
        else
            newFireBall.GetComponent<FireBall>().fireBallDirection = Vector2.left;
        fireBallSound.Play();
    }

    void GoingRight()
    {
        goingRight = true; goingLeft = false; idle = false;
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        PlayAnimation("Run");
    }

    void GoingLeft()
    {
        goingLeft = true; goingRight = false; idle = false;
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        PlayAnimation("Run");
    }

    void MeleeAttack()
    {
        animCont.SetTrigger("Melee");
        if (meleeRange)
        {
            if (enemy.GetComponent<Player>().block)
            {
                enemy.GetComponent<Player>().health -= 3;
                enemy.GetComponent<Animator>().SetTrigger("BlockHurt");
                blockSound.Play();
            }
            else
            {
                enemy.GetComponent<Player>().health -= 25;
                enemy.GetComponent<Player>().GetHurt();
                swordSound.Play();
            }
        }
        if (enemy.GetComponent<Player>().health <= 0)
        {
            enemy.gameObject.GetComponent<Player>().Dying();            
        }
    }

    void Rolling()
    {
        roll = true;
        animCont.SetTrigger("Roll");
        if (idle)
            transform.DOMoveX(transform.position.x + 3, 0.75f).SetDelay(0.1f).OnComplete(RollEnd);
        else if (goingRight)
            transform.DOMoveX(transform.position.x + 3, 0.75f).SetDelay(0.1f).OnComplete(RollEnd);
        else if (goingLeft)
            transform.DOMoveX(transform.position.x - 3, 0.75f).SetDelay(0.1f).OnComplete(RollEnd);
    }

    void RollEnd()
    {
        roll = false;
    }

    public void Dying()
    {
        dieSound.Play();
        death = true;
        PlayAnimation("Death");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Invoke(nameof(CloseText), 0.5f);
        gameManager.GameEnd();
    }

    void CloseText()
    {
        healthText.gameObject.SetActive(false);
    }

    public void GetHurt()
    {
        animCont.SetTrigger("Hurt");
        GetComponent<SpriteRenderer>().DOColor(Color.red, redStart);
        GetComponent<SpriteRenderer>().DOColor(Color.white, whiteTurn).SetDelay(redWait);
    }

    void Jumping()
    {
        animCont.SetTrigger("Jump");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;
    }

    void Idle()
    {
        goingLeft = false; goingRight = false; idle = true;
        PlayAnimation("Idle");
    }

    void Blocking()
    {
        block = true;
        PlayAnimation("Block");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponent<Player>().playerType != playerType)
        {
            meleeRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<Player>().playerType != playerType)
        {
            meleeRange = false;
        }
    }
}
