using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCactus : MonoBehaviour {

    // Use this for initialization
    public GameObject cactusParticles;
    public GameObject target;
    public GameObject textPanel;
    public Text text;
    float time = 25;
    bool active = false;
    bool talking = false;
    public string[] frases;
    int frase = 0;
	void Start () {
        textPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            Quaternion r = transform.GetChild(1).rotation;
            transform.GetChild(1).LookAt(target.transform,target.transform.up);
            transform.GetChild(1).rotation = new Quaternion(r.x, transform.GetChild(1).rotation.y, r.z, transform.GetChild(1).rotation.w);

            time = time - 1 * Time.deltaTime;
            if(time <= 0)
            {
                time = 25;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Instantiate(cactusParticles, transform.position + new Vector3(0, 2, 0), gameObject.transform.GetChild(0).transform.rotation);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                active = false;
                talking = false;
                frase = 0;
                //textPanel.SetActive(false);
            }
            
        }
        if (talking)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                if (frase < frases.Length)
                {
                    text.text = frases[frase];
                    frase++;
                }
                else
                {
                    textPanel.SetActive(false);
                    frase = 0;
                    talking = false;
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!active)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Instantiate(cactusParticles, transform.position + new Vector3(0, 2, 0), gameObject.transform.GetChild(0).transform.rotation);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                active = true;

            }
            if (!talking)
            {
                textPanel.SetActive(true);
                text.text = frases[frase];
                frase++;
                talking = true;
            }

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("salgo");
            textPanel.SetActive(false);
            talking = false;
            frase = 0;
        }
    }
}
