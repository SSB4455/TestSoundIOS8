/*
 * 
 * SSB4455 2014.09.14
 * 
 */
using System.Collections.Generic;
using UnityEngine;

public class TestSongPlayer : MonoBehaviour
{
	public string BmsPath { get; set; }
	public AudioSource audioSource;
	//List<AudioSource> audioSourceList = new List<AudioSource>();
	//List<AudioSource> audioSourcePlayingList = new List<AudioSource>();
	Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
	List<MusicNote> musicNotes = new List<MusicNote>();
	public float BPM { get; private set; }
	public float BeatTime { get; private set; }
	public long BeatTimeL { get; private set; }
	long playTime;
	public long PlayTicks { get { return playTime; } }
	int playI = 0;

	
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log("Stopwatch.Frequency = " + System.Diagnostics.Stopwatch.Frequency);

		/*for (int i = 0; i < 20; i++)
		{
			//audioSourceList.Add((Instantiate(soundPlayerPrefab) as GameObject).GetComponent<AudioSource>());
		}*/


		BmsPath = @"Songs/lalala/LALALA";
		//BmsPath = @"Songs/compose/bms";

		string bathPath = BmsPath.Substring(0, BmsPath.LastIndexOf("/") + 1);
		string[] musicScoreLines = ((TextAsset)Resources.Load(BmsPath)).text.Split(new char[1] { '\n' });
		for (int i = 0; i < musicScoreLines.Length; i++)
		{
			string aLine = musicScoreLines[i];
			string[] lineSplite = aLine.Split(new char[1] { '\t' });

			// check to see if it's a usef
			if (lineSplite.Length < 2)
			{
				continue;
			}

			string lineHead = lineSplite[0];
			if (lineHead.Equals("BPM"))
			{
				this.BPM = float.Parse(lineSplite[1]);
				BeatTime = 60 / BPM;
				BeatTimeL = (long)(BeatTime * 10000000L);

				Debug.Log("BeatTime = " + BeatTime);

			} else if (lineHead.Equals("WAV")) {
				string wavId = lineSplite[1];
				string wavPath = bathPath + lineSplite[2];
				//Debug.Log("wavId\t" + wavId + "\twavPath\t" + wavPath);

				AudioClip audioClip = Resources.Load(wavPath) as AudioClip;
				audioClips.Add(wavId, audioClip);

			} else if (lineHead.Equals("MusicNote")) {
				long insertTime = long.Parse(lineSplite[1]);
				string wavId = lineSplite[3];
				AddMusicNote(insertTime, wavId);

			} else if (lineHead.Equals("Note")) {
				long insertTime = long.Parse(lineSplite[1]);
				string wavId = lineSplite[3];
				AddMusicNote(insertTime, wavId);

			} else if (lineHead.Equals("LongNote")) {
				long insertTime = long.Parse(lineSplite[1]);
				string wavId = lineSplite[3];
				AddMusicNote(insertTime, wavId);

			} else if (lineHead.Equals("LongNoteSub")) {
				long insertTime = long.Parse(lineSplite[1]);
				string wavId = lineSplite[3];
				AddMusicNote(insertTime, wavId);
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		playTime += (long)(Time.deltaTime * 10000000L);
		while (playI < musicNotes.Count && musicNotes[playI].InsertTime <= PlayTicks)
		{
			PlayWAVById(musicNotes[playI].WAVId);
			
			playI++;
		}

		/*for (int i = 0; i < audioSourcePlayingList.Count; i++)
		{
			if (!audioSourcePlayingList[i].isPlaying)
			{
				audioSourceList.Add(audioSourcePlayingList[i]);
				audioSourcePlayingList.RemoveAt(i--);
			}
		}*/

		// exit
		if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void AddMusicNote(long insertTime, string wavId)
	{
		MusicNote musicNote = new MusicNote(insertTime, wavId);
		if (audioClips.ContainsKey(wavId))
		{
			musicNotes.Add(musicNote);
		}
	}

	public bool PlayWAVById(string wavId)
	{
		/*if (audioSourceList.Count < 1)
		{
			return false;
		}

		AudioSource audioSource = audioSourceList[0];
		audioSource.clip = audioClips[wavId];
		audioSource.Play();
		//audioSource.PlayOneShot(audioClips[wavId]);

		audioSourceList.RemoveAt(0);
		audioSourcePlayingList.Add(audioSource);
		return true;
		*/
		
		audioSource.PlayOneShot(audioClips[wavId]);
		return true;
	}

	void OnApplicationPause()
	{

		Debug.Log("OnApplicationPause");
	}

	void OnApplicationFocus()
	{
		Debug.Log("OnApplicationFocus");
		
		/*if (!pause)
		{
			Debug.Log("play pause");
			pause = true;
			playwatch.Stop();
		} else {
			pause = false;
			playwatch.Start();
		}*/
	}

	void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit");
	}







































	struct MusicNote
	{
		long insertTime;
		internal long InsertTime { get { return insertTime; } }
		string wavId;
		internal string WAVId { get { return wavId; } }



		public MusicNote(long insertTime, string wavId)
		{
			this.insertTime = insertTime;
			this.wavId = wavId;
		}
	}
}
