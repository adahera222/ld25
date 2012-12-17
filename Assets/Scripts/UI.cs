using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI ins;
	
	public Camera uiCamera;
	public TextMesh lives;
	public TextMesh score;
	public TextMesh selection;
	public TextMesh shield;
	public Renderer insertCoin;
	public GameObject restart, title;
	
	public UIPelletGroup[] groups;
	
	private UITile[] tiles;
	
	void Awake() {
		ins = this;
		tiles = GetComponentsInChildren<UITile>( false );
		insertCoin.enabled = false;
		
		restart.AddComponent<TitleButton>().action = () => {
			Application.LoadLevel( Application.loadedLevel );
		};
		
		title.AddComponent<TitleButton>().action = () => {
			Application.LoadLevel( 0 );
		};
		
		restart.SetActive( false );
		title.SetActive( false );
	}
	
	public static void GameOver() {
		ins.restart.SetActive( true );
		ins.title.SetActive( true );
		ins.StartCoroutine( ins._GameOver() );
	}
	
	IEnumerator _GameOver() {
		while( true ) {
			insertCoin.enabled = !insertCoin.enabled;
			yield return new WaitForSeconds( 1f );
		}
	}
	
	public static void MouseInput( Vector2 mousePos ) {
		Ray r = ins.uiCamera.ScreenPointToRay( mousePos );
		RaycastHit info;
		if( Physics.Raycast( r, out info, float.PositiveInfinity, 1 << 30 ) ) {
			info.collider.SendMessage( "Select" );
		}
	}
	
	public static void SetScore( int i ) {
		ins.score.text = "Score: "+i.ToString();
	}
	
	public static void SetSelection( EnemyTypes type ) {
		ins.selection.text = type.ToString();
		foreach( UITile tile in ins.tiles ) {
			tile.SetActive( type );
		}
	}
	
	public static void SetShield( int hp, int maxhp ) {
		float pct = (float)hp / (float)maxhp;
		ins.shield.text = ((int)100f*pct).ToString( "0.0" ) + "%";
		ins.shield.renderer.material.color = new Color( 1f-pct, pct, 0f, 1f );
	}
	
	public static void SetNumPellets( int num ) {
		for( int a = 0 ; a < ins.groups.Length ; a++ ) {
			ins.groups[a].SetNumActivePellets( Mathf.Min( 10, num ) );
			num -= 10;
		}
	}
	
	public static void SetNumLives( int i ) {
		ins.lives.text = "Lives:     x"+i.ToString();
	}
}
