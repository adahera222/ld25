using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomber : BadGuyShip {
	protected override int scoreValue { get { return 500; } }
	
	public override void Initialise( Vector3 position ) {
		StartCoroutine( Action( position ) );
		
		lifetime = 999f;
		hp = 2;
		refireDelay = 0f;
	}
	
	protected override void FireShot() {
		Bullet b = BulletManager.RequestBullet();
		b.Initialise( transform.position, -Vector3.up * 20f );
		b.gameObject.layer = gameObject.layer+1;
	}
	
	IEnumerator Action( Vector3 target ) {
		Vector3 start = new Vector3(
			UserInput.left - UserInput.unitWidth * 0.1f,
			UserInput.bottom + UserInput.unitHeight * 1.1f
		);
		
		float dx = Mathf.Abs(start.x - target.x);
		float sqrtHeight = Mathf.Sqrt( UserInput.unitHeight );
		
		bool payloadDelivered = false;
		for( float t = -1f ; t < 1f ; t += Time.deltaTime ) {
			if( !payloadDelivered && t >= 0 ) {
				payloadDelivered = true;
				
				for( int a = 0 ; a < 10 ; a++ ) {
					Bullet b = BulletManager.RequestBullet();
					b.Initialise( transform.position, Random.Range( 8f, 12f ) * new Vector2( Random.Range( -1f, 1f ), -2f ).normalized );
					b.gameObject.layer = gameObject.layer+1;
				}
			}
			
			float x, y;
			x = target.x + dx * t;
			y = Mathf.Pow(sqrtHeight * t, 2f) + target.y;
			
			transform.position = new Vector3( x, y );
			
			yield return null;
		}
		
		Destroy( gameObject );
	}
}
