using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierItem : MonoBehaviour {

    [SerializeField] public bool isActive = false;
    [SerializeField] public GameObject host;
    [SerializeField] public float duration = 0;
    [SerializeField] public float cooldown = 0;
    public Text CoolDownTimeShield_UI;
    private ParticleSystem ps;
    bool onCooldown = false;
    // Use this for initialization
    void Start () {
        cooldown = 0;
        duration = 0;
        CoolDownTimeShield_UI.text = cooldown.ToString("F1");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
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
            CoolDownTimeShield_UI.text = cooldown.ToString("F1");
        }
        if (isActive)
        {

            duration -= Time.deltaTime;
            cooldown -= Time.deltaTime;
            CoolDownTimeShield_UI.text = cooldown.ToString("F1");

            if (duration <= 0)
            {
               isActive = false;
               host.GetComponent<ParticleSystem>().Stop();
            }
        }
	}

    public void activateItem()
    {
        onCooldown = true;
        isActive = true;
        duration = 8;
        cooldown = 45;
        CoolDownTimeShield_UI.text = cooldown.ToString("F1");
        host.GetComponent<healthSystem>().invFrames = -8;
        host.GetComponent<ParticleSystem>().Play();
    }

    public bool checkAvailable()
    {
        if (cooldown > 0)
        {
            return false;
        }
        return true;
    }
}
