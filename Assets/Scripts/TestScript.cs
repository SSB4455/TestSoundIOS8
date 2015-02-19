/*
 * 
 * SSB4455 2015.01.19
 * 
 * �������ɻ��Ĵ��루����������ɵĻ���
 */

using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public Transform louti;
	public GameObject cube;
	private float r = 500;
	private float angle = 0;            //�Ƕ� 


	void Start()
	{
		for (int i = 0; i < 36; i++)
		{
			float hudu = angle * Mathf.PI / 180;        //�󻡶�  
			float dx = r * Mathf.Cos(hudu);
			float dy = r * Mathf.Sin(hudu);
			Transform center = louti.transform;

			GameObject cube1 = (GameObject)GameObject.Instantiate(cube);
			cube1.transform.position = new Vector3(center.position.x + dx, center.position.y + dy, center.position.z);
			cube1.transform.LookAt(center);     //�ı䳯�� ��ת����  
			angle += 10;
		}
	}  

	void Update ()
	{
		
	}

}
