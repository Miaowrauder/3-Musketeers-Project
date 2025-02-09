using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject followTarget, castPos, effectCollider, effectCollPrefab, self;
    public float regenPerSec, duration;
    public LayerMask layerMask;
    RaycastHit hit;
    bool isDestroying;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        effectCollider = Instantiate(effectCollPrefab, this.transform.position, Quaternion.identity);
        effectCollider.GetComponent<RainCollider>().statusDuration = 5f;
        effectCollider.GetComponent<RainCollider>().regenPerSec = regenPerSec;
    }

    // Update is called once per frame
    void Update()
    {
        DownCast();

        this.transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position, 3 * Time.deltaTime);

        if(duration > 0)
        {
            if(!isDestroying)
            {
                isDestroying = true;
                StartCoroutine(DestroySelf());
            }

            
        }

    }

    void DownCast() //quite funny if you ask me
    {
        if (Physics.Raycast(castPos.transform.position, castPos.transform.forward, out hit, 999f, layerMask, QueryTriggerInteraction.Ignore))
        {
            effectCollider.transform.position = hit.point;
        }

    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(duration);
        Destroy(effectCollider);
        Destroy(self);
    }
}
