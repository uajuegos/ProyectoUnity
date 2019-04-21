using UnityEngine;
using TrackerP3;

public class TrackerObject : MonoBehaviour
{
    //Variables configurables en el editor
    public string GameName;
    public SerializerType SerializerType;
    public PersistenceType PersistenceType;
    public float FlushRate;

    private void Awake()
    {
        if (FindObjectsOfType<TrackerObject>().Length > 1)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Tracker.Instance.Init("Beerkings", SerializerType, PersistenceType, FlushRate);
    }

    private void OnApplicationQuit()
    {
        Tracker.Instance.End();
    }

}
