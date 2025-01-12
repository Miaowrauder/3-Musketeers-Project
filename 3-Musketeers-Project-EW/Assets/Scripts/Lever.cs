using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOn;

    public float moveSpeed;
    public Transform Bridge;
    public Transform pointA;
    public Transform pointB;
    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Animator>().SetBool("leverOn", isOn);

        BridgeFunction();
    }

    void BridgeFunction()
    {
        if (isOn)
        {
            Bridge.position = Vector3.Lerp(Bridge.position, pointA.position, moveSpeed * Time.deltaTime);
        }
        else if (!isOn)
        {
            Bridge.position = Vector3.Lerp(Bridge.position, pointB.position, moveSpeed * Time.deltaTime);
        }
    }

}
