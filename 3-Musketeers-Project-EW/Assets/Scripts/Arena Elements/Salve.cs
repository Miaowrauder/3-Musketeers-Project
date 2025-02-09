using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salve : MonoBehaviour
{
    // Start is called before the first frame update
    public int effectID;
    public float effectDuration;
    public GameObject pl;
    public bool inHand;
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if((pl.GetComponent<OffhandFunctions>().isConsuming == true) && inHand)
        {
            SalveEffects();
        }
    }

    void SalveEffects()
    {
        if(pl.GetComponent<MusketeerAbilities>().musketeerID == 2)
        {
            GameObject ui = GameObject.FindWithTag("UImanager");
            ui.GetComponent<UImanager>().musketeerCharge += (6f + (pl.GetComponent<MusketeerAbilities>().musketeerLevel*2));
        }
        if(effectID == 0)
        {
            //burn
            pl.GetComponent<StatusManager>().DotDuration[0] = 0f;
            //poison
            pl.GetComponent<StatusManager>().DotDuration[1] = 0f;
            //regen (yes it cleanses EVERYTHING deal with it)
            pl.GetComponent<StatusManager>().DotDuration[3] = 0f;
            //slow and weak respectively
            pl.GetComponent<StatusManager>().DotDuration[4] = 0f;
            pl.GetComponent<StatusManager>().DotDuration[5] = 0f;
            
            pl.GetComponent<OffhandFunctions>().isConsuming = false;
            pl.GetComponent<OffhandFunctions>().hasSalve = false;

            Destroy(this.gameObject);
        }
        if(effectID == 1)
        {
            //regen 10hp over 10s base
            pl.GetComponent<StatusManager>().DotDuration[3] = effectDuration;
            pl.GetComponent<StatusManager>().DotDmg[3] = (0.5f * (pl.GetComponent<OffhandFunctions>().choiceLevel + 1));

            pl.GetComponent<OffhandFunctions>().isConsuming = false;
            pl.GetComponent<OffhandFunctions>().hasSalve = false;

            Destroy(this.gameObject);
            
        }
    }
}
