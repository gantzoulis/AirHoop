using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneHorizontalMove : MonoBehaviour
{
    [SerializeField]
    private float airplaneSpeed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * airplaneSpeed);
    }
}
