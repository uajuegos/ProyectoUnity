using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reverb : MonoBehaviour
{

    // Use this for initialization

    FMOD.Reverb3D myReverb;
    public GameObject teclaE;
    void Start()
    {
        SoundManager.sm.createReverb(out myReverb, transform.position, 0, 100.0f, FMOD.PRESET.UNDERWATER());

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        teclaE.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyUp(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            bool pausado;
            myReverb.getActive(out pausado);
            myReverb.setActive(!pausado);
            SoundManager.sm.UpdateSM();
            GetComponent<MeshRenderer>().enabled = !pausado;

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        teclaE.SetActive(false);
    }
}
