using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffhandFunctions : MonoBehaviour
{
    public Transform holdPosition;

    public GameObject[] grenades, muskets, salves;

    public bool offhandEnabled, hasGrenade, hasSalve, hasMusket; // could have hasItem[] but this is visually clearer

    // Start is called before the first frame update
    void Start()
    {
        offhandEnabled = true;
        hasGrenade = false;
        hasSalve = false;
        hasMusket = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
