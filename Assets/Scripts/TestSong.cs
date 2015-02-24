using System.Collections.Generic;
using UnityEngine;

public class TestSong : MonoBehaviour
{
	string path;
	private AudioSource audioSource;
	public bool useUpdate = true;
	// Use this for initialization
	void Start ()
	{
		path = @"Songs/compose/BGM1";
		var clip = Resources.Load (path) as AudioClip;
		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.loop = true;
		audioSource.Play();
	}

	private long playTime;
	void Update(){
		if (!useUpdate)
		{
			return;
		}

		playTime += (long)(Time.deltaTime * 10000000L);

		for(var i = 0 ; i < 30;i++){
			if (playTime >100 ) playTime -= 100;
		}

		if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		//for (var i = 0; i < 10; i++) {
			if (audioSource.isPlaying) Debug.Log("xxxxxxxxxxxxxxxx");
		//}
	}
}
