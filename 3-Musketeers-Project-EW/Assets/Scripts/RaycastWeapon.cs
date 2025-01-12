using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public float damage;
    public float shootDistance;
    public LayerMask layerMask;
    public GameObject cam;
    RaycastHit hit;

    public bool shotFired;




    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootDistance, layerMask))
                {
                    shotFired = true;
                    hit.collider.gameObject.GetComponent<Health>().hp -= damage;
                    shotFired = false;

                }
            }
        }
    }
}
