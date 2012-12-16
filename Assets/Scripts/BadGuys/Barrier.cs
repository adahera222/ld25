using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrier : BadGuyShip {
	protected override int scoreValue { get { return 250; } }
	protected override float lifetime { get { return 10f; } }
	protected override float refireDelay { get { return 0f; } }
	
	public override void Initialise( Vector3 position ) {
		transform.position = position;
		
		hp = 20;
	}
	
	protected override void FireShot() {
	}
}
