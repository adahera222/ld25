using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyTypes {
	BOMBER,
	BURSTER,
	DUMMY,
}

public abstract class BadGuyShip : MonoBehaviour {
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
		Destroy( gameObject );
	}
	
	protected virtual void OnCollisionEnter( Collision c ) {
		if( --hp <= 0 ) {
			Destroy( gameObject );
			GoodGuyShip.AddScore( scoreValue );
		}
	}
	
	public abstract void Initialise( Vector3 position );
	protected abstract void FireShot();
}
