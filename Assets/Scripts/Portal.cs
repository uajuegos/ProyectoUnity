using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    public string scene;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TrackerObject.tr.tracker.AddEvent(Tracker.EventCreator.Final(Tracker.ActorSubjectType.Player, Tracker.ActorSubjectType.None, "Level " + SceneManager.GetActiveScene().name));
            SceneManager.LoadScene(scene);
        }
    }
}
