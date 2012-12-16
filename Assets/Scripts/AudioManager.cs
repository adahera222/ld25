using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	private static AudioManager ins;
	
	private AudioSource[] channels;
	private int channel = 0;
	
	public AudioClip explosion;
	public AudioClip warp;
	public AudioSource shotloop;
		
	void Awake() {
		ins = this;
		
		channels = new AudioSource[16];
		for( int a = 0 ; a < channels.Length ; a++ ) {
			channels[a] = new GameObject( "Channel "+a ).AddComponent<AudioSource>();
			channels[a].playOnAwake = false;
			channels[a].transform.parent = transform;
			channels[a].transform.localPosition = Vector3.zero;
		}
	}
	
	void _Play( AudioClip clip ) {
		if( ++channel >= channels.Length ) {
			channel = 0;
		}
		
		channels[channel].clip = clip;
		channels[channel].Play();
	}
	
	public static void ToggleShotLoop( bool tf ) {
		ins.shotloop.enabled = tf;
	}
	
	public static void Play( AudioClip clip ) {
		ins._Play( clip );
	}
	
	public static void Explosion() {
		ins._Play( ins.explosion );
	}
	
	public static void Warp() {
		ins._Play( ins.warp );
	}
}
