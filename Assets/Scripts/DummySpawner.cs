using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummySpawner : MonoBehaviour {
	public GameObject badGuyPrefab;
	
	IEnumerator Start() {
		while( true ) {
			yield return new WaitForSeconds( 1f );
			
			GameObject g = (GameObject)Instantiate( badGuyPrefab ) as GameObject;
			BadGuyShip b = g.AddComponent<Dummy>();
			b.Initialise( Vector3.zero );
		}
	}
}
