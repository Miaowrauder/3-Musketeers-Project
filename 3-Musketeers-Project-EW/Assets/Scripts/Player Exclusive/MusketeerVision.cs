using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerVision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] breakables, destroyables, grenadePickups, salvePickups, interactables;
    public GameObject beamPrefab;
    public Canvas overlay;
    bool canUp;
    GameObject ui;
    
    void Start()
    {
        overlay.enabled = false;
        ui = GameObject.FindWithTag("UImanager");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse2) && (ui.GetComponent<UImanager>().hasTrinket[5] == false))
        {
            
            breakables = GameObject.FindGameObjectsWithTag("Breakable");
            destroyables = GameObject.FindGameObjectsWithTag("Destroyable");
            grenadePickups = GameObject.FindGameObjectsWithTag("GrenadePickup");
            salvePickups = GameObject.FindGameObjectsWithTag("ConsumablePickup");
            interactables = GameObject.FindGameObjectsWithTag("Interactable");

            for(int i = 0; i < breakables.Length; i++)
            {
                GameObject beam = Instantiate(beamPrefab, breakables[i].transform.position, Quaternion.identity);
            }
            for(int i = 0; i < destroyables.Length; i++)
            {
                GameObject beam = Instantiate(beamPrefab, destroyables[i].transform.position, Quaternion.identity);;
            }
            for(int i = 0; i < grenadePickups.Length; i++)
            {
                GameObject beam = Instantiate(beamPrefab, grenadePickups[i].transform.position, Quaternion.identity);;
            }
            for(int i = 0; i < salvePickups.Length; i++)
            {
                GameObject beam = Instantiate(beamPrefab, salvePickups[i].transform.position, Quaternion.identity);;
            }
            for(int i = 0; i < interactables.Length; i++)
            {
                GameObject beam = Instantiate(beamPrefab, interactables[i].transform.position, Quaternion.identity);;
            }

            //Time.timeScale = 0.5f;

            overlay.enabled = true;
            canUp = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse2) && canUp)
        {
            canUp = false;

            for(int i = 0; i < breakables.Length; i++)
            {
                breakables[i] = null;
            }
            for(int i = 0; i < destroyables.Length; i++)
            {
                destroyables[i] = null;
            }
            for(int i = 0; i < grenadePickups.Length; i++)
            {
                grenadePickups[i] = null;
            }
            for(int i = 0; i < salvePickups.Length; i++)
            {
                salvePickups[i] = null;
            }
            for(int i = 0; i < interactables.Length; i++)
            {
                interactables[i] = null;
            }
            
            //Time.timeScale = 1f;
            overlay.enabled = false;
            
        }
    }
}
