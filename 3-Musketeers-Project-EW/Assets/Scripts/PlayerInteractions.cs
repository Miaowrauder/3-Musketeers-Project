using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject hand, cam, player, target;
    public float lookDistance;
    public LayerMask layerMask;
    public LayerMask layerMaskInf;
    public float castRadius;

    public bool castTime;

    RaycastHit hit;

    public GameManager gmSc;
    // Start is called before the first frame update
    void Start()
    {
        gmSc = null;
        gmSc = GameObject.Find("GameManager_DND").GetComponent<GameManager>();
        gmSc.infoText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {

        if(castTime)
        {
            InfiniteRaycast();
            castTime = false;
        }

        if (Physics.SphereCast(cam.transform.position, castRadius, cam.transform.forward, out hit, lookDistance, layerMask)) //add childcount
        {
            if((hand.transform.childCount == 0) && ((hit.collider.gameObject.CompareTag("GrenadePickup")) || hit.collider.gameObject.CompareTag("ConsumablePickup")))
            {
                gmSc.infoText.text = "Depress thine F key to seize!";

                if((hit.collider.gameObject.CompareTag("GrenadePickup")) && (Input.GetKeyDown(KeyCode.F)))
                {
                    gmSc.infoText.text = "";

                    player.GetComponent<OffhandFunctions>().item = (hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    hit.collider.gameObject.transform.parent = hand.transform;
                    hit.collider.gameObject.tag = "Untagged";
                    hit.collider.gameObject.transform.position = hand.transform.position;
                    hit.collider.gameObject.transform.rotation = hand.transform.rotation;
                    StartCoroutine(miniDelayGrenade());
                    
                }
                else if((hit.collider.gameObject.CompareTag("ConsumablePickup")) && (Input.GetKeyDown(KeyCode.F)))
                {
                    gmSc.infoText.text = "";
                    player.GetComponent<OffhandFunctions>().item = (hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    hit.collider.gameObject.transform.parent = hand.transform;
                    hit.collider.gameObject.tag = "Untagged";
                    hit.collider.gameObject.transform.position = hand.transform.position;
                    hit.collider.gameObject.transform.rotation = hand.transform.rotation;
                    hit.collider.gameObject.GetComponent<Salve>().inHand = true;
                    StartCoroutine(miniDelaySalve());
                    
                }
                
            }
            if((hand.transform.childCount == 0) && (hit.collider.gameObject.CompareTag("Interactable")));
            {
                if(hit.collider.gameObject.CompareTag("Interactable")) //this seems redundant, but if I remove it again everything breaks
                {
                gmSc.infoText.text = "Depress thine F key to operate!";
                }

                if((hit.collider.gameObject.CompareTag("Interactable")) && (Input.GetKeyDown(KeyCode.F)))
                {
                    gmSc.infoText.text = "";

                    hit.collider.GetComponent<InteractableObject>().isActivated = true;
                    
                }

            }
            
        }
        else
            {
                gmSc.infoText.text = " ";
            }

    }

    //to prevent grenade being thrown after pickup
    public IEnumerator miniDelayGrenade()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<OffhandFunctions>().hasGrenade = true;
    }
    public IEnumerator miniDelaySalve()
    {
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<OffhandFunctions>().hasSalve = true;
    }

    void InfiniteRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 999f, layerMaskInf))
        {
            if((hit.collider.gameObject.CompareTag("Enemy")))
                {
                    target = hit.collider.gameObject;
                }
                
        }
    }
}

   
