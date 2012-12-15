using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI ins;
	
	public GUIText score;
	public GUIText selection;
	
	void Awake() {
		ins = this;
	}
	
	public static void SetScore( int i ) {
		ins.score.text = "Score: "+i.ToString();
	}
	
	public static void SetSelection( string s ) {
		ins.selection.text = "Current Type: "+s;
	}
}
