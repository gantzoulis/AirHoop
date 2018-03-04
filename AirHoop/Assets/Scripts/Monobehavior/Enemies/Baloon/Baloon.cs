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


	[SerializeField]
	private float distMoving;
	[SerializeField]
	private float speed;

	void Start()
	{
		curPos = this.gameObject.transform.position;

		randMove = Random.Range(0,2);

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

	}

	void Update()
	{
		MoveBaloon();
	}

	private void MoveBaloon()
	{
		transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}
}
