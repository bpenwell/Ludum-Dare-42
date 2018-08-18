﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngryZombieAI : MonoBehaviour {

    [SerializeField] public Transform target; //Reference to the player mostly; change with decoy?
    [SerializeField] public float speed = 5; //Speed of the zombo
    [SerializeField] public float lifespan; //Lifespan if we want to use it; maybe for explosive enemies? (Not used)
    [SerializeField] public GameObject corpse; //The type of corpse we drop
    [SerializeField] public GameObject spriteRef;
    [SerializeField] public float health = 100; //Health
    [SerializeField] public float throwCooldown = 0; //Cooldown for throwing corpses
    [SerializeField] public GameObject thrownCorpse;
    [SerializeField] public GameObject hitEffect;
    [SerializeField] public int UpperGoldRange = 15;
    [SerializeField] public int LowerGoldRange = 5;
    private waveManager WaveManager;

    Text score;
    int goldTemp;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(100,0,0);
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(health / 100f, health / 100f, 1, 1);
        target = GameObject.FindWithTag("Player").transform;
        score = GameObject.FindWithTag("Score").GetComponent<Text>();
        WaveManager = GameObject.Find("ZombieSpawner").GetComponent<waveManager>();

        //health = 100;
        //speed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        throwCooldown -= Time.deltaTime;

        if (health <= 0)
        {
            WaveManager.KillAngry();
            int.TryParse(score.text, out goldTemp);
            int addedGold = Random.Range(LowerGoldRange, UpperGoldRange);
            Debug.Log("New gold total: " + goldTemp);
            score.text = goldTemp.ToString();

            Instantiate(corpse, transform.position, spriteRef.transform.rotation);
            Destroy(gameObject);
        }
        if (this.transform.position != target.position)
        {
            float step = speed * Time.deltaTime;

            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, step);
        }

    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if ((col.gameObject.tag == "Corpse") && throwCooldown <= 0)
        {
            throwCooldown = 2;
            col.gameObject.GetComponent<corpseBehavior>().setThrown(transform.position, spriteRef.transform.rotation); //pass velocity and rotation
        }
    }

    //Call to decrease the health of the zombie from any source
    //Use negative damage to heal, and positive damage to hurt
    public void changeHealth(float damage)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        health -= damage;
    }

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }
    //resets to player target
    public void revertTarget()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
}
