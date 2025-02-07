using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalistAbilities : MonoBehaviour
{
    bool canAttack;
    float delay;

    public GameObject weaveProj, weaveGrenade, tpCollider, doomCone, spinnyBeams, evilLerpProj;
    public GameObject pl, self, shootPos;
    public GameObject[] tpSpots, lerpSpawns;

    GameObject beams;
    public float[] distance;

    int i, chosen, trigger;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        canAttack = true;
        delay = 1; //acts as a second, but value is lower when enemy health is lowered to reduce time between attacks.
        trigger = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackTimings());
        }

        if((this.GetComponent<Health>().hp >= (this.GetComponent<Health>().maxHp * 0.75)) && (trigger == 3))
        {
            trigger -= 1;
            delay = 0.8f;
        }
        else if((this.GetComponent<Health>().hp >= (this.GetComponent<Health>().maxHp * 0.5)) && (trigger == 2))
        {
            trigger -= 1;
            delay = 0.6f;
        }
        else if((this.GetComponent<Health>().hp >= (this.GetComponent<Health>().maxHp * 0.25)) && (trigger == 2))
        {
            trigger -= 1;
            delay = 0.4f;
        }

    }

    private IEnumerator AttackTimings()
    {
        if(this.GetComponent<EnemyMove>().mainhandEnabled || this.GetComponent<EnemyMove>().offhandEnabled) //either arm remaining
        {
            WeaveAttack();
            yield return new WaitForSeconds(delay * 2f);
        }
        yield return new WaitForSeconds(delay * 2f);// having delay only on attack use means limb chopping can be detrimental, having it always means limb chopping is too strong - so I split it

        if(this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed > 1f) //has at least one leg, im aware weave can still tp - thats fine, its to keep people off-guard ;)
        {
            Tp();
            yield return new WaitForSeconds(delay * 2.5f);
        }
        yield return new WaitForSeconds(delay * 2.5f);
        
        if(this.GetComponent<EnemyMove>().mainhandEnabled && this.GetComponent<EnemyMove>().offhandEnabled) //both arms remaining
        {
            BigAttack();
            yield return new WaitForSeconds(delay * 5f);
            Destroy(beams);

        }
        yield return new WaitForSeconds(delay * 4f);
        
        if(this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed > 1f) 
        {
            Tp();
            yield return new WaitForSeconds(delay * 2.5f);
        }
        yield return new WaitForSeconds(delay * 2.5f);

        canAttack = true;
    }

    public void WeaveAttack()
    {
        i = Random.Range(0,3);

        if(i == 0)
        {
            WeaveProj();
        }
        else if(i == 1)
        {
            WeaveGrenade();
        }
        else if(i == 2)
        {
            Tp();
        }
    }

    public void WeaveProj()
    {
        GameObject proj = Instantiate(weaveProj, shootPos.transform.position, shootPos.transform.rotation);
    }

    public void WeaveGrenade()
    {
        GameObject gren = Instantiate(weaveGrenade, shootPos.transform.position, shootPos.transform.rotation);

        gren.gameObject.GetComponent<Rigidbody>().AddForce(gren.transform.forward * 5, ForceMode.Impulse);    
        gren.gameObject.GetComponent<Rigidbody>().AddForce(gren.transform.up * (3), ForceMode.Impulse);
        gren.gameObject.GetComponent<Grenade>().isPrimed = true;
    }

    public void Tp()
    {
        for(int o = 0; o < tpSpots.Length; o++)
        {
            distance[o] = Vector3.Distance(tpSpots[o].transform.position, pl.transform.position);
        }

        int pos = 0;
        for (int i = 0; i < distance.Length; i++)
        {
            if (distance[i] < distance[pos]) 
            {
                pos = i;
            }
            chosen = i;
        }

        GameObject tpDmg = Instantiate(tpCollider, self.transform.position, Quaternion.identity);

        self.transform.position = tpSpots[chosen].transform.position;

        GameObject tpDmg2 = Instantiate(tpCollider, self.transform.position, Quaternion.identity);
    }

    public void BigAttack()
    {
        i = Random.Range(0,3);

        if(i == 0)
        {
            SpinnyBeams();
        }
        else if(i == 1)
        {
            ConeOfDoom();
        }
        else if(i == 2)
        {
            EvilLerpers();
        }
    }

    public void SpinnyBeams()
    {
        beams = Instantiate(spinnyBeams, this.transform.position, Quaternion.identity);
        beams.transform.parent = this.transform;

    }
    public void ConeOfDoom()
    {
        GameObject cone = Instantiate(doomCone, shootPos.transform.position, shootPos.transform.rotation);
        cone.transform.parent = shootPos.transform;
    }
    public void EvilLerpers()
    {
        for(int l = 0; l < lerpSpawns.Length; l++)
        {
            GameObject lerper = Instantiate(evilLerpProj, lerpSpawns[l].transform.position, Quaternion.identity);
        }
    }
}
