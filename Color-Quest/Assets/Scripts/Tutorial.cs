using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Inventory;

    public void Start()
    {
        Inventory.gameObject.SetActive(true);
    }

    public void Switch()
    {
        if (Inventory.activeSelf == false) Inventory.SetActive(true);
        else Inventory.SetActive(false);
    }
}