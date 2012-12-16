using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spinner : BadGuyShip {
	protected override int scoreValue { get { return 500; } }
	protected override float lifetime { get { return 15f; } }
	protected override float refireDelay { get { return 0f; } }
	
	public override void Initialise( Vector3 position ) {
		transform.position = position;
		StartCoroutine( Action() );
		
		refund = 7;
		hp = 25;
	}
	
	protected override void FireShot() {
	}
	
	
	IEnumerator Action() {
		Vector3 rate = new Vector3( 0f, 0f, -36f );
		float p = 0f;
		while( p < 1f ) {
			transform.localEulerAngles = rate * Mathf.Sin( p*Mathf.PI/2f );
			p += Time.deltaTime/2f;
			yield return null;
		}
		
		rate = new Vector3( 0f, 0f, 36f );
		
		int cooldown = 0;
		
		while( true ) {
			if( cooldown++ > 6 ) {
				Bullet b = BulletManager.RequestBullet();
				b.Initialise( transform.position, new Vector2( Random.value - 0.5f, Random.value - 0.5f ).normalized * 15f );
				b.gameObject.layer = gameObject.layer+1;
				
				cooldown = 0;
			}
			
			transform.localEulerAngles += rate*Time.deltaTime;
			rate.z += 4f;
			yield return null;
		}
	}
}
