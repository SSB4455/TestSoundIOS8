/*
 * 
 * SSB4455 2015.02.17
 * 
 */
using UnityEngine;

public class BeatControllerScript : IMoveController
{
	public TestSongPlayer songPlayer;
	float beatShift;
	float length = 10000;
	int beatIndex;



	void Start()
	{
		//AddPrepareMoveEntity(5);
	}

	public override void Update ()
	{
		base.Update();

		beatShift += Time.deltaTime;
		if (beatShift > songPlayer.BeatTime)
		{
			beatShift = 0;// (songPlayer.PlayTicks % songPlayer.BeatTimeL) / 10000000f;
			float time = songPlayer.BeatTime * 8;
			AddMoveEntity().Start("beatTunnel" + beatIndex++, new Vector3(0, length, 0), new Vector3(0, -length / time, 0), time);
		}
		
	}

}
