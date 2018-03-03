using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelConsumption : MonoBehaviour
{
    private Image fuelImage;
    private float fullFuel;
    [SerializeField]
    private GameObject[] playerPlane;
    [SerializeField]
    private Aircraft_motor playerMotor;

	// Use this for initialization
	void Start ()
    {
        playerPlane = GameObject.FindGameObjectsWithTag("Player");
        playerMotor = playerPlane[1].GetComponent<Aircraft_motor>();
        fuelImage = GetComponent<Image>();
        fullFuel = playerMotor.aircraft.fuel;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CalculateFuelBar();
	}

    private void CalculateFuelBar()
    {
        float currentFuel = playerPlane[1].GetComponent<Aircraft_motor>().aircraft.fuel;
        float fuelBarFill = 1 / currentFuel;
        Debug.Log("fuel " + fuelBarFill);
        fuelImage.GetComponent<Image>().fillAmount = fuelBarFill;
    }
}
