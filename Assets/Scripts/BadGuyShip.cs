using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyTypes {
	BARRIER,
	BOMBER,
	BURSTER,
	DUMMY,
	MINE,
	SPINNER,
	TRACKER,
}

public abstract class BadGuyShip : MonoBehaviour {
	protected int refund { get; set; }
	protected float hp { get; set; }
	protected abstract int scoreValue { get; }
	protected abstract float lifetime { get; }
	protected abstract float refireDelay { get; }
	
	void Start() {
		StartCoroutine( Decay() );
		StartCoroutine( Fire() );
	}
	
	IEnumerator Fire() {
		if( refireDelay <= 0f )
			yield break;
		
		float f = 0f;
		
		for(;;) {
			while( f < refireDelay ) {
				f += Time.deltaTime;
				yield return null;
			}
			
			FireShot();
			f = 0f;
		}
	}
	
	IEnumerator Decay() {
		yield return new WaitForSeconds( lifetime );
		GracefulExit();
	}
	
	protected virtual void OnCollisionEnter( Collision c ) {
		if( --hp <= 0 ) {
			Destroy( gameObject );
			AudioManager.Explosion();
			Explosion.Play( transform.position );
			GoodGuyShip.AddScore( scoreValue );
		}
	}
	
	protected void GracefulExit() {
		UserInput.AddPellets( refund );
		UserInput.PlaySpawnParticle( transform.position );
		AudioManager.Warp();
		Destroy( gameObject );
	}
	
	public abstract void Initialise( Vector3 position );
	protected abstract void FireShot();
}
