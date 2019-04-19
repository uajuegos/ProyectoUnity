using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerObject : MonoBehaviour {

    public static TrackerObject tr;
    public Tracker.Tracker tracker;
    // Use this for initialization
    void Start () {
        if (tr == null)
        {
            tr = this;
            DontDestroyOnLoad(this.gameObject);
            tracker = Tracker.Tracker.Instance;
        }
        else
            Destroy(this.gameObject);
        

    }
	

}
