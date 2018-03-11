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

	// Use this for initialization
	void Start ()
    {
        playerPlane = GameObject.FindGameObjectWithTag("Player");
        playerMotor = playerPlane.GetComponent<Aircraft_motor>();
        fuelImage = GetComponent<Image>();
        fullFuel = playerMotor.aircraft.fuel;
    }
	
	// Update is called once per frame
	void Update ()
    {
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
}
