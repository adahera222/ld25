using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dummy : BadGuyShip {
	protected override int scoreValue { get { return 50; } }
	protected override float lifetime { get { return 60f; } }
	protected override float refireDelay { get { return 1.5f; } }
	
	public override void Initialise( Vector3 position ) {
		StartCoroutine( Action() );
		StartCoroutine( Rotate() );
		
		hp = 2;
	}
	
	protected override void FireShot() {
		Bullet b = BulletManager.RequestBullet();
		b.Initialise( transform.position, 5f * (Vector3.down * 10f + Vector3.right * Random.Range( -1f, 1f )).normalized );
		b.gameObject.layer = gameObject.layer+1;
	}
	
	
	IEnumerator Action() {
		transform.position = new Vector3( GoodGuyShip.Centre + Random.Range( -5f, 5f ), UserInput.bottom + UserInput.unitHeight*1.1f );
		
		while( transform.position.y > UserInput.bottom-1f ) {
			transform.position += Vector3.down * Time.deltaTime;
			yield return null;
		}
		
		Destroy( gameObject );
	}
	
	IEnumerator Rotate() {
		transform.localEulerAngles = new Vector3( 0f, 0f, Random.Range( 0f, 360f ) );
		Vector3 rot = new Vector3( 0f, 0f, 60f );
		while( true ) {
			transform.localEulerAngles += rot * Time.deltaTime;
			yield return null;
		}
	}
}
