using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : MonoBehaviour
{
    public GameObject pl, bullet;
    public bool inHand;

    public int ammo, musketID;
    
    public Transform shootPos;

    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        ammo = (1 + pl.GetComponent<OffhandFunctions>().choiceLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if((pl.GetComponent<OffhandFunctions>().shoot == true) && inHand)
        {
            pl.GetComponent<OffhandFunctions>().shoot = false;
            Shoot();
        }

        
    }

    void Shoot()
    {
        if(ammo > 0)
        {
            if(musketID == 0)
        {
        GameObject proj = Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);
        proj.GetComponent<Projectile>().rangedDmg = (50 * (pl.GetComponent<OffhandFunctions>().choiceLevel + 1));
        proj.GetComponent<Projectile>().moveSpeed = -(10 * (pl.GetComponent<OffhandFunctions>().choiceLevel + 1));
        proj.GetComponent<Projectile>().lifespan = 5f;
        }
        if(musketID == 1)
        {
        GameObject proj = Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);
        proj.GetComponent<Projectile>().magicDmg = (30 * (pl.GetComponent<OffhandFunctions>().choiceLevel + 1));
        proj.GetComponent<Projectile>().isHoming = true;
        proj.GetComponent<Projectile>().moveSpeed = (2 * (pl.GetComponent<OffhandFunctions>().choiceLevel + 1));
        proj.GetComponent<Projectile>().lifespan = 5f;
        }

        ammo -= 1;

        if(ammo == 0)
        {
            pl.GetComponent<OffhandFunctions>().hasMusket = false;
            Destroy(this.gameObject);
        }
        }
        
    }
}
