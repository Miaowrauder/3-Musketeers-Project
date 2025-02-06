using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerAbilities : MonoBehaviour
{
    public int musketeerLevel, musketeerID;
    public GameObject cloudPrefab;
    public GameObject cloudSpot;

    GameObject ui;

    public int por, ath, ara;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UImanager");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && (ui.GetComponent<UImanager>().musketeerCharge == 100))
        {
            ui.GetComponent<UImanager>().musketeerCharge = 0;
            ui.GetComponent<UImanager>().canSet = true;
            ui.GetComponent<UImanager>().musketext.text = " -X- ";

            if(musketeerID == 1)
            {
                por++;
            }
            else if(musketeerID == 2)
            {
                GameObject cloud = Instantiate(cloudPrefab, cloudSpot.transform.position, Quaternion.identity);
                cloud.GetComponent<CloudController>().followTarget = cloudSpot;
                cloud.GetComponent<CloudController>().duration = (10f + (musketeerLevel * 2));
                cloud.GetComponent<CloudController>().regenPerSec = (10f + (musketeerLevel * 5));

            }
            else if(musketeerID == 3)
            {
                ara++;
            }
        }
    }
}
