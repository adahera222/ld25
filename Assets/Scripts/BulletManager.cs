using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	private static BulletManager ins;
	
	public GameObject bulletPrefab;
	
	private Queue<Bullet> bullets;
	private Transform bulletContainer;
	
	void Awake() {
		ins = this;
		bullets = new Queue<Bullet>();
		bulletContainer = new GameObject( "BulletContainer" ).transform;
		bulletContainer.transform.parent = transform;
	}
	
	void Start() {
		for( int a = 0 ; a < 250 ; a++ ) {
			GameObject go = (GameObject)Instantiate( ins.bulletPrefab );
			Bullet b = go.GetComponent<Bullet>();
			b.transform.parent = bulletContainer;
			b.Deactivate();
			
			bullets.Enqueue( b );
		}
	}
	
	public static void ClearBullets() {
		foreach( Transform t in ins.bulletContainer ) {
			ReturnBullet( t.GetComponent<Bullet>() );
		}
	}
	
	public static Bullet RequestBullet() {
		if( ins.bullets.Count > 0 ) {
			return ins.bullets.Dequeue();
		}
		
		GameObject go = (GameObject)Instantiate( ins.bulletPrefab );
		Bullet b = go.GetComponent<Bullet>();
		b.transform.parent = ins.bulletContainer;
		
		return b;
	}
	
	public static void ReturnBullet( GameObject go ) {
		ReturnBullet( go.GetComponent<Bullet>() );
	}
	
	public static void ReturnBullet( Bullet b ) {
		b.Deactivate();
		ins.bullets.Enqueue( b );
	}
}
