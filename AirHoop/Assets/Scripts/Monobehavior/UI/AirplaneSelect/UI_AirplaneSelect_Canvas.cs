using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_AirplaneSelect_Canvas : MonoBehaviour
{
    [SerializeField]
    private Transform airplaneSpawnPos;
    [SerializeField]
    private PlayerAirplaneSelection[] airplanePrefabs;
    [SerializeField]
    private int currentAPselection;
    [SerializeField]
    private int nextAPselection;
    [SerializeField]
    private int prevAPselection;

    
    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;
    [SerializeField]
    private GameObject planeSelect;
    [SerializeField]
    private GameObject planeSelectLocked;
    [SerializeField]
    private GameObject costPaneText;
    [SerializeField]
    private GameObject costPane;

    private List<GameObject> airplanePool = new List<GameObject>();
    private List<GameObject> airplanePoolSelection = new List<GameObject>();


    // Use this for initialization
    void Start ()
    {
        PopulatePlaneList();
        currentAPselection = 0;
        foreach (GameObject go in airplanePool)
        {
            GameObject spawnedPlane = Instantiate(go,airplaneSpawnPos.position, Quaternion.Euler(0, -160, 0));
            airplanePoolSelection.Add(spawnedPlane);
        }

        airplanePoolSelection[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");

        /*
        DataManager.Instance.airplaneList[currentAPselection].aircraft.planeSelectModel[0].
            GetComponent<Animator>().SetTrigger("PlayAnimation");
        */
        //TBD
            //GameManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[currentAPselection].aircraft;

        DataManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[currentAPselection].aircraft;

        if (DataManager.Instance.airplaneList[currentAPselection].playerOwned)
        {
            planeSelect.SetActive(true);
            planeSelectLocked.SetActive(false);
            costPane.SetActive(false);
        }
        else
        {
            planeSelect.SetActive(false);
            planeSelectLocked.SetActive(true);
            costPane.SetActive(true);
            costPaneText.GetComponent<Text>().text = DataManager.Instance.airplaneList[currentAPselection].airPlaneCost.ToString();
        }
        nextAPselection = currentAPselection + 1;
        prevAPselection = currentAPselection;
        leftButton.GetComponent<Button>().interactable = false;
	}
	
	void Update ()
    {
        CheckArrowButtons();
    }

    public void ShowNextPlane()
    {
        nextAPselection = currentAPselection + 1;
        airplanePoolSelection[nextAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
        if (DataManager.Instance.airplaneList[nextAPselection].playerOwned)
        {
            planeSelect.SetActive(true);
            planeSelectLocked.SetActive(false);
            costPane.SetActive(false);
        }
        else
        {
            planeSelect.SetActive(false);
            planeSelectLocked.SetActive(true);
            costPane.SetActive(true);
            costPaneText.GetComponent<Text>().text = DataManager.Instance.airplaneList[nextAPselection].airPlaneCost.ToString();
        }
        airplanePoolSelection[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        //TBD
            //GameManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[nextAPselection].aircraft;
        DataManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[nextAPselection].aircraft;
        currentAPselection = nextAPselection;
    }

    public void ShowPrevPlane()
    {
        prevAPselection = currentAPselection - 1;
        airplanePoolSelection[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        airplanePoolSelection[prevAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
        if (DataManager.Instance.airplaneList[prevAPselection].playerOwned)
        {
            planeSelect.SetActive(true);
            planeSelectLocked.SetActive(false);
            costPane.SetActive(false);
        }
        else
        {
            planeSelect.SetActive(false);
            planeSelectLocked.SetActive(true);
            costPane.SetActive(true);
            costPaneText.GetComponent<Text>().text = DataManager.Instance.airplaneList[prevAPselection].airPlaneCost.ToString();
        }
        
        //TBD
            //GameManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[prevAPselection].aircraft;

        DataManager.Instance.choosenAircraft = DataManager.Instance.airplaneList[prevAPselection].aircraft;
        currentAPselection = prevAPselection;
    }

    private void CheckArrowButtons()
    {
        if (currentAPselection == DataManager.Instance.airplaneList.Count -1)
        {
            rightButton.GetComponent<Button>().interactable = false;
            currentAPselection = DataManager.Instance.airplaneList.Count - 1;
        }

        if (currentAPselection == 0)
        {
            leftButton.GetComponent<Button>().interactable = false;
        }

        if (currentAPselection >0 && currentAPselection < DataManager.Instance.airplaneList.Count - 1)
        {
            leftButton.GetComponent<Button>().interactable = true;
            rightButton.GetComponent<Button>().interactable = true;
        }
    }

    public void StartLevel()
    {
        SceneMainManager.Instance.LoadGameScene("Main");
    }

   public void PopulatePlaneList()
    {
        
        foreach (PlayerAirplaneSelection aircraftSelection in DataManager.Instance.airplaneList)
        {
            airplanePool.Add(aircraftSelection.aircraft.planeSelectModel[0]);
        }
        
        
    }
}
