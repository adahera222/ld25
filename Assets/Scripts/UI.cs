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
	
	public UIPelletGroup[] groups;
	
	private UITile[] tiles;
	
	void Awake() {
		ins = this;
		tiles = GetComponentsInChildren<UITile>( false );
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
		ins.selection.text = "Current Type: "+type.ToString();
		foreach( UITile tile in ins.tiles ) {
			tile.SetActive( type );
		}
	}
	
	public static void SetShield( int hp, int maxhp ) {
		float pct = (float)hp / (float)maxhp;
		ins.shield.text = ((int)100f*pct).ToString() + "%";
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
