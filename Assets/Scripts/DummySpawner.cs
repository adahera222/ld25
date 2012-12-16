using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummySpawner : MonoBehaviour {
	public Dummy dummyPrefab;
	
	IEnumerator Start() {
		while( true ) {
			yield return new WaitForSeconds( 1f );
			
			Dummy d = Instantiate( dummyPrefab ) as Dummy;
			d.Initialise( Vector3.zero );
		}
	}
}
