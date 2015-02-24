using UnityEngine;
using System.Collections;

public class audio : MonoBehaviour {


    public AudioSource music;	
    public float musicVolume;	
    
    void Start() {
   		 musicVolume = 0.5F;	
    }
	void OnGUI() {
		
		if (GUI.Button(new Rect(10, 10, 100, 50), "Play music"))  {
			
			if (!music.isPlaying){
				
				music.Play();
			}
			
		}
		
		if (GUI.Button(new Rect(10, 60, 100, 50), "Stop music"))  {
			if (music.isPlaying){
				music.Stop();
			}
		}
		
		if (GUI.Button(new Rect(10, 110, 100, 50), "Pause music"))  {
			if (music.isPlaying){
				music.Pause();
			}
		}

		musicVolume = GUI.HorizontalSlider (new Rect(160, 10, 100, 50), musicVolume, 0.0F, 1.0F);
	
		GUI.Label(new Rect(160, 50, 300, 20), "Music Volueme is " + (int)(musicVolume * 100) + "%");
		
		if (music.isPlaying){
			music.volume = musicVolume;
		}
	}
}
