using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P2 : MonoBehaviour
{
    public PlayerTwoType playerTwoType;
    public GameObject playerTwoSelect;

    public enum PlayerTwoType
    {
        PC,
        AI
    }

    private void Start()
    {
        playerTwoType = PlayerTwoType.AI;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "AI";
        playerTwoSelect.GetComponent<CharacterSelect>().isAI = true;
    }

    public void ChangePlayerTwoType()
    {
        if(playerTwoType == PlayerTwoType.PC)
        {
            playerTwoType = PlayerTwoType.AI;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "AI";
            playerTwoSelect.GetComponent<CharacterSelect>().isAI = true;
        }
        else
        {
            playerTwoType = PlayerTwoType.PC;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PLAYER TWO";
            playerTwoSelect.GetComponent<CharacterSelect>().isAI = false;
        }
        playerTwoSelect.GetComponent<CharacterSelect>().InitialCharacter();
    }
}
