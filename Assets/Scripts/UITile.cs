using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITile : MonoBehaviour {
	public EnemyTypes enemyType;
	
	private Renderer tile;
	private static Color off = new Color( 0.75f, 0.75f, 0.75f, 0.75f );
	private static Color on  = new Color( 0f, 1f, 1f, 0.75f );
	
	void Awake() {
		tile = transform.Find( "Image-Tile" ).renderer;
		BoxCollider bc = gameObject.AddComponent<BoxCollider>();
		bc.size = new Vector3( 3f, 3f, 1f );
	}
	
	void Select() {
		UserInput.SetSelection( enemyType );
	}
	
	public void SetActive( EnemyTypes type ) {
		tile.material.color = type == enemyType? on : off;
	}
}
