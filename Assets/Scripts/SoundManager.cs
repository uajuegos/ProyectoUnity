using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMOD;

public class SoundManager : MonoBehaviour {

    public static SoundManager sm;
    FMOD.Studio.System system;
    FMOD.System lowLevelSystem;
    FMOD.Studio.Bank masterBank;
    FMOD.Studio.Bank stringBank;
    FMOD.Studio.Bank musicBank;
    FMOD.Studio.Bank fxBank;


    string path;
    private void Awake()
    {
        if (sm == null)
        {
            //PlayerPrefs.DeleteAll();
            sm = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (sm != this)
        {
            Destroy(gameObject);
        }

        FMOD.Studio.System.create(out sm.system);

        // The example Studio project is authored for 5.1 sound, so set up the system output mode to match

        sm.system.getLowLevelSystem(out sm.lowLevelSystem);
        sm.lowLevelSystem.setSoftwareFormat(0, SPEAKERMODE._5POINT1, 0);
        sm.system.initialize(1024, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, System.IntPtr.Zero);

        path = Application.dataPath + "/FMODStudio/Sonidito/Banks";
        // Carganmos Bancos de sonidos
        if (sm.system.loadBankFile(path + "/Master Bank.bank", LOAD_BANK_FLAGS.NORMAL, out sm.masterBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Master Bank.strings.bank", LOAD_BANK_FLAGS.NORMAL, out sm.stringBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Musicas.bank", LOAD_BANK_FLAGS.NORMAL, out sm.musicBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Fx.bank", LOAD_BANK_FLAGS.NORMAL, out sm.fxBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");

    }
    // Use this for initialization
    void Start () {
       
        

        
    }
	
    public void UpdateSM()
    {
        sm.system.update();
    }

    public void getEvtinstance(string evt, out EventInstance evtInstance)
    {
        FMOD.Studio.EventDescription description;
       if( sm.system.getEvent(evt, out description) != RESULT.OK) UnityEngine.Debug.Log("NoVa");
        description.loadSampleData();
        if (description.createInstance(out evtInstance) != RESULT.OK) UnityEngine.Debug.Log("NoVa2");
        
    }
    public FMOD.Studio.Bank MusicBank
    {
        get { return sm.musicBank; }
    }
  
    private void OnApplicationQuit()
    {
        sm.system.release();
        
    }

    public void SetListener(Vector3 pos, Vector3 vel_, Vector3 at_, Vector3 up_)
    {
        //listenerVel = { 0,0,0 }, up = { 0,1,0 }, at = { 0,0,-1 };
        FMOD.ATTRIBUTES_3D attributes = new FMOD.ATTRIBUTES_3D();
        SetFMODVector(out attributes.position, pos);
        SetFMODVector(out attributes.velocity, vel_);
        SetFMODVector(out attributes.up, up_);
        SetFMODVector(out attributes.forward, at_);

        sm.system.setListenerAttributes(0, attributes);
        UpdateSM();
    }
    public void SetFMODVector(out FMOD.VECTOR v, Vector3 vAux)
    {
        v.x = vAux.x;
        v.y = vAux.y;
        v.z = vAux.z;
    }
}
