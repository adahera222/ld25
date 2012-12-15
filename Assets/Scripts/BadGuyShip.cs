using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyTypes {
	BOMBER,
	BURSTER,
}

public abstract class BadGuyShip : MonoBehaviour {	
	public float hp { get; protected set; }
	
	public float refireDelay = 0.2f;
	
	protected float lifetime;
	
	protected abstract int scoreValue { get; }
	
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
