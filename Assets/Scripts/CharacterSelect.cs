using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject myChar, charButton, inGamePlayerParent;
    public CharacterType myCharacterType;
    public int charIndex = 1;
    public bool isAI, playerOne, playerTwo;
    GameManager gameManager;

    public enum CharacterType
    {
        HeroKnight,
        YellowKnight,
        BlueKnight,
        DarkKnight
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        InitialCharacter();
    }

    public void CharSelect()
    {
        string charName = "";
        if (charIndex == 4)
            charIndex = 1;
        else
            charIndex++;
        switch (charIndex)
        {
            case 1: myCharacterType = CharacterType.HeroKnight; break;
            case 2: myCharacterType = CharacterType.YellowKnight; break;
            case 3: myCharacterType = CharacterType.BlueKnight; break;
            case 4: myCharacterType = CharacterType.DarkKnight; break;
        }
        if (myCharacterType == CharacterType.HeroKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "HERO KNIGHT";
            myChar.GetComponent<Image>().color = Color.white;
            charName = "HeroKnight";
            gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight").gameObject;
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight").gameObject;
        }
        else if (myCharacterType == CharacterType.YellowKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YELLOW KNIGHT";
            myChar.GetComponent<Image>().color = Color.yellow;
            charName = "HeroKnight Yellow";
            if(playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Yellow").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Yellow").gameObject;
        }
        else if (myCharacterType == CharacterType.BlueKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BLUE KNIGHT";
            myChar.GetComponent<Image>().color = Color.blue;
            charName = "HeroKnight Blue";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Blue").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Blue").gameObject;
        }
        else if (myCharacterType == CharacterType.DarkKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DARK KNIGHT";
            myChar.GetComponent<Image>().color = Color.black;
            charName = "HeroKnight Dark";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Dark").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Dark").gameObject;
        }
        SelectInGameChar(charName);
    }

    public void InitialCharacter()
    {
        string charName = "";
        if(myCharacterType == CharacterType.HeroKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "HERO KNIGHT";
            myChar.GetComponent<Image>().color = Color.white;
            charIndex = 1;
            charName = "HeroKnight";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight").gameObject;
        }
        else if (myCharacterType == CharacterType.YellowKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YELLOW KNIGHT";
            myChar.GetComponent<Image>().color = Color.yellow;
            charIndex = 2;
            charName = "HeroKnight Yellow";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Yellow").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Yellow").gameObject;
        }
        else if (myCharacterType == CharacterType.BlueKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BLUE KNIGHT";
            myChar.GetComponent<Image>().color = Color.blue;
            charIndex = 3;
            charName = "HeroKnight Blue";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Blue").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Blue").gameObject;
        }
        else if (myCharacterType == CharacterType.DarkKnight)
        {
            charButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DARK KNIGHT";
            myChar.GetComponent<Image>().color = Color.black;
            charIndex = 4;
            charName = "HeroKnight Dark";
            if (playerOne)
                gameManager.playerOne = inGamePlayerParent.transform.Find("HeroKnight Dark").gameObject;
            else
                gameManager.playerTwo = inGamePlayerParent.transform.Find("HeroKnight Dark").gameObject;
        }
        SelectInGameChar(charName);
    }

    void SelectInGameChar(string charName)
    {
        for (int i = 0; i < inGamePlayerParent.transform.childCount; i++)
        {
            inGamePlayerParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        inGamePlayerParent.transform.Find(charName).gameObject.SetActive(true);
        if (isAI && playerTwo)
            inGamePlayerParent.transform.Find(charName).GetComponent<Player>().playerType = Player.PlayerType.AI;
        else if(!isAI && playerTwo)
            inGamePlayerParent.transform.Find(charName).GetComponent<Player>().playerType = Player.PlayerType.PlayerTwo;
    }
}
