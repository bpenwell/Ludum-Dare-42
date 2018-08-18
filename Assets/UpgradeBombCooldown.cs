﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBombCooldown : MonoBehaviour {

    public MoneyBag goldReference;
    [SerializeField] public bombMaker bomb;
    public bool bombUpgraded;
    public int price;
    // Use this for initialization
    void Start()
    {
        bombUpgraded = false;
        price = 150;
        goldReference = GameObject.FindWithTag("Player").GetComponent<MoneyBag>();
        gameObject.GetComponentInChildren<TextMesh>().text = price.ToString();
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player found");
            if (goldReference.currentGold >= price && !bombUpgraded)
            {
                goldReference.EditGold(-price);
                bomb.upgradeBombCooldown();
                bombUpgraded = true;
                gameObject.GetComponentInChildren<TextMesh>().text = "Sold Out";
                //gameObject.GetComponentInChildren<TextMesh>().text = (pricePerLevel * shopLevel).ToString();
            }
        }

    }
}
