using UnityEngine;
using System.Collections;
using System.Collections.Generic;

delegate void Action();

public class TitleScreen : MonoBehaviour {
	public GameObject play, instructions, fullInstructions;
	
	void Start() {
		play.AddComponent<TitleButton>().action = OnPlay;
		instructions.AddComponent<TitleButton>().action = OnHelp;
	}
	
	void OnPlay() {
		Application.LoadLevel( "game" );
	}
	
	void OnHelp() {
		fullInstructions.renderer.enabled = !fullInstructions.renderer.enabled;
	}
}

class TitleButton : MonoBehaviour {
	public Action action;
	
	void OnMouseEnter() {
		renderer.material.color = Color.cyan;
	}
	
	void OnMouseExit() {
		renderer.material.color = Color.white;
	}
	
	void OnMouseDown() {
		renderer.material.color = Color.blue;
	}
	
	void OnMouseUpAsButton() {
		renderer.material.color = Color.cyan;
		action();
	}
}
