using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bombMaker : MonoBehaviour {

	[SerializeField] GameObject Object;
    [SerializeField] public collectColliders bomb;
    //[SerializeField] GameObject player;
    // Use this for initialization
    public int CoolDownTime;
	public Text CoolDownTime_UI;

	float time=0;
	bool onCooldown=false;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1") && !onCooldown){
			//CoolDownTime_UI.text = CoolDownTime.ToString();
			time = CoolDownTime;
            //Instantiate(Object, transform);
            //transform.DetachChildren();
            Invoke("detonateCall", 2);
            bomb.transform.position = transform.position;
			onCooldown = true;
		}
		if(onCooldown){
			time -= Time.deltaTime;
			CoolDownTime_UI.text = time.ToString("F1");
		}
		if(time <= 0){
			time = 0;
			onCooldown = false;
			CoolDownTime_UI.text = time.ToString("F1");
		}

	}
    void detonateCall()
    {
        bomb.detonate();
    }
    public void upgradeBombCooldown()
    {
        CoolDownTime = 10;
    }
}
