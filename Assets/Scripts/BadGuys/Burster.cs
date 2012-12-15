using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burster : BadGuyShip {
	protected override int scoreValue { get { return 100; } }
	
	public override void Initialise( Vector3 position ) {
		transform.position = position;
		
		refireDelay = 1f;
		hp = 3;
		lifetime = 3.5f;
	}
	
	protected override void FireShot() {
		Vector3 playerPosition = GoodGuyShip.Position;
		Vector3 dir = playerPosition - transform.position;
		
		for( int a = 0 ; a < 3 ; a++ ) {
			Bullet b = BulletManager.RequestBullet();
			b.Initialise( transform.position, 10f * (dir + Vector3.up * Random.Range( -2f, 2f ) + Vector3.right * Random.Range( -2f, 2f )).normalized );
			b.gameObject.layer = gameObject.layer+1;
		}
	}
}
