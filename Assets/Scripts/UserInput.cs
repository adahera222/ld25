using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {
	public GameObject badGuyPrefab;
	
	public static float unitWidth { private set; get; }
	public static float unitHeight { private set; get; }
	public static float left { private set; get; }
	public static float bottom { private set; get; }
	
	private EnemyTypes selection = EnemyTypes.BURSTER;
	
	private int numPellets = 0;
	private int maxPellets = 40;
	
	void Start() {
		unitHeight = Mathf.Abs( Camera.main.transform.position.z ) * Mathf.Tan( Camera.main.fov * Mathf.PI/180f );
		unitWidth = unitHeight * Camera.main.aspect;
		
		left = unitWidth * -0.5f;
		bottom = unitHeight * -0.5f;
		
		UI.SetNumPellets( numPellets = 0);
		StartCoroutine( GeneratePellets() );
		
		Debug.Log( left+" "+bottom+"\n"+unitWidth+" "+unitHeight );
	}
	
	void Update() {
		if( Input.GetKeyDown( KeyCode.Alpha1 ) ) {
			selection = EnemyTypes.BURSTER;
			UI.SetSelection( selection.ToString() );
		} else if( Input.GetKeyDown( KeyCode.Alpha2 ) ) {
			selection = EnemyTypes.BOMBER;
			UI.SetSelection( selection.ToString() );
		}
		
		if( Input.GetMouseButtonDown( 0 ) ) {
			Debug.Log( "input: "+Input.mousePosition );
			switch( selection ) {
			case EnemyTypes.BOMBER:
				if( numPellets < 10 ) break;
				numPellets -= 10;
				Spawn<Bomber>( Input.mousePosition );
				break;
				
			case EnemyTypes.BURSTER:
				if( numPellets < 5 ) break;
				numPellets -= 5;
				Spawn<Burster>( Input.mousePosition );
				break;
				
			default:
				break;
			}
			
			UI.SetNumPellets( numPellets );
		}
	}
	
	IEnumerator GeneratePellets() {
		while( true ) {
			numPellets += numPellets < maxPellets? 1 : 0;
			UI.SetNumPellets( numPellets );
			yield return new WaitForSeconds( 0.2f );
		}
	}
	
	void Spawn<T>( Vector3 position ) where T : BadGuyShip {
		GameObject g = (GameObject)Instantiate( badGuyPrefab ) as GameObject;
		Vector3 viewport = Camera.main.ScreenToViewportPoint( position );
		
		BadGuyShip b = g.AddComponent<T>();
		b.Initialise( new Vector3( left + viewport.x * unitWidth, bottom + viewport.y * unitHeight ) );
	}
}
