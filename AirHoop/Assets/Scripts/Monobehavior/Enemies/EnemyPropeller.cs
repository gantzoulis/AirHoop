using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPropeller : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.gameObject.transform.Rotate(5000 * Time.deltaTime,0,0);
    }
}
