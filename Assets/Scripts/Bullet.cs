using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	public static readonly float R2D = 180f/Mathf.PI;
	public static readonly float D2R = Mathf.PI/180f;
	
	public void Initialise( Vector2 position, Vector2 velocity ) {
		gameObject.SetActive( true );
		rigidbody.velocity = velocity;
		transform.position = position;
		transform.localEulerAngles = new Vector3( 0f, 0f, Mathf.Atan( velocity.y/velocity.x ) * R2D + 90f );
	}
	
	public void Deactivate() {
		transform.position = Vector3.one * 9999f;
		rigidbody.velocity = Vector3.zero;
		gameObject.SetActive( false );
	}
	
	void OnTriggerExit( Collider c ) {
		BulletManager.ReturnBullet( this );
	}
	
	void OnCollisionEnter( Collision c ) {
		BulletManager.ReturnBullet( this );
	}
}
