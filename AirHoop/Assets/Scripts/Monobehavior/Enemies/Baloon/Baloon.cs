using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour 
{
	private Vector3 curPos;
	private Vector3 pos1;
	private Vector3 pos2;

	private int randMove;
	private Vector3 upMove;
	private Vector3 downMove;

	private float distMoving;
	[SerializeField]
	private float minDistMoving;
	[SerializeField]
	private float maxDistMoving;
	private float speed;
	[SerializeField]
	private float maxSpeed;
	[SerializeField]
	private float minSpeed;

	[SerializeField] private float destroyTime = 20.0f;

	void Start()
	{
		curPos = this.gameObject.transform.position;

		randMove = Random.Range(0,2);
		speed = Random.Range(minDistMoving,maxDistMoving);
		distMoving = Random.Range(minDistMoving, maxDistMoving);

		switch(randMove)
		{
		case 0:
			pos1 = new Vector3(curPos.x, curPos.y + distMoving, curPos.z);
			pos2 = new Vector3(curPos.x, curPos.y - distMoving, curPos.z);
			break;
		case 1:
			pos1 = new Vector3(curPos.x, curPos.y - distMoving, curPos.z);
			pos2 = new Vector3(curPos.x, curPos.y + distMoving, curPos.z);
			break;
		}

		StartCoroutine(DestroyBaloon());
	}

	void Update()
	{
		MoveBaloon();
	}

	private void MoveBaloon()
	{
		transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}

	IEnumerator DestroyBaloon()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
