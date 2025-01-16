using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject hand, cam, player;
    public float lookDistance;
    public LayerMask layerMask;
    public float castRadius;

    RaycastHit hit;

    GameManager gmSc;
    // Start is called before the first frame update
    void Start()
    {
        gmSc = GameObject.Find("GameManager/DND").GetComponent<GameManager>();
        gmSc.infoText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {

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
                    StartCoroutine(miniDelay());
                    
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
                    
                }
                
            }
            
        }
        else
            {
                gmSc.infoText.text = " ";
            }

    }

    //to prevent grenade being thrown after pickup
    public IEnumerator miniDelay()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<OffhandFunctions>().hasGrenade = true;
    }
}

   
