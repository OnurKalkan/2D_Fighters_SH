using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne, playerTwo;
    public TextMeshProUGUI timerText;
    public int time = 90;

    private void Start()
    {
        timerText.text = time.ToString();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        time--;
        timerText.text = time.ToString();
        if(time > 0)
            StartCoroutine(Timer());
        else
        {
            if (playerOne.GetComponent<Player>().health > playerTwo.GetComponent<Player>().health)
                print("Player One Won!");
            else if (playerOne.GetComponent<Player>().health < playerTwo.GetComponent<Player>().health)
                print("Player Two Won!");
            else
                print("Deuce!");
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
