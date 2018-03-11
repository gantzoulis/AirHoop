using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneHorizontalMove : MonoBehaviour
{
    [SerializeField]
    private float airplaneSpeed = 5.0f;

	[SerializeField] private float destroyTime = 20.0f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(DestroyPlanes());
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * airplaneSpeed);
    }

	IEnumerator DestroyPlanes()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
