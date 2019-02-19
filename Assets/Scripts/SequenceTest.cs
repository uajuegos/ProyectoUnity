using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceTest : MonoBehaviour {

    private KeyCode[] sequence = new KeyCode[] { KeyCode.P, KeyCode.A, KeyCode.S, KeyCode.S };

    private int sequenceIndex;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(sequence[sequenceIndex]))
        {
            if (++sequenceIndex == sequence.Length)
            {
                sequenceIndex = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (Input.anyKeyDown)
        {
            sequenceIndex = 0;
        }
	}
}
