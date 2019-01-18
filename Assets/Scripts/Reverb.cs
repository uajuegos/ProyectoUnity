using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reverb : MonoBehaviour
{

    // Use this for initialization
    public string ReverbType;
    public int minDistance;
    public int maxDistance;
    FMOD.REVERB_PROPERTIES myProp;
    FMOD.Reverb3D myReverb;
    public GameObject teclaE;
    void Start()
    {
        #region SWITCH_MYPROP
        switch (ReverbType)
        {
            case "OFF":
                myProp = FMOD.PRESET.OFF();
                break;
            case "GENERIC":
                myProp = FMOD.PRESET.GENERIC();
                break;
            case "PADDEDCELL":
                myProp = FMOD.PRESET.PADDEDCELL();
                break;
            case "ROOM":
                myProp = FMOD.PRESET.ROOM();
                break;
            case "BATHROOM":
                myProp = FMOD.PRESET.BATHROOM();
                break;
            case "LIVINGROOM":
                myProp = FMOD.PRESET.LIVINGROOM();
                break;
            case "STONEROOM":
                myProp = FMOD.PRESET.STONEROOM();
                break;
            case "AUDITORIUM":
                myProp = FMOD.PRESET.AUDITORIUM();
                break;
            case "CONCERTHALL":
                myProp = FMOD.PRESET.CONCERTHALL();
                break;
            case "CAVE":
                myProp = FMOD.PRESET.CAVE();
                break;
            case "ARENA":
                myProp = FMOD.PRESET.ARENA();
                break;
            case "HANGAR":
                myProp = FMOD.PRESET.HANGAR();
                break;
            case "CARPETTEDHALLWAY":
                myProp = FMOD.PRESET.CARPETTEDHALLWAY();
                break;
            case "HALLWAY":
                myProp = FMOD.PRESET.HALLWAY();
                break;
            case "STONECORRIDOR":
                myProp = FMOD.PRESET.STONECORRIDOR();
                break;
            case "ALLEY":
                myProp = FMOD.PRESET.ALLEY();
                break;
            case "FOREST":
                myProp = FMOD.PRESET.FOREST();
                break;
            case "CITY":
                myProp = FMOD.PRESET.CITY();
                break;
            case "MOUNTAINS":
                myProp = FMOD.PRESET.MOUNTAINS();
                break;
            case "QUARRY":
                myProp = FMOD.PRESET.QUARRY();
                break;
            case "PLAIN":
                myProp = FMOD.PRESET.PLAIN();
                break;
            case "PARKINGLOT":
                myProp = FMOD.PRESET.PARKINGLOT();
                break;
            case "SEWERPIPE":
                myProp = FMOD.PRESET.SEWERPIPE();
                break;
            case "UNDERWATER":
                myProp = FMOD.PRESET.UNDERWATER();
                break;
            default:
                Debug.Log("Preset " + ReverbType + " was not found");
                break;
        }
        #endregion

        SoundManager.sm.createReverb(out myReverb, transform.position, minDistance, maxDistance, myProp);

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
