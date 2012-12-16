using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : BadGuyShip {
	protected override int scoreValue { get { return 150; } }
	protected override float lifetime { get { return 30f; } }
	protected override float refireDelay { get { return 0f; } }
	
	private float burstLength = 5;
	private float burstCount = 0;
		
	public override void Initialise( Vector3 position ) {
		transform.position = position;
		
		StartCoroutine( Rotate() );
		StartCoroutine( Drop() );
		
		refund = 0;
		hp = 1;
	}
	
	protected override void FireShot() {
	}
	
	protected override void OnCollisionEnter( Collision c ) {
		base.OnCollisionEnter( c );
		
		Detonate();
	}
	
	void Detonate() {
		for( int a = 0 ; a < 3 ; a++ ) {
			Bullet b = BulletManager.RequestBullet();
			b.Initialise( transform.position, 10f * new Vector2( -1f, -0.125f + 0.125f*a ).normalized );
			b.gameObject.layer = gameObject.layer+1;
		}
		
		for( int a = 0 ; a < 3 ; a++ ) {
			Bullet b = BulletManager.RequestBullet();
			b.Initialise( transform.position, 10f * new Vector2( 1f, -0.125f + 0.125f*a ).normalized );
			b.gameObject.layer = gameObject.layer+1;
		}
	}
	
	IEnumerator Rotate() {
		transform.localEulerAngles = new Vector3( 0f, 0f, Random.Range( 0f, 360f ) );
		Vector3 rot = new Vector3( 0f, 0f, Random.value * 60f - 30f );
		while( true ) {
			transform.localEulerAngles += rot * Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator Drop() {
		Vector3 v = Vector3.down;
		while( transform.position.y > -5f ) {
			transform.position += v * Time.deltaTime;
			yield return null;
		}
		
		Detonate();
		Explosion.Play( transform.position );
		Destroy( gameObject );
	}
}
