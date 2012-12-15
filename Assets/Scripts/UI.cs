using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI ins;
	
	public TextMesh score;
	public TextMesh selection;
	public TextMesh shield;
	
	void Awake() {
		ins = this;
	}
	
	public static void SetScore( int i ) {
		ins.score.text = "Score: "+i.ToString();
	}
	
	public static void SetSelection( string s ) {
		ins.selection.text = "Current Type: "+s;
	}
	
	public static void SetShield( int pct ) {
		ins.shield.text = pct.ToString() + "%";
		ins.shield.renderer.material.color = new Color( 1f-((float)pct)/100f, ((float)pct)/100f, 0f, 1f );
	}
}
