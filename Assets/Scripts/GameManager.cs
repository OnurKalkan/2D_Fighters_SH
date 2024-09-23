using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne, playerTwo;

    // Update is called once per frame
    void Update()
    {
        if(playerOne.transform.position.x > playerTwo.transform.position.x)
        {
            playerOne.GetComponent<Player>().standingSide = Player.StandingSide.Right;
            playerTwo.GetComponent<Player>().standingSide = Player.StandingSide.Left;
            playerOne.transform.localScale = new Vector3(-2, 2, 2);
            playerTwo.transform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            playerOne.GetComponent<Player>().standingSide = Player.StandingSide.Left;
            playerTwo.GetComponent<Player>().standingSide = Player.StandingSide.Right;
            playerOne.transform.localScale = new Vector3(2, 2, 2);
            playerTwo.transform.localScale = new Vector3(-2, 2, 2);
        }
    }
}
