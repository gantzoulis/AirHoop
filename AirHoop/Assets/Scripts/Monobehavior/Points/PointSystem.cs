using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    [SerializeField]
    private List<PointSystemClass> enemyScorePoints = new List<PointSystemClass>();
    
    // Use this for initialization
	void Start ()
    {
        AssignPointScores();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void AssignPointScores()
    {
        foreach (var item in enemyScorePoints)
        {
            item.pointTrigger.gameObject.GetComponent<PointTrigger>().pointScore = item.pointScore;
        }
    }
}
