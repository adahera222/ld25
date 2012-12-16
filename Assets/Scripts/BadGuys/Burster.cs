using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burster : BadGuyShip {
	protected override int scoreValue { get { return 100; } }
	protected override float lifetime { get { return 5.75f; } }
	protected override float refireDelay { get { return 1f; } }
	
	void Update() {
		Vector3 v3 = GoodGuyShip.Position - transform.position;// transform.position - GoodGuyShip.Position;
		float z = Mathf.Atan( v3.y/v3.x ) * Bullet.R2D - 90f;
		if( v3.x < 0f ) z += 180f;
		transform.localEulerAngles = new Vector3( 0f, 0f, z );
	}
	
	public override void Initialise( Vector3 position ) {
		transform.position = position;
		
		refund = 2;
		hp = 10;
	}
	
	protected override void FireShot() {
		Vector3 playerPosition = GoodGuyShip.Position;
		Vector3 dir = playerPosition - transform.position;
		
		for( int a = 0 ; a < 2 ; a++ ) {
			Bullet b = BulletManager.RequestBullet();
			b.Initialise( transform.position, 10f * (dir + Vector3.up * Random.Range( -2f, 2f ) + Vector3.right * Random.Range( -2f, 2f )).normalized );
			b.gameObject.layer = gameObject.layer+1;
		}
	}
}
