using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPelletGroup : MonoBehaviour {
	public Renderer[] pellets;
	
	public void SetNumActivePellets( int i ) {
		for( int a = 0 ; a < pellets.Length ; a++ ) {
			pellets[a].enabled = a < i;
		}
	}
}
