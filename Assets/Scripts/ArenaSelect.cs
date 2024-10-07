using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaSelect : MonoBehaviour
{
    public bool isSelected;
    public GameObject arena;

    public void SelectThisArena()
    {
        if (!isSelected)
        {
            CloseOthers();
            isSelected = true;
            transform.parent.GetComponent<Image>().enabled = true;
            arena.SetActive(true);
            GameObject.Find("PlayExit").transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
        else
        {
            isSelected = false;
            transform.parent.GetComponent<Image>().enabled = false;
            arena.SetActive(false);
            GameObject.Find("PlayExit").transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }

    void CloseOthers()
    {
        GameObject.Find("PlayExit").transform.GetChild(0).GetComponent<Button>().interactable = false;
        GameObject arenaParent = transform.parent.parent.gameObject;
        for (int i = 0; i < arenaParent.transform.childCount; i++)
        {
            arenaParent.transform.GetChild(i).GetComponent<Image>().enabled = false;
            arenaParent.transform.GetChild(i).GetChild(0).GetComponent<ArenaSelect>().isSelected = false;
            arenaParent.transform.GetChild(i).GetChild(0).GetComponent<ArenaSelect>().arena.SetActive(false);
        }
    }
}
