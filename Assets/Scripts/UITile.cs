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
		tile.gameObject.AddComponent<UITileClickListener>().SetType( enemyType );
	}
	
	public void SetActive( EnemyTypes type ) {
		tile.material.color = type == enemyType? on : off;
	}
}

	
class UITileClickListener : MonoBehaviour {
	private EnemyTypes enemyType;
	public void SetType( EnemyTypes type ) {
		enemyType = type;
	}
	
	void OnMouseUpAsButton() {
		UI.SetSelection( enemyType );
	}
}
