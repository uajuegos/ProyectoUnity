using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TrackerP3;

public class Portal : MonoBehaviour {
    public string scene;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Tracker.Instance.AddEvent(EventCreator.Final(ActorSubjectType.Player, ActorSubjectType.None, "Level " + SceneManager.GetActiveScene().name));
            SceneManager.LoadScene(scene);
        }
    }
}
