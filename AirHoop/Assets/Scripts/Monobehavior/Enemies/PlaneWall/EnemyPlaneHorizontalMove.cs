using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneHorizontalMove : MonoBehaviour
{
    [SerializeField]
    private float airplaneSpeed = 5.0f;

	private float destroyTime = 120.0f;
    [SerializeField] private float deactivateDistance = 100f;

    private GameObject player;


	void Start () 
	{
        //StartCoroutine(DestroyPlanes());
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * airplaneSpeed);
        DeactivateThroughDistance();
    }

	IEnumerator DestroyPlanes()
	{
		yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
	}

    private void DeactivateThroughDistance()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance > deactivateDistance)
        {
            this.gameObject.SetActive(false);
        }
    }
}
