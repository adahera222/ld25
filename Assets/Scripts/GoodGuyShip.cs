using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoodGuyShip : MonoBehaviour {
	private static GoodGuyShip ins;
	
	public static Vector3 Position { get { return ins.transform.position; } }
	public static float Centre { get { return -3.5f; } }
	
	private float refireDelay = 0.1f;
	
	private int hp = 50;
	private int maxhp = 50;
	private int lives = 3;
	private int score = 0;
	
	void Awake() {
		ins = this;
	}
	
	IEnumerator Start() {
		UI.SetNumLives( lives );
		UI.SetScore( score = 0 );
		UI.SetShield( hp = maxhp, maxhp );
		StartCoroutine( Fire() );
		StartCoroutine( FireFan() );
		StartCoroutine( FireWideFan() );
		StartCoroutine( ScoreTick() );
		rigidbody.velocity = Vector3.right * 2f;
		
		while( maxhp < 100 ) {
			yield return new WaitForSeconds( 30f );
			maxhp++;
			hp++;
		}
	}
	
	void Reinitialise() {
		AudioManager.ToggleShotLoop( false );
		StopCoroutine( "RegenerateShield" );
		collider.enabled = false;
		transform.Find( "Image" ).renderer.enabled = false;
		BulletManager.ClearBullets();
		transform.position = new Vector3( Centre, -5f );
		rigidbody.velocity = Vector3.zero;
		
		if( lives == 0 ) {
			gameObject.SetActive( false );
			UI.GameOver();
			return;
		}
		
		Invoke( "res", 1.5f );
	}
	
	void res() {
		AudioManager.ToggleShotLoop( true );
		collider.enabled = true;
		transform.Find( "Image" ).renderer.enabled = true;
		transform.position = new Vector3( Centre, -5f );
		rigidbody.velocity = Vector3.right * 2f;
		
		UI.SetNumLives( --lives );
		UI.SetShield( hp = maxhp, maxhp );
		
		StartCoroutine( Fire() );
		StartCoroutine( FireFan() );
		StartCoroutine( FireWideFan() );
		StartCoroutine( ScoreTick() );
	}
	
	#region attacks
	IEnumerator Fire() {
		float f = 0f;
		Vector2 dir = new Vector2( 0f, 20f );
		
		while( true ) {
			while( f < refireDelay ) {
				f += Time.deltaTime;
				yield return null;
			}
			
			if( hp > 0 ) {
				SpawnBullet().Initialise( transform.position, dir );
			}
			f = 0f;
		}
	}
	
	IEnumerator FireFan() {
		while( score < 2000 ) yield return null;
		
		Vector2 d1a = new Vector2( -1f, 2f ).normalized * 20f;
		Vector2 d1b = new Vector2(  1f, 2f ).normalized * 20f;
		
		Vector2 d2a = new Vector2( -1f, 4f ).normalized * 20f;
		Vector2 d2b = new Vector2(  1f, 4f ).normalized * 20f;
		
		bool flag = true;
		
		float f = 0f;
		while( true ) {
			while( f < refireDelay*2f ) {
				f += Time.deltaTime;
				yield return null;
			}
			
			if( hp > 0 ) {
				SpawnBullet().Initialise( transform.position, flag? d1a : d2a );
				SpawnBullet().Initialise( transform.position, flag? d1b : d2b );
			}
			
			flag = !flag;
			f = 0f;
		}
	}
	
	IEnumerator FireWideFan() {
		while( score < 4000 ) yield return null;
		
		Vector2 d1a = new Vector2( -1.5f, 1f ).normalized * 20f;
		Vector2 d1b = new Vector2(  1.5f, 1f ).normalized * 20f;
		
		Vector2 d2a = new Vector2( -3f, 1f ).normalized * 20f;
		Vector2 d2b = new Vector2(  3f, 1f ).normalized * 20f;
		
		bool flag = true;
		
		float f = 0f;
		while( true ) {
			while( f < refireDelay*2f ) {
				f += Time.deltaTime;
				yield return null;
			}
			
			if( hp > 0 ) {
				SpawnBullet().Initialise( transform.position, flag? d1a : d2a );
				SpawnBullet().Initialise( transform.position, flag? d1b : d2b );
			}
			
			flag = !flag;
			f = 0f;
		}
	}
	#endregion
	
	Bullet SpawnBullet() {
		Bullet b = BulletManager.RequestBullet();
		b.gameObject.layer = gameObject.layer+1;
		
		return b;
	}
	
	IEnumerator OnTriggerExit( Collider c ) {
		yield return null;
		
		if( c.name == "GoodGuyTrigger" ) {
			float f = Random.value * 2.5f;
			yield return new WaitForSeconds( f );
			Vector3 v3 = rigidbody.velocity;
			v3.x = -v3.x;
			rigidbody.velocity = v3;
		}
	}
	
	public static void AddScore( int i ) {
		UI.SetScore( ins.score += i );
	}
	
	void OnCollisionEnter( Collision c ) {
		UI.SetShield( --hp, maxhp );
		
		if( hp <= 0 ) {
			AudioManager.Explosion();
			Explosion.Play( transform.position );
			Reinitialise();
		} else {
			StartCoroutine( RegenerateShield() );
		}
	}
	
	IEnumerator RegenerateShield() {
		float ctr = 0f;
		while( hp > 0 && ctr < 15f ) {
			ctr += Time.deltaTime;
			yield return null;
		}
		
		if( ctr >= 15f && hp > 0 )
			UI.SetShield( ++hp, maxhp );
	}
	
	IEnumerator ScoreTick() {
		while( true ) {
			if( hp > 0 ) AddScore( 10 );
			yield return new WaitForSeconds( 1f );
		}
	}
}
