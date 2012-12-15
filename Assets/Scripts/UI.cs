using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI ins;
	
	public TextMesh score;
	public TextMesh selection;
	public TextMesh shield;
	
	public UIPelletGroup[] groups;
	
	void Awake() {
		ins = this;
	}
	
	public static void SetScore( int i ) {
		ins.score.text = "Score: "+i.ToString();
	}
	
	public static void SetSelection( string s ) {
		ins.selection.text = "Current Type: "+s;
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
}
