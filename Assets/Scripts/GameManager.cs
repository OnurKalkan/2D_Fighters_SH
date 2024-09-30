using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne, playerTwo;
    public TextMeshProUGUI timerText, winnerText, counterText;
    public int time = 90;
    public bool gameEnd, gameStart, skipCounting;

    private void Start()
    {
        timerText.text = time.ToString();
        if(!skipCounting)
            StartCoroutine(CountBackToFight());
        else
        {
            StartCoroutine(Timer());
            counterText.text = "";
            gameStart = true;
        }
        playerOne.GetComponent<Player>().enemy = playerTwo;
        playerTwo.GetComponent<Player>().enemy = playerOne;
    }

    IEnumerator CountBackToFight()
    {
        float countTime = 0.75f;
        counterText.text = "3";
        counterText.transform.DOScale(Vector3.one * 1.5f, countTime);
        yield return new WaitForSeconds(countTime);
        counterText.transform.localScale = Vector3.one;
        counterText.transform.DOScale(Vector3.one * 1.5f, countTime);
        counterText.text = "2";
        yield return new WaitForSeconds(countTime);
        counterText.transform.localScale = Vector3.one;
        counterText.transform.DOScale(Vector3.one * 1.5f, countTime);
        counterText.text = "1";
        yield return new WaitForSeconds(countTime);
        counterText.transform.localScale = Vector3.one;
        counterText.transform.DOScale(Vector3.one * 1.5f, countTime);
        counterText.text = "Ready";
        yield return new WaitForSeconds(countTime);
        counterText.transform.localScale = Vector3.one;
        counterText.transform.DOScale(Vector3.one * 1.5f, countTime);
        counterText.text = "Fight!!";
        yield return new WaitForSeconds(countTime + 0.5f);
        StartCoroutine(Timer());
        counterText.text = "";
        gameStart = true;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        time--;
        timerText.text = time.ToString();
        if(time > 0 && !gameEnd)
            StartCoroutine(Timer());
        else if(!gameEnd)
        {
            GameEnd();            
        }
    }

    public void GameEnd()
    {
        gameEnd = true;
        if (playerOne.GetComponent<Player>().health > playerTwo.GetComponent<Player>().health)
        {
            print("Player One Won!");
            winnerText.text = "Player One Won!";
            winnerText.gameObject.SetActive(true);
            playerOne.GetComponent<Player>().PlayAnimation("Idle");
            playerTwo.GetComponent<Player>().PlayAnimation("Death");
            playerTwo.GetComponent<Player>().dieSound.Play();
        }
        else if (playerOne.GetComponent<Player>().health < playerTwo.GetComponent<Player>().health)
        {
            print("Player Two Won!");
            winnerText.text = "Player Two Won!";
            winnerText.gameObject.SetActive(true);
            playerOne.GetComponent<Player>().PlayAnimation("Death");
            playerTwo.GetComponent<Player>().PlayAnimation("Idle");
            playerOne.GetComponent<Player>().dieSound.Play();
        }
        else
        {
            print("Deuce");
            winnerText.text = "Deuce!";
            winnerText.gameObject.SetActive(true);
            playerOne.GetComponent<Player>().PlayAnimation("Idle");
            playerTwo.GetComponent<Player>().PlayAnimation("Idle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerOne.transform.position.x > playerTwo.transform.position.x)
        {
            if (!playerOne.GetComponent<Player>().death && !playerTwo.GetComponent<Player>().death)
            {
                playerOne.GetComponent<Player>().standingSide = Player.StandingSide.Right;
                playerOne.transform.localScale = new Vector3(-2, 2, 2);
                playerTwo.GetComponent<Player>().standingSide = Player.StandingSide.Left;
                playerTwo.transform.localScale = new Vector3(2, 2, 2);
            }            
        }
        else
        {
            if (!playerOne.GetComponent<Player>().death && !playerTwo.GetComponent<Player>().death)
            {
                playerOne.GetComponent<Player>().standingSide = Player.StandingSide.Left;
                playerTwo.GetComponent<Player>().standingSide = Player.StandingSide.Right;
                playerOne.transform.localScale = new Vector3(2, 2, 2);
                playerTwo.transform.localScale = new Vector3(-2, 2, 2);
            }
        }
    }
}
