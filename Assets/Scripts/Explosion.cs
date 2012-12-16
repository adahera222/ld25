using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour {
	private static Explosion ins;
	
	void Awake() {
		ins = this;
	}
	
	public static void Play( Vector3 pt ) {
		ins.transform.position = pt;
		ins.particleSystem.Play();
	}
}
