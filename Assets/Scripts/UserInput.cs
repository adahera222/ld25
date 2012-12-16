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
		minePrefab,
		spinnerPrefab,
		trackerPrefab;
	
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
		StartCoroutine( IncreaseMaxPellets() );
		
		Debug.Log( left+" "+bottom+"\n"+unitWidth+" "+unitHeight );
	}
	
	void OnMouseUpAsButton() {
		print ( "omg "+Input.GetMouseButtonUp(0) );
		
		if( Input.GetMouseButtonUp( 0 ) ) {
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
				
			case EnemyTypes.TRACKER:
				if( numPellets < 20 ) break;
				numPellets -= 20;
				Spawn( trackerPrefab, Input.mousePosition, true );
				break;
				
			case EnemyTypes.MINE:
				if( numPellets < 10 ) break;
				numPellets -= 10;
				Spawn( minePrefab, Input.mousePosition, true );
				break;
				
				
			default:
				break;
			}
			
			UI.SetNumPellets( numPellets );
		}
	}
	
	void Update() {
		if( Input.GetMouseButtonDown( 0 ) ) {
			UI.MouseInput( Input.mousePosition );
		}
		
		if( Input.GetKeyDown( KeyCode.Alpha1 ) ) {
			Select( EnemyTypes.BARRIER );
		} else if( Input.GetKeyDown( KeyCode.Alpha2 ) ) {
			Select( EnemyTypes.BURSTER );
		} else if( Input.GetKeyDown( KeyCode.Alpha3 ) ) {
			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha4 ) ) {
			Select( EnemyTypes.SPINNER );
		} else if( Input.GetKeyDown( KeyCode.Alpha5 ) ) {
			Select( EnemyTypes.TRACKER );
		} else if( Input.GetKeyDown( KeyCode.Alpha6 ) ) {
			Select( EnemyTypes.MINE );
		} else if( Input.GetKeyDown( KeyCode.Alpha7 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha8 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha9 ) ) {
//			Select( EnemyTypes.BOMBER );
		} else if( Input.GetKeyDown( KeyCode.Alpha0 ) ) {
//			Select( EnemyTypes.BOMBER );
		}
	}
	
	IEnumerator IncreaseMaxPellets() {
		while( maxPellets < 150 ) {
			yield return new WaitForSeconds( 30f );
			maxPellets += 10;
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
}
