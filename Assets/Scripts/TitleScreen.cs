using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public float count;
	private float _currentPos;
	
	// Update is called once per frame
	private void Update () {
		var transform1 = transform;
		var position = transform1.position;
		position = new Vector3 (position.x + count, position.y, -10);
		transform1.position = position;

		if(Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene ("scene-1");
		}

	}
}
