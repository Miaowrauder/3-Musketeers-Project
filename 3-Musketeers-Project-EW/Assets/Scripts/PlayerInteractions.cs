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
}