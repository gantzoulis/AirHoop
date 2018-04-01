using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelConsumption : MonoBehaviour
{
    private Image fuelImage;
    private float fullFuel;
    [SerializeField]
    private GameObject playerPlane;
    [SerializeField]
    private Aircraft_motor playerMotor;
    [SerializeField]
    private bool playerFound = false;

	// Use this for initialization
	void Start ()
    {
        //GetPlayerObject();
        Debug.Log(playerFound);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(playerFound);
        if (!playerFound)
        {
            GetPlayerPlaneObject();
        }

        if (!GameManager.Instance.gameOver)
        {
            CalculateFuelBar();
        }

        
	}

    private void CalculateFuelBar()
    {
        float currentFuel = playerPlane.GetComponent<Aircraft_motor>().aircraft.fuel;
        float fuelBarFill = currentFuel / fullFuel;
        //Debug.Log("fuel " + currentFuel  + " of "+ fullFuel + "(" + fuelBarFill +")");
        fuelImage.GetComponent<Image>().fillAmount = fuelBarFill;
    }

    private void GetPlayerPlaneObject()
    {
        if (playerPlane = GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.Log("Player found " + playerPlane.name);
            if (playerMotor = playerPlane.GetComponent<Aircraft_motor>())
            {
                Debug.Log("Player Motor found " + playerMotor.name);
                fuelImage = GetComponent<Image>();
                fullFuel = playerMotor.aircraft.fuel;
                playerFound = true;
            }
        }
        //playerPlane = GameObject.FindGameObjectWithTag("Player");
        //playerMotor = playerPlane.GetComponent<Aircraft_motor>();
    }
}
