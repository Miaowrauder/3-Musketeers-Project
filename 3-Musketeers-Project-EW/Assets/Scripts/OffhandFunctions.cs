using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffhandFunctions : MonoBehaviour
{
    public Transform hand;

    public GameObject[] abilityItems;

    public GameObject previewGrenade, item; //current held item, dont change in editor
    public bool offhandEnabled, hasGrenade, hasSalve, hasMusket, isScaling, isConsuming;
    
    public int abilityID, choiceLevel;

    public GameObject cooldownManager;

    public float baseThrowForce, throwForce;
    void Start()
    {
        offhandEnabled = true;
        hasGrenade = false;
        hasSalve = false;
        hasMusket = false;
        throwForce = baseThrowForce;
    }

       void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) && (cooldownManager.GetComponent<cooldownManager>().choiceCD == 0) && (hand.transform.childCount == 0))
        {   
            //summons item corresponding to choice ability id in hand
            item = Instantiate(abilityItems[abilityID], hand.transform.position, Quaternion.identity);
            item.transform.parent = hand.transform;
            item.transform.position = hand.transform.position;
            item.transform.rotation = hand.transform.rotation;
            item.GetComponent<Rigidbody>().isKinematic = true;

            if((abilityID >= 0) && (abilityID <= 1))
            {
                hasGrenade = true;
            }

            if((abilityID == 999))
            {
                hasMusket = true;
            }

            if((abilityID >= 2) && (abilityID <= 3))
            {
                hasSalve = true;
            }

        }

        
        if (Input.GetKey(KeyCode.F))
        {
            if(hasGrenade)
            {
            if(isScaling == false)
            {
                StartCoroutine(ThrowScale());
            }
            }

        }

        if (Input.GetKeyDown(KeyCode.F))
        {       
            if(hasMusket)
            {

            }

            if(hasSalve)
            {
                isConsuming = true;              
            }
        }

        //grenade throw
        if (Input.GetKeyUp(KeyCode.F) && hasGrenade)
        {
            
            item.GetComponent<Rigidbody>().isKinematic = false;
            //-up is forward
            item.gameObject.GetComponent<Rigidbody>().AddForce(item.transform.forward * throwForce, ForceMode.Impulse);
            //negative forward is up
            item.gameObject.GetComponent<Rigidbody>().AddForce(item.transform.up * (throwForce/2), ForceMode.Impulse);
            item.gameObject.transform.parent = null;
            item.GetComponent<Grenade>().isPrimed = true;
            throwForce = baseThrowForce;
            hasGrenade = false;
        }
    }
    
    //grenade throw charge, scales throw strength with a preview grenade thrown every second
    public IEnumerator ThrowScale()
    {
        isScaling = true;

        GameObject preview = Instantiate(previewGrenade, hand.transform.position, hand.transform.rotation);

            preview.GetComponent<Rigidbody>().isKinematic = false;
            preview.gameObject.GetComponent<Rigidbody>().AddForce(preview.transform.forward * throwForce, ForceMode.Impulse);
            preview.gameObject.GetComponent<Rigidbody>().AddForce(preview.transform.up * (throwForce/2), ForceMode.Impulse);
            preview.gameObject.transform.parent = null;
            preview.GetComponent<Grenade>().isPrimed = true;

        yield return new WaitForSeconds(0.5f);
        throwForce += 1.5f;

        isScaling = false;
    }

    }

