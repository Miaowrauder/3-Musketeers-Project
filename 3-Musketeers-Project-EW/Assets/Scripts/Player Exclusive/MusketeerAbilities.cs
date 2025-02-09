using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerAbilities : MonoBehaviour
{
    public int musketeerLevel, musketeerID;
    public GameObject cloudPrefab, cloudSpot, araPrefab, araSpot;
    public GameObject[] enemies;
    GameObject ui;
    public int por, ath, ara;
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UImanager");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && (ui.GetComponent<UImanager>().musketeerCharge >= 100) && (musketeerID > 0))
        {
            ui.GetComponent<UImanager>().musketeerCharge = 0;
            ui.GetComponent<UImanager>().canSet = true;
            ui.GetComponent<UImanager>().musketext.text = " -X- ";

            if(musketeerID == 1)
            {
                delay = (10f +(musketeerLevel*2));
                StartCoroutine(Delay());
            }
            else if(musketeerID == 2)//ath
            {
                GameObject cloud = Instantiate(cloudPrefab, cloudSpot.transform.position, Quaternion.identity);
                cloud.GetComponent<CloudController>().followTarget = cloudSpot;
                cloud.GetComponent<CloudController>().duration = (10f + (musketeerLevel * 2));
                cloud.GetComponent<CloudController>().regenPerSec = (10f + (musketeerLevel * 5));

            }
            else if(musketeerID == 3)
            {
                delay = (5f);
                StartCoroutine(Delay());
            }
        }
    }

    private IEnumerator Delay()
    {
        if(musketeerID == 1)//po
        {
            this.GetComponent<MeleeAttack>().porthosAb = true; 
            this.GetComponent<MeleeAttack>().hitboxScale *= 2f;

            for(int i = 0; i < (delay/2); i++)
            {
                yield return new WaitForSeconds(delay/(delay/2)); //2s delay regardless of duration
                this.GetComponent<plHealth>().health += (10f + (musketeerLevel * 5));
            }

            //yield return new WaitForSeconds(delay);   

            this.GetComponent<MeleeAttack>().hitboxScale /= 2f;
            this.GetComponent<MeleeAttack>().porthosAb = false;
        }

        if(musketeerID == 3)//ara
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = null;
            }
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyMove>().canSeePlayer = true;
                enemies[i].GetComponent<EnemyMove>().mainhandEnabled = false;
                enemies[i].GetComponent<EnemyMove>().offhandEnabled = false;
            }
            yield return new WaitForSeconds(delay);
            this.GetComponent<plHealth>().health = this.GetComponent<plHealth>().maxHealth;

            GameObject strike = Instantiate(araPrefab, araSpot.transform.position, Quaternion.identity);
            strike.GetComponent<DamageCollider>().magicDmg = (150 + (musketeerLevel * 150));

            yield return new WaitForSeconds(delay/5); //stops enemies attacking instantly after strike

            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyMove>().mainhandEnabled = true;
                enemies[i].GetComponent<EnemyMove>().offhandEnabled = true;
            }
        }
        
    }
}
