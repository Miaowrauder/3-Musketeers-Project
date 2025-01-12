using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject hand;
    public GameObject cam;
    public float lookDistance;
    public LayerMask layerMask;
    public Collider triggerColl;

    RaycastHit hit;

    GameManager gmSc;
    // Start is called before the first frame update
    void Start()
    {
        gmSc = GameObject.Find("GameManager").GetComponent<GameManager>();
        gmSc.infoText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
       
       //General Interactions

        if(triggerColl != null && Input.GetKeyDown(KeyCode.F))
        {
            if(triggerColl.gameObject.CompareTag("Lever"))
            {
                Lever leverSc = triggerColl.gameObject.GetComponent<Lever>();
                leverSc.isOn = !leverSc.isOn;
            }
        }

        //Weapons

        if(hand.transform.childCount == 1 && Input.GetKeyDown(KeyCode.F))
        {
            hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            hand.transform.GetChild(0).gameObject.transform.parent = null;
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, lookDistance, layerMask))
        {
            gmSc.infoText.text = "Press F to equip";

            if(hand.transform.childCount == 0 && Input.GetKeyDown(KeyCode.F))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.gameObject.transform.parent = hand.transform;
                hit.collider.gameObject.transform.position = hand.transform.position;
                hit.collider.gameObject.transform.rotation = hand.transform.rotation;
            }
        }
        else
        {
            if(triggerColl == null)
            {
                gmSc.infoText.text = " ";
            }
        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward * lookDistance, Color.yellow);
    }

    void OnTriggerEnter(Collider other)
    {
        triggerColl = other;

        if (other.gameObject.CompareTag("Lever"))
        {
            gmSc.infoText.text = "Press E to use";
        }
    }

    void OnTriggerExit (Collider other)
    {
        triggerColl = null;     
    }
}
