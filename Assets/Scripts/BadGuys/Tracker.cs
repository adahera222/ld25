using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tracker : BadGuyShip {
	protected override int scoreValue { get { return 750; } }
	protected override float lifetime { get { return 10f; } }
	protected override float refireDelay { get { return 0.1f; } }
	
	private float burstLength = 5;
	private float burstCount = 0;
	
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
		if( burstCount < 0 ) return;
		
		Vector3 playerPosition = GoodGuyShip.Position;
		Vector3 dir = playerPosition - transform.position;
		
		Bullet b = BulletManager.RequestBullet();
		b.Initialise( transform.position, 10f * (dir + Vector3.right * dir.magnitude/7.5f * Random.Range( -1f, 1f )).normalized );
		b.gameObject.layer = gameObject.layer+1;
		
		if( ++burstCount >= burstLength ) {
			burstCount = -1;
			StartCoroutine( Refresh() );
		}
	}
	
	IEnumerator Refresh() {
		yield return new WaitForSeconds( 2f );
		burstCount = 0;
	}
}
