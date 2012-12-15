using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoodGuyShip : MonoBehaviour {
	private static GoodGuyShip ins;
	private delegate void FireUpgrade();
	
	public static Vector3 Position { get { return ins.transform.position; } }
	
	private float refireDelay = 0.05f;
	
	private int hp = 25;
	private int maxhp = 25;
	private int bombs = 3;
	private int score = 0;
	
	private event FireUpgrade fireUpgrade;
	
	void Awake() {
		ins = this;
	}
	
	void Start() {
		UI.SetScore( score = 0 );
		UI.SetShield( hp = maxhp, maxhp );
		StartCoroutine( Fire() );
		rigidbody.velocity = Vector3.right * 2f;
	}
	
	void Reinitialise() {
		BulletManager.ClearBullets();
		StopAllCoroutines();
		
		UI.SetShield( hp = maxhp, maxhp );
		transform.position = new Vector3( 0f, -5f );
		
		StartCoroutine( Fire() );
	}
	
	IEnumerator Fire() {
		float f = 0f;
		
		while( true ) {
			while( f < refireDelay ) {
				f += Time.deltaTime;
				yield return null;
			}
			
			SpawnBullet();
			f = 0f;
		}
	}
	
	void SpawnBullet() {
		Bullet b = BulletManager.RequestBullet();
		b.Initialise( transform.position, Vector3.up * 20f );
		b.gameObject.layer = gameObject.layer+1;
	}
	
	IEnumerator OnTriggerExit( Collider c ) {
		yield return null;
		
		if( c.name == "GoodGuyTrigger" ) {
			float f = Random.value * 3f;
			yield return new WaitForSeconds( f );
			Vector3 v3 = rigidbody.velocity;
			v3.x = -v3.x;
			rigidbody.velocity = v3;
		}
	}
	
	public static void AddScore( int i ) {
		UI.SetScore( ins.score += i );
	}
	
	IEnumerator OnCollisionEnter( Collision c ) {
		UI.SetShield( --hp, maxhp );
		
		if( hp <= 0 ) {
			Reinitialise();
			yield break;
		} else {
			yield return new WaitForSeconds( 15f );
			
			UI.SetShield( ++hp, maxhp );
		}
		
	}
}
