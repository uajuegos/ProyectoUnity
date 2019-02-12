using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCactus : MonoBehaviour {

    // Use this for initialization
    public GameObject cactusParticles;
    public GameObject target;
    float time = 25;
    bool active = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            Quaternion r = transform.rotation;
            transform.LookAt(target.transform,target.transform.up);
            transform.rotation = new Quaternion(r.x, transform.rotation.y, r.z, transform.rotation.w);

            time = time - 1 * Time.deltaTime;
            if(time <= 0)
            {
                time = 25;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Instantiate(cactusParticles, transform.position + new Vector3(0, 2, 0), gameObject.transform.GetChild(0).transform.rotation);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                active = false;
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(cactusParticles, transform.position + new Vector3(0, 2, 0), gameObject.transform.GetChild(0).transform.rotation);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            active = true;
        }

    }
}
