/*
 * 
 * SSB4455 2015.02.17
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public abstract class IMoveController : MonoBehaviour
{
	public GameObject moveEntityPrefab;
	List<MoveEntity> moveEntityPrepareList = new List<MoveEntity>();
	List<MoveEntity> moveEntityList = new List<MoveEntity>();



	public virtual void Update()
	{
		for (int i = 0; i < moveEntityList.Count; i++)
		{
			moveEntityList[i].Update(Time.deltaTime);
			if (moveEntityList[i].Over)
			{
				moveEntityList[i].Recycle();
				moveEntityList.RemoveAt(i--);
			}
		}
	}

	protected MoveEntity AddMoveEntity()
	{
		MoveEntity moveEntity = GetMoveEntity();
		moveEntityList.Add(moveEntity);
		return moveEntity;
	}

	protected MoveEntity GetMoveEntity()
	{
		MoveEntity moveEntity = null;
		if (moveEntityPrepareList.Count > 0)
		{
			int i = Random.Range(0, moveEntityPrepareList.Count);
			moveEntity = moveEntityPrepareList[i];
			moveEntityPrepareList.RemoveAt(i);
		} else {
			moveEntity = CreateMoveEntity();
		}

		return moveEntity;
	}

	protected MoveEntity CreateMoveEntity()
	{
		return new MoveEntity((Instantiate(moveEntityPrefab) as GameObject).transform, this);
	}

	protected void AddPrepareMoveEntity(int count)
	{
		for (int i = 0; i < count; i++)
		{
			CreateMoveEntity().Recycle();
		}
	}

	void Recycle(MoveEntity moveEntity)
	{
		moveEntityPrepareList.Add(moveEntity);
	}


































	
	
	
	
	
	public class MoveEntity
	{
		IMoveController controller;
		Transform trans;

		//Vector3 startPosition;
		Vector3 endPosition;
		Vector3 speed;
		float time;
		public bool Over { get; private set; }



		internal MoveEntity(Transform trans, IMoveController controller)
		{
			this.trans = trans;
			this.controller = controller;
		}

		internal void Start(Vector3 startPosition, Vector3 speed, float time)
		{
			trans.position = startPosition;
			this.speed = speed;
			this.time = time;
			Over = false;
			trans.gameObject.SetActive(true);
		}

		internal void Start(string name, Vector3 startPosition, Vector3 speed, float time)
		{
			trans.name = name;
			Start(startPosition, speed, time);
		}

		internal void Update(float deltaTime)
		{
			time -= deltaTime;
			if (time < 0)
			{
				Over = true;
			}

			trans.Translate(speed * deltaTime);
		}

		internal void Recycle()
		{
			trans.gameObject.SetActive(false);
			controller.Recycle(this);
		}
	}
}
