using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public GameObject self, ui;
    public float[] DotDmg, DotDuration;
    public bool isTicking, isEnemy;
    public bool[] getStatus;
    public int buffDebuffID;
    public GameObject[] buffDebuffIcons;
    public Transform[] buffDebuffSlots;

    bool one, two;

    public bool[] canSet;
    // Start is called before the first frame update
    void Start()
    {
        one = true;
        ui = GameObject.FindWithTag("UImanager");
        isTicking = false;

        for(int i = 0; i < DotDmg.Length; i++)
        {
            DotDmg[i] = 0f;
        }
        for(int i = 0; i < DotDuration.Length; i++)
        {
            DotDuration[i] = 0f;
        }

        DotDmg[4] = 1f;
        DotDmg[5] = 1f;
 
        
    }

    // Update is called once per frame
    void Update()
    {
        if( (((DotDuration[0]) > 0f) || ((DotDuration[1]) > 0f) || ((DotDmg[2]) > 0f) || ((DotDuration[3]) > 0f) || ((DotDuration[4]) > 0f) || ((DotDuration[5]) > 0f) || ((DotDuration[6]) > 0f)) && (isTicking == false))
        {
            StartCoroutine(tickDown());
        }

        if(DotDuration[0] <= 0f)
        {
            if(!isEnemy)
            {
            canSet[0] = true;
            }
            DotDmg[0] = 0f;
        }

        if(DotDuration[1] <= 0f)
        {
            if(!isEnemy)
            {
            canSet[1] = true;
            }
            DotDmg[1] = 0f;
        }

        if(DotDuration[3] <= 0f)
        {
            if(!isEnemy)
            {
            canSet[3] = true;
            }
            DotDmg[3] = 0f;
        }

        if(DotDuration[4] <= 0f)
        {
            if(!isEnemy)
            {
            canSet[4] = true;
            }
            DotDmg[4] = 1f;
        }

        if(DotDuration[5] <= 0f)
        {
            if(!isEnemy)
            {
            canSet[5] = true;
            }
            DotDmg[5] = 1f;
        }

        if(DotDuration[6] <= 0f) //fear, enemy exclusive
        {
            if(isEnemy && two)
            {
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 10f);
                two = false;//two does the same thing but to prevent this repeating instead
                one = true; //one makes the speed flip only once per status get, preventing toggle on refresh;
            }
            
        }



        // poison wearoff
        if(!isEnemy)
        {
        if (self.GetComponent<OffhandFunctions>().isConsuming == true)
        {
            DotDuration[1] = 0f;
        }
        }

        if(!isEnemy)
        {
            GetStatus();
        }
        
    }

    private IEnumerator tickDown()
    {
        isTicking = true;

        if(DotDuration[0] >= 0.5f)
        {
            if(canSet[0] && (!isEnemy))
            {
                getStatus[0] = true;
            }
    
            DotDuration[0] -= 0.5f;

            if(isEnemy)
            {
                self.GetComponent<Health>().hp -= DotDmg[0];
            }
            else if (!isEnemy)
            {
                if(ui.GetComponent<UImanager>().hasTrinket[7])
                {
                    self.GetComponent<plHealth>().health += DotDmg[0];
                }
                else if (ui.GetComponent<UImanager>().hasTrinket[7] == false)
                {
                    self.GetComponent<plHealth>().health -= DotDmg[0];
                }

                if(self.GetComponent<MusketeerAbilities>().musketeerID == 2)
                {
                    ui.GetComponent<UImanager>().musketeerCharge += (1f + (self.GetComponent<MusketeerAbilities>().musketeerLevel / 2f));
                }

                if (self.GetComponent<PlayerController>().isSprinting == true)
                {
                    DotDuration[0] -= 0.5f;
                }
            }
        }

        if(DotDuration[1] >= 0.5f)
        {
            if(canSet[1] && (!isEnemy))
            {
                getStatus[1] = true;
            }
            DotDuration[1] -= 0.5f;

            if(isEnemy)
            {
                self.GetComponent<Health>().hp -= DotDmg[1];
            }
            else if (!isEnemy)
            {
                if(ui.GetComponent<UImanager>().hasTrinket[8])
                {
                    self.GetComponent<plHealth>().health += DotDmg[1];
                }
                else if(ui.GetComponent<UImanager>().hasTrinket[8] == false)
                {
                    self.GetComponent<plHealth>().health -= DotDmg[1];
                }

                if(self.GetComponent<MusketeerAbilities>().musketeerID == 2)
                {
                    ui.GetComponent<UImanager>().musketeerCharge += (0.5f + (self.GetComponent<MusketeerAbilities>().musketeerLevel / 2f));
                }

            }
        }

        //bleed, enemy exclusive
        if(DotDmg[2] > 0)
        {    
             self.GetComponent<Health>().hp -= DotDmg[2];         
        }

        //regen
        if(DotDuration[3] >= 0.5f)
        {
            if(canSet[3] && (!isEnemy))
            {
                getStatus[3] = true;
            }
            DotDuration[3] -= 0.5f;

            if(isEnemy)
            {
                self.GetComponent<Health>().hp += DotDmg[3];
            }
            else if (!isEnemy)
            {
                self.GetComponent<plHealth>().health += DotDmg[3];

            }
        }

        //slow
        if(DotDuration[4] >= 0.5f)
        {
            if(canSet[4] && (!isEnemy))
            {
                getStatus[4] = true;
            }

            if(self.GetComponent<MusketeerAbilities>().musketeerID == 2)
            {
                ui.GetComponent<UImanager>().musketeerCharge += (0.5f + (self.GetComponent<MusketeerAbilities>().musketeerLevel / 2f));
            }

            DotDuration[4] -= 0.5f;

            //slow is applied in movement calculations, reasons for why in reflection
        }

        //weak
        if(DotDuration[5] >= 0.5f)
        {
            if(canSet[5] && (!isEnemy))
            {
                getStatus[5] = true;
            }
            DotDuration[5] -= 0.5f;

            if(self.GetComponent<MusketeerAbilities>().musketeerID == 2)
            {
                ui.GetComponent<UImanager>().musketeerCharge += (0.5f + (self.GetComponent<MusketeerAbilities>().musketeerLevel / 2f));
            }

            //weak is applied in attack scripts, same reason as slow
        }
        
        //fear
        if(DotDuration[6] >= 0.5f)
        {
            if(isEnemy)
            {
                if(one)
                {
                    this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed -= (this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 0.9f);
                    one = false;
                    two = true;
                }
            }
            DotDuration[6] -= 0.5f;

        }

        yield return new WaitForSeconds(0.5f);

         isTicking = false;

    }

    public void BuffDebuff()
    {
        for(int b = 0; b < buffDebuffSlots.Length; b++)
       {
            if(buffDebuffSlots[b].transform.childCount < 1)
            {
             GameObject icon = Instantiate(buffDebuffIcons[buffDebuffID], buffDebuffSlots[b].transform.position, Quaternion.identity);
             icon.transform.parent = buffDebuffSlots[b];
             b = buffDebuffSlots.Length;
             canSet[buffDebuffID] = false;
             getStatus[buffDebuffID] = false;           
            }
        }
    }

    public void GetStatus()
    {
        if(getStatus[0] && canSet[0])
        {
            buffDebuffID = 0;
            BuffDebuff();
        }
        if(getStatus[1] && canSet[1])
        {
            buffDebuffID = 1;
            BuffDebuff();
        }
        if(getStatus[3] && canSet[3])
        {
            buffDebuffID = 3;
            BuffDebuff();
        }
        if(getStatus[4] && canSet[4])
        {
            buffDebuffID = 4;
            BuffDebuff();
        }
        if(getStatus[5] && canSet[5])
        {
            buffDebuffID = 5;
            BuffDebuff();
        }
    }
}
