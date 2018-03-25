using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLvItem : MonoBehaviour 
{
	[SerializeField]
	private int pointScore;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == GameManager.Instance.playerColliderName)
		{
			GameManager.Instance.playerScore += pointScore;
			this.gameObject.SetActive(false);
		}
	}
}
