using UnityEngine;

public class ShowFPS : MonoBehaviour {
	/// <summary>
	/// 每次刷新计算的时间      帧/秒
	/// </summary>
	public float updateInterval = 0.4f;
	public int targetFrameRate = 60;
	/// <summary>
	/// 最后间隔结束时间
	/// </summary>
	private double lastInterval;
	private int frames = 0;
	private int frames2 = 0;
	private float currFPS;

	float minFPS;
	float maxDeltaTime;

	private GUIStyle fpsStyle;



	
	// Use this for initialization
	void Start ()
	{
		//修改当前的FPS
		Application.targetFrameRate = targetFrameRate;
		
		lastInterval = Time.realtimeSinceStartup;
		frames = 0;
		minFPS = 61;

		fpsStyle = new GUIStyle();
		//fpsStyle.normal.background = null;
		//fpsStyle.normal.textColor = new Color(0, 1, 1);
		fpsStyle.fontSize = 40;
	}
	
	// Update is called once per frame
	void Update ()
	{
		++frames;
		if (frames2 < 100)
		{
			frames2++;
			minFPS = 60;
			maxDeltaTime = 0;
		}

		float timeNow = Time.realtimeSinceStartup;
		if (timeNow > lastInterval + updateInterval)
		{
			currFPS = (float)(frames / (timeNow - lastInterval));
			frames = 0;
			lastInterval = timeNow;
		}
		if (minFPS > currFPS)
		{
			minFPS = currFPS;
		}
		if (maxDeltaTime < Time.deltaTime)
		{
			maxDeltaTime = Time.deltaTime;
		}
	}
	
	private void OnGUI ()
	{
		GUILayout.Label("FPS:" + minFPS.ToString("F2") + "\nMaxDeltaTime:" + maxDeltaTime, fpsStyle);
	}

}
