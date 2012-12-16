using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorUtils : Editor {
	[MenuItem( "Tools/asdf" )]
	public static void Hack() {
		int a = 0;
		foreach( Transform t in Selection.transforms ) {
			t.position -= Vector3.up*0.5f;
		}
	}
	
	[MenuItem( "Tools/Nudge Up" )]
	public static void nudgeup() {
		foreach( Transform t in Selection.transforms ) {
			t.position += Vector3.up*0.5f;
		}
	}
	
	[MenuItem( "Tools/Nudge Down" )]
	public static void nudgedown() {
		foreach( Transform t in Selection.transforms ) {
			t.position -= Vector3.up*0.5f;
		}
	}
}
