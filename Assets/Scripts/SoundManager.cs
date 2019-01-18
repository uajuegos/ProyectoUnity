using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMOD;

public class SoundManager : MonoBehaviour
{

    public static SoundManager sm;
    FMOD.Studio.System system;
    FMOD.System lowLevelSystem;
    FMOD.Studio.Bank masterBank;
    FMOD.Studio.Bank stringBank;
    FMOD.Studio.Bank musicBank;
    FMOD.Studio.Bank fxBank;
    FMOD.Geometry geometry;
    int numPolygons;
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


        sm.numPolygons = GameObject.FindGameObjectsWithTag("Geometry").Length;
        UnityEngine.Debug.Log(numPolygons);
        FMOD.Studio.System.create(out sm.system);

        // The example Studio project is authored for 5.1 sound, so set up the system output mode to match

        sm.system.getLowLevelSystem(out sm.lowLevelSystem);
        sm.lowLevelSystem.setSoftwareFormat(0, SPEAKERMODE._5POINT1, 0);
        sm.system.initialize(1024, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, System.IntPtr.Zero);

        path = Application.dataPath + "/FMODStudio/Sonidito/Banks/Desktop";
        // Carganmos Bancos de sonidos
        if (sm.system.loadBankFile(path + "/Master Bank.bank", LOAD_BANK_FLAGS.NORMAL, out sm.masterBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Master Bank.strings.bank", LOAD_BANK_FLAGS.NORMAL, out sm.stringBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Musicas.bank", LOAD_BANK_FLAGS.NORMAL, out sm.musicBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");
        if (sm.system.loadBankFile(path + "/Fx.bank", LOAD_BANK_FLAGS.NORMAL, out sm.fxBank) != RESULT.OK) UnityEngine.Debug.Log("Nosepue");


        FMOD.VECTOR posPolygon = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out posPolygon, Vector3.zero);
        SetGeometryPosition(ref posPolygon);
        CreateGeometry();

    }
    // Use this for initialization
    void Start()
    {




    }
    public void UpdateSM()
    {
        sm.system.update();
    }
    #region LOWLEVEL
    public void loadSound(string name, out FMOD.Sound sound)
    {
        //Cargar sonido 
        lowLevelSystem.createSound(Application.dataPath + "/Sounds/" + name, FMOD.MODE._3D | FMOD.MODE.LOOP_NORMAL, out sound);
    }

    public void setSource(out FMOD.Channel channel, FMOD.Sound sound)
    {
        lowLevelSystem.playSound(sound, new FMOD.ChannelGroup(), false, out channel);
    }
    public void CreateGeometry()
    {

        sm.geometry = new FMOD.Geometry();
        UnityEngine.Debug.Log(lowLevelSystem.createGeometry(sm.numPolygons * 3, sm.numPolygons * 12, out sm.geometry));

    }
    public void AddPolygon(float dOclusion, float rOclusion, bool doubleSided, int numVertex, FMOD.VECTOR[] indexV, out int index)
    {
        sm.geometry.addPolygon(dOclusion, rOclusion, doubleSided, numVertex, indexV, out index);


    }
    public void SetGeometryPosition(ref FMOD.VECTOR posPolygon)
    {
        sm.geometry.setPosition(ref posPolygon);
    }
    public void SetGeometryRotation(ref FMOD.VECTOR forward, ref FMOD.VECTOR up)
    {
        sm.geometry.setRotation(ref forward, ref up);
    }

    public void createReverb(out FMOD.Reverb3D reverb, Vector3 pos, float minD, float maxD, FMOD.REVERB_PROPERTIES prop)
    {
        reverb = new FMOD.Reverb3D();
        lowLevelSystem.createReverb3D(out reverb);
        FMOD.REVERB_PROPERTIES prop2 = prop;
        reverb.setProperties(ref prop2);
        FMOD.VECTOR posR;
        sm.SetFMODVector(out posR, pos);

        reverb.set3DAttributes(ref posR, minD, maxD);
        sm.UpdateSM();
    }
    public int NumPolygons
    {
        get { return numPolygons; }
        set { numPolygons = value; }
    }
    #endregion
    #region STUDIO
    public void getEvtinstance(string evt, out EventInstance evtInstance)
    {
        FMOD.Studio.EventDescription description;
        if (sm.system.getEvent(evt, out description) != RESULT.OK) UnityEngine.Debug.Log("NoVa " + evt);
        description.loadSampleData();
        if (description.createInstance(out evtInstance) != RESULT.OK) UnityEngine.Debug.Log("NoVa2");

    }
    public FMOD.Studio.Bank MusicBank
    {
        get { return sm.musicBank; }
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
    #endregion



    public void SetFMODVector(out FMOD.VECTOR v, Vector3 vAux)
    {
        v.x = vAux.x;
        v.y = vAux.y;
        v.z = vAux.z;
    }

    private void OnApplicationQuit()
    {
        geometry.clearHandle();
        masterBank.unload();
        stringBank.unload();
        musicBank.unload();
        fxBank.unload();
        sm.system.release();

    }
}
