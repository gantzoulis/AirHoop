using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLv : MonoBehaviour 
{
    private List<GameObject> hoopsList;
    private bool fullBonusAchieved = false;
    [SerializeField] private int bonusAwardScore;
    private bool bonusAwarded = false;

    private float counting;

    void Start()
    {
        CreateHoopList();
    }

    void Update()
    {
       // DeactivateSpecialLv();
        CheckIfFullBonusAchieved();
        AssignBonus();
    }

    private void DeactivateSpecialLv()
    {
        counting = GameObject.Find("SpawnManager").GetComponent<AirSpawnManager>().coundDownSpecialTime;

        if (counting <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void CreateHoopList()
    {
        hoopsList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.tag == "Hoop")
            {
                hoopsList.Add(child.gameObject);
            }
        }
    }

    private void CheckIfFullBonusAchieved()
    {
        foreach (GameObject hoop in hoopsList)
        {
            if (hoop.GetComponentInChildren<Hoop>().passedHoop == false)
            {
                fullBonusAchieved = false;
                return;
            }
            else
            {
                fullBonusAchieved = true;
            }
        }
    }

    private void AssignBonus()
    {
        if (fullBonusAchieved && !bonusAwarded)
        {
            DataManager.Instance.playerScore += bonusAwardScore;
            Debug.Log("Full Bonus Achieved");
            bonusAwarded = true;
        }
    }
}
