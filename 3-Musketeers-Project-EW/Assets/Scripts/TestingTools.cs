using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTools : MonoBehaviour
{
    public GameObject killbox, pl, ui;
    public bool killAll, UIreset;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UImanager");
    }

    // Update is called once per frame
    void Update()
    {
        if(killAll)
        {
            killAll = false;
            GameObject box = Instantiate(killbox, pl.transform.position, pl.transform.rotation);
        }

        if(UIreset)
        {
            UIreset = false;
            ui.GetComponent<UImanager>().endCanvas.enabled = false;
            ui.GetComponent<UImanager>().mainMenuCanvas.enabled = false;
            ui.GetComponent<UImanager>().settingsCanvas.enabled = false;
            ui.GetComponent<UImanager>().loadingCanvas.enabled = false;
            ui.GetComponent<UImanager>().controlsCanvas.enabled = false;
            ui.GetComponent<UImanager>().pauseCanvas.enabled = false;
            ui.GetComponent<UImanager>().inGameCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }
}
