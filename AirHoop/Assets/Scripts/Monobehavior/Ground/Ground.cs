﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour 
{

	[SerializeField] float destroyTime = 40.0f;

	void Start()
	{
		StartCoroutine(DestroyGround());
	}

	IEnumerator DestroyGround()
	{
		yield return new WaitForSeconds(destroyTime);
        if (!DataManager.Instance.gameOver)
        {
            gameObject.SetActive(false);
        }
	}
}
