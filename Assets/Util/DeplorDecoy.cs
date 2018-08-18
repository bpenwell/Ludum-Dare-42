using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeplorDecoy : MonoBehaviour {


    [SerializeField] public bool isActive = false;
    [SerializeField] public float cooldown;
    [SerializeField] public float duration;
    public Text CoolDownTimeDecoy_UI;
    bool onCooldown = false;

    // Use this for initialization
    void Start () {
        cooldown = 0;
        duration = 0;
        CoolDownTimeDecoy_UI.text = cooldown.ToString("F1");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
	}

    // Update is called once per frame
    void Update() {
        if (onCooldown)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < 0)
                {
                    cooldown = 0;
                }
            }
            CoolDownTimeDecoy_UI.text = cooldown.ToString("F1");
        }
        if (isActive)
        {

            
            CoolDownTimeDecoy_UI.text = cooldown.ToString("F1");
            duration -= Time.deltaTime;
           
            if (duration <= 0)
            {
                isActive = false;
                //gameObject.GetComponent<CircleCollider2D>().enabled = false;
                //gameObject.SetActive(false);
                // decoySprite.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
	}

    public void activateItem(Vector3 newPos)
    {
        onCooldown = true;
        isActive = true;
        duration = 10;
        cooldown = 60;
        CoolDownTimeDecoy_UI.text = cooldown.ToString("F1");
        Vector3 temp = new Vector3(0, 0, 10);
        newPos += temp;
        gameObject.transform.position = newPos;
        //gameObject.GetComponent<CircleCollider2D>().enabled = true;
        //gameObject.SetActive(true);
        //decoySprite.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    public bool checkAvailable()
    {
        if (cooldown > 0)
        {
            return false;
        }
        return true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.GetComponent<zombieAI>())
            {
                col.GetComponent<zombieAI>().changeTarget(transform);
            }
            else if (col.GetComponent<AngryZombieAI>())
            {
                col.GetComponent<AngryZombieAI>().changeTarget(transform);
            }
            
        }

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.GetComponent<zombieAI>())
            {
                col.GetComponent<zombieAI>().revertTarget();
            }
            else if (col.GetComponent<AngryZombieAI>())
            {
                col.GetComponent<AngryZombieAI>().revertTarget();
            }

        }
    }
}
