using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectColliders : MonoBehaviour {

    //public CircleCollider2D setToTrigger;
    private waveManager WaveManager;
    public zombieAI zombiePointer;
    public GameObject explosion;
    public Transform playerReference;
    public int bombDamage;
    //public Transform bombPreFab;



    void Start()
	{
        bombDamage = 85;
        transform.position = new Vector2(-5000, -5000);
        WaveManager = GameObject.Find("ZombieSpawner").GetComponent<waveManager>();
        //Invoke ("detonate", 2.0f);
        playerReference = GameObject.Find("Player").GetComponent<Transform>();

    }

	public void detonate(){
        Debug.Log("detonate called");
        //transform.position = playerReference.position;
        Vector2 explosionPosition = transform.position;
		float explosionRadius = 2;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
		Debug.Log(colliders.Length);
        
		for(int i=0;i< colliders.Length;i++)
        { 
            if (colliders[i].tag == "Enemy")
            {
               zombiePointer =  colliders[i].GetComponent<zombieAI>(); 
               zombiePointer.changeHealth(bombDamage);
            }
            if (colliders[i].tag == "Corpse")
		    {
		        Destroy(colliders[i].gameObject);
		    }
           
	    }
        Instantiate(explosion, transform.position, Quaternion.identity);
        transform.position = new Vector2(-5000, -5000);

    }
    public void upgradeBombDamage()
    {
        bombDamage = 125;
    }

	/*void OnTriggerEnter2D(Collider2D col){
		Debug.Log("YOOOO");
		Destroy(this);
	}*/
}
