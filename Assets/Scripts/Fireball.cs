using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public GameObject particleSystem;
	void Start () {
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation), 1.5f);
        Destroy(gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation), 1.5f);
        Destroy(gameObject);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation),1.5f);
        Destroy(gameObject);
    }
}
