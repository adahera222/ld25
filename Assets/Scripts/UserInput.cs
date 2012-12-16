using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {
	private static UserInput ins;
	public GameObject badGuyPrefab;
	public ParticleSystem spawnParticle;
	
	public static float unitWidth { private set; get; }
	public static float unitHeight { private set; get; }
	public static float left { private set; get; }
	public static float bottom { private set; get; }
	
	public GameObject
		barrierPrefab,
		bursterPrefab,
		bomberPrefab,
		spinnerPrefab;
	
	private EnemyTypes selection = EnemyTypes.BURSTER;
	
	private int numPellets = 0;
	private int maxPellets = 40;
	
	public static void SetSelection( EnemyTypes type ) {
		ins.Select( type );
	}
	
	public static void AddPellets( int num ) {
		ins.numPellets = Mathf.Min( ins.maxPellets, ins.numPellets+num );
	}
	
	public static void PlaySpawnParticle( Vector3 position ) {
		ins.spawnParticle.transform.position = position;
		ins.spawnParticle.Play( true );
	}
	
	void Awake() {
		ins = this;
	}
	
	void Start() {
		UI.SetSelection( selection );
		
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
			Select( EnemyTypes.BARRIER );
		} else if( Input.GetKeyDown( KeyCode.Alpha2 ) ) {
			Select( EnemyTypes.BURSTER );
		} else if( Input.GetKeyDown( KeyCode.Alpha3 ) ) {
			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha4 ) ) {
			Select( EnemyTypes.SPINNER );
		} else if( Input.GetKeyDown( KeyCode.Alpha5 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha6 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha7 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha8 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha9 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha0 ) ) {
//			Select( EnemyTypes.BOMBER );
		}
		
		if( Input.GetMouseButtonDown( 0 ) ) {
//			Debug.Log( "input: "+Input.mousePosition+"\n"+(Input.mousePosition.x/Screen.width)+" "+(Input.mousePosition.y/Screen.height) );
			
			UI.MouseInput( Input.mousePosition );
			
			if( Input.mousePosition.x / Screen.width > 0.65f ) return;
			if( Input.mousePosition.y / Screen.height < 0.25f ) return;
			
			switch( selection ) {
			case EnemyTypes.BARRIER:
				if( numPellets < 5 ) break;
				numPellets -= 5;
				Spawn( barrierPrefab, Input.mousePosition, true );
				break;
				
			case EnemyTypes.BOMBER:
				if( numPellets < 10 ) break;
				numPellets -= 10;
				Spawn( bomberPrefab, Input.mousePosition );
				break;
				
			case EnemyTypes.BURSTER:
				if( numPellets < 5 ) break;
				numPellets -= 5;
				Spawn( bursterPrefab, Input.mousePosition, true );
				break;
				
			case EnemyTypes.SPINNER:
				if( numPellets < 15 ) break;
				numPellets -= 15;
				Spawn( spinnerPrefab, Input.mousePosition, true );
				break;
				
			default:
				break;
			}
			
			UI.SetNumPellets( numPellets );
		}
	}
	
	void Select( EnemyTypes type ) {
		selection = type;
		UI.SetSelection( selection );
	}
	
	IEnumerator GeneratePellets() {
		while( true ) {
			numPellets += numPellets < maxPellets? 1 : 0;
			UI.SetNumPellets( numPellets );
			yield return new WaitForSeconds( 0.2f );
		}
	}
	
	void Spawn( GameObject prefab, Vector3 position, bool particles = false ) {
		GameObject g = (GameObject)Instantiate( prefab ) as GameObject;
		Vector3 viewport = Camera.main.ScreenToViewportPoint( position );
		Vector3 location = new Vector3( left + viewport.x * unitWidth, bottom + viewport.y * unitHeight );
		
		BadGuyShip b = g.GetComponent<BadGuyShip>();
		b.Initialise( location );
		
		if( particles ) {
			spawnParticle.transform.position = location;
			spawnParticle.Play( true );
		}
	}
	
	void Spawn<T>( Vector3 position, bool particles = false ) where T : BadGuyShip {
		GameObject g = (GameObject)Instantiate( badGuyPrefab ) as GameObject;
		Vector3 viewport = Camera.main.ScreenToViewportPoint( position );
		Vector3 location = new Vector3( left + viewport.x * unitWidth, bottom + viewport.y * unitHeight );
		
		BadGuyShip b = g.AddComponent<T>();
		b.Initialise( location );
		
		if( particles ) {
			spawnParticle.transform.position = location;
			spawnParticle.Play( true );
		}
	}
}
