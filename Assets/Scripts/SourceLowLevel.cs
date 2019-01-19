using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceLowLevel : MonoBehaviour
{

    // Use this for initialization
    public bool Pausable;
    FMOD.Sound sonido;
    public string soundName;
    FMOD.Channel source;
    public float volume = 1;
    public int min = 1, max = 1000;
    public bool movable = false;
    public GameObject tecla;
    void Start()
    {
        sonido = new FMOD.Sound();
        SoundManager.sm.loadSound(soundName, out sonido);
        source = new FMOD.Channel();
        SoundManager.sm.setSource(out source, sonido);

        FMOD.VECTOR pos = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out pos, transform.position);
        FMOD.VECTOR vel = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out vel, new Vector3(0, 0, 0));
        FMOD.VECTOR alt_pan = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out alt_pan, Vector3.zero);

        source.set3DMinMaxDistance(1, 1000);

        source.setVolume(volume);
        if (source.set3DAttributes(ref pos, ref vel, ref alt_pan) != FMOD.RESULT.OK) Debug.Log("Nosepue");
        SoundManager.sm.UpdateSM();

    }
    public void Stop()
    {
        source.stop();
        SoundManager.sm.UpdateSM();
    }
    public void Play()
    {
        SoundManager.sm.setSource(out source, sonido);
    }
    private void Update()
    {
        if (movable)
        {

            FMOD.VECTOR pos = new FMOD.VECTOR();
            SoundManager.sm.SetFMODVector(out pos, transform.position);
            FMOD.VECTOR vel = new FMOD.VECTOR();
            SoundManager.sm.SetFMODVector(out vel, new Vector3(0, 0, 0));
            FMOD.VECTOR alt_pan = new FMOD.VECTOR();
            SoundManager.sm.SetFMODVector(out alt_pan, Vector3.zero);

            source.set3DMinMaxDistance(1, 1000);

            source.setVolume(volume);
            if (source.set3DAttributes(ref pos, ref vel, ref alt_pan) != FMOD.RESULT.OK) Debug.Log("Nosepue" + soundName);
            SoundManager.sm.UpdateSM();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Pausable && other.gameObject.CompareTag("Player"))
        {
            tecla.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Pausable && other.gameObject.CompareTag("Player"))
        {
            tecla.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
       if (Input.GetKeyUp(KeyCode.E)&& Pausable && other.gameObject.CompareTag("Player"))
       {
                bool pausado;
                source.getPaused(out pausado);
                source.setPaused(!pausado);
                SoundManager.sm.UpdateSM();

       }

        
    }
}
