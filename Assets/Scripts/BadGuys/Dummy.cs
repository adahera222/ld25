using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dummy : BadGuyShip {
	protected override int scoreValue { get { return 50; } }
	protected override float lifetime { get { return 60f; } }
	protected override float refireDelay { get { return 1.5f; } }
	
	public override void Initialise( Vector3 position ) {
		StartCoroutine( Action() );
		hp = 10;
	}
	
	protected override void FireShot() {
		Bullet b = BulletManager.RequestBullet();
		b.Initialise( transform.position, 5f * (Vector3.down * 10f + Vector3.right * Random.Range( -1f, 1f )).normalized );
		b.gameObject.layer = gameObject.layer+1;
	}
	
	
	IEnumerator Action() {
		transform.position = new Vector3( 0.25f*Random.Range( -UserInput.unitWidth, UserInput.unitWidth ), UserInput.bottom + UserInput.unitHeight*1.1f );
		
		while( true ) {
			transform.position += Vector3.down * Time.deltaTime;
			yield return null;
		}
	}
}
