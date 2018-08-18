using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    // Use this for initialization
    [SerializeField] public GameObject gunSpawn;
    [SerializeField] public GameObject rifleSpawn;
    [SerializeField] public LineRenderer lineEdit;
    public Vector2 lastZombieHit;
    public int[] currentClip;
    public int[] bulletPenetration;
    public int[] maxClip;
    [SerializeField] public int[] currentUpgrade;
    public float[] fireRate;
    public float[] cooldown;
    public float[] accuracy;
    public bool isReloading;
    public float[] reloadTime;
    public int[] damageValue;
    public int[] damageUpgrade;
    public bool[] clipUpgraded;
    public bool[] accuracyUpgraded;
    Vector2 extension;
    Vector2 endPoint;
    Vector2 offset;
    public int currentWeapon;
    public int passthroughDamage;
    public Color traceColor;

    public AudioClip pistol_shot;
    public AudioClip rifle_shot;
    public AudioClip submachinegun_shot;

    //private AudioSource sourceMusic;
    public AudioSource sourceGunNoise;



    void Start()
    {
        traceColor = Color.white;
        //Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        lineEdit.startColor = Color.grey;
        lineEdit.endColor = Color.red;
        //bulletSpawn = GetChild(0).transform;
        currentUpgrade = new int[3];
        //lineEdit.SetColors(Color.blue, traceColor);
        currentClip = new int[3];
        damageUpgrade = new int[3];
        clipUpgraded = new bool[3];
        accuracyUpgraded = new bool[3];
        bulletPenetration = new int[3];
        bulletPenetration[0] = 2;
        bulletPenetration[1] = 3;
        bulletPenetration[2] = 2;
        damageUpgrade[0] = 15;
        damageUpgrade[1] = 20;
        damageUpgrade[2] = 7;
        maxClip = new int[3];
        fireRate = new float[3];
        cooldown = new float[3];
        accuracy = new float[3];
        reloadTime = new float[3];
        damageValue = new int[3];
        currentWeapon = 0;
        isReloading = false;
        maxClip[0] = 7;
        maxClip[1] = 8;
        maxClip[2] = 30;
        damageValue[0] = 40;
        damageValue[1] = 80;
        damageValue[2] = 25;
        fireRate[0] = 0.2f;
        fireRate[1] = 0.45f;
        fireRate[2] = 0.05f;
        reloadTime[0] = 1f;
        reloadTime[1] = 3.15f;
        reloadTime[2] = 2.25f;
        accuracy[0] = .7f;
        accuracy[1] = .8f;
        accuracy[2] = .5f;
        for (int i = 0; i < 3; i++)
        {
            currentClip[i] = maxClip[i];
            cooldown[i] = 0f;
            currentUpgrade[i] = 0;
            clipUpgraded[i] = false;
            accuracyUpgraded[i] = false;
        }
        extension = new Vector2(100, 100);
        endPoint = new Vector2(100, 100);
        //sourceMusic.GetComponent<AudioSource>();
        sourceGunNoise.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown[currentWeapon] -= Time.deltaTime;

        if (Input.GetKeyDown("q") && !(isReloading))
        {
            if (currentWeapon == 0)
                currentWeapon = 2;
            else
                currentWeapon--;
        }
        if (Input.GetKeyDown("q") && isReloading)
        {

        }
        if (Input.GetKeyDown("e") && !(isReloading))
        {
            if (currentWeapon == 2)
                currentWeapon = 0;
            else
                currentWeapon++;
        }
        if (Input.GetKeyDown("e") && isReloading)
        {

        }

        if (Input.GetKey("r") && !(isReloading) && currentClip[currentWeapon] != maxClip[currentWeapon])
        {

            Invoke("reload", reloadTime[currentWeapon]);
            isReloading = true;
            //upgradeAccuracy(currentWeapon);
            Debug.Log("RELOADING");

        }
        if (Input.GetMouseButton(0) && (currentClip[currentWeapon] > 0) && !(isReloading) && cooldown[currentWeapon] <= 0)
        {
            cooldown[currentWeapon] = fireRate[currentWeapon];
            currentClip[currentWeapon]--;
            extension = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            extension.Normalize();
            //Debug.Log(extension);
            // Debug.Log(endPoint);
            endPoint = extension * 1000;
            offset = new Vector2(Random.Range(-400.0f * (1 - accuracy[currentWeapon]), 400.0f * (1 - accuracy[currentWeapon])), Random.Range(-400.0f * (1 - accuracy[currentWeapon]), 400.0f * (1 - accuracy[currentWeapon])));
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(gunSpawn.transform.position, endPoint + offset);//, distance, whatIsSolid);

            /* if (hitInfo[i].collider != null)
             {
                 if (hitInfo[i].collider.CompareTag("Enemy"))
                 {
                     //hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                 }
                 //DestroyProjectile();
             }
             */
            //Ray bulletPath = new Ray(bulletSpawn.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.DrawLine(gunSpawn.transform.position, endPoint + offset, Color.blue, 0.2f);
            //RaycastHit2D hit = Physics2D.Raycast(bulletPath.origin, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.DrawRay(gunSpawn.transform.position, endPoint + offset, Color.green, 0.2f);
            lastZombieHit = endPoint + offset;
            // Debug.Log("Bullet Fired from " + currentWeapon);
            print("Bullet Fired from " + currentWeapon);
            if(currentWeapon == 0){
                sourceGunNoise.PlayOneShot(pistol_shot,1);
            }
            if(currentWeapon == 1){
                sourceGunNoise.PlayOneShot(rifle_shot,1);
            }
            if(currentWeapon == 2){
                sourceGunNoise.PlayOneShot(submachinegun_shot,1);
            }

            if (currentWeapon == 1)
            {
                rifleSpawn.GetComponent<SpriteRenderer>().enabled = true;
                Invoke("DisableMuzzle", 0.1f);
            }
            else
            {
                gunSpawn.GetComponent<SpriteRenderer>().enabled = true;
                Invoke("DisableMuzzle", 0.1f);
            }

            int size = 0;

            if (hitInfo.Length > bulletPenetration[currentWeapon])
            {
                size = bulletPenetration[currentWeapon];
            }
            else
            {
                size = hitInfo.Length;
            }

            for (int i = 0; i < size; i++)
            {
                //calculated damage to the zombie based off how many things bullet has hit
                
                if (hitInfo[i].collider != null)
                {
                    passthroughDamage = damageValue[currentWeapon] + (damageUpgrade[currentWeapon] * currentUpgrade[currentWeapon]) - (damageUpgrade[currentWeapon] * i);
                    //Debug.Log(hitInfo[i].collider.tag);
                    if(passthroughDamage < damageUpgrade[currentWeapon])
                    {
                        passthroughDamage = damageUpgrade[currentWeapon];
                    }

                    if (hitInfo[i].collider.CompareTag("Enemy"))
                    {

                        // Debug.Log("Enemy Hit!");
                        lastZombieHit = hitInfo[i].transform.position;

                        if (hitInfo[i].collider.GetComponent<AngryZombieAI>())
                        {
                            hitInfo[i].collider.GetComponent<AngryZombieAI>().changeHealth(passthroughDamage);
                        }
                        else if(hitInfo[i].collider.GetComponent<WraithAI>())
                        {
                            hitInfo[i].collider.GetComponent<WraithAI>().changeHealth(passthroughDamage);
                        }
                        else
                        {
                            hitInfo[i].collider.GetComponent<zombieAI>().changeHealth(passthroughDamage);
                        }

                        //hit.collider.GetComponent<zombieAI>().TakeDamage(damage);
                    }
                }
            }

            lineEdit.SetPosition(0, gunSpawn.transform.position);
            lineEdit.SetPosition(1, lastZombieHit);
            Invoke("eraseLine", 0.1f);





        }
        else if (Input.GetMouseButtonDown(0) && isReloading)
        {
            Debug.Log("Reloading");
        }
        else if (Input.GetMouseButtonDown(0) && currentClip[currentWeapon] <= 0)
        {
            Debug.Log("RELOADING");
            Invoke("reload", reloadTime[currentWeapon]);
            isReloading = true;
        }


    }
    void eraseLine()
    {
        lineEdit.SetPosition(0, new Vector2(-5000, -5000));
        lineEdit.SetPosition(1, new Vector2(-5000, -5000));
        //Destroy(this.renderer.material);
    }

    void reload()
    {


        currentClip[currentWeapon] = maxClip[currentWeapon];
        isReloading = false;
        Debug.Log("Finished Reload");

    }
    void DisableMuzzle()
    {
        if (currentWeapon == 1)
        {
            rifleSpawn.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gunSpawn.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public int getCurrentWeapon
    {
        get { return currentWeapon; }
    }
    public void upgradeWeaponDamage(int weapondToUpgrade)
    {
        currentUpgrade[weapondToUpgrade]++;
    }
    public void upgradeClip(int weaponToUpgrade)
    {
        clipUpgraded[weaponToUpgrade] = true;
        if (weaponToUpgrade == 0)
            maxClip[0] = 15;
        if (weaponToUpgrade == 1)
            maxClip[1] = 12;
        if (weaponToUpgrade == 2)
            maxClip[2] = 45;
    }
    public void upgradeAccuracy(int weaponToUpgrade)
    {
        accuracyUpgraded[weaponToUpgrade] = true;
        if (weaponToUpgrade == 0)
            accuracy[0] = .9f;
        if (weaponToUpgrade == 1)
            accuracy[1] = .95f;
        if (weaponToUpgrade == 2)
            accuracy[2] = .7f;
    }
    public void upgradeBulletPenetration(int weaponToUpgrade)
    {
        bulletPenetration[weaponToUpgrade]++;

    }
}
