using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_AirplaneSelect_Canvas : MonoBehaviour
{

    [SerializeField]
    private PlayerAirplaneSelection[] airplanePrefabs;
    [SerializeField]
    private int currentAPselection;
    [SerializeField]
    private int nextAPselection;
    [SerializeField]
    private int prevAPselection;

    [SerializeField]
    private AnimationClip movePlaneLeft;
    [SerializeField]
    private AnimationClip movePlaneRight;
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


    // Use this for initialization
    void Start ()
    {
        currentAPselection = 0;
        airplanePrefabs[currentAPselection].AirPlanePrefab.GetComponent<Animator>().SetTrigger("PlayAnimation");
        GameManager.Instance.choosenAircraft = airplanePrefabs[currentAPselection].aircraft;
        if (airplanePrefabs[currentAPselection].playerOwned)
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
            costPaneText.GetComponent<Text>().text = airplanePrefabs[currentAPselection].airPlaneCost.ToString();
        }
        nextAPselection = currentAPselection + 1;
        prevAPselection = currentAPselection;
        leftButton.GetComponent<Button>().interactable = false;
        Debug.Log("Plane Array length " + airplanePrefabs.Length);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckArrowButtons();
    }

    public void ShowNextPlane()
    {
        nextAPselection = currentAPselection + 1;
        airplanePrefabs[nextAPselection].AirPlanePrefab.GetComponent<Animator>().SetTrigger("PlayAnimation");
        if (airplanePrefabs[nextAPselection].playerOwned)
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
            costPaneText.GetComponent<Text>().text = airplanePrefabs[nextAPselection].airPlaneCost.ToString();
        }
        airplanePrefabs[currentAPselection].AirPlanePrefab.GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        GameManager.Instance.choosenAircraft = airplanePrefabs[nextAPselection].aircraft;
        currentAPselection = nextAPselection;
    }

    public void ShowPrevPlane()
    {
        prevAPselection = currentAPselection - 1;
        airplanePrefabs[currentAPselection].AirPlanePrefab.GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        airplanePrefabs[prevAPselection].AirPlanePrefab.GetComponent<Animator>().SetTrigger("PlayAnimation");
        if (airplanePrefabs[prevAPselection].playerOwned)
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
            costPaneText.GetComponent<Text>().text = airplanePrefabs[prevAPselection].airPlaneCost.ToString();
        }
        GameManager.Instance.choosenAircraft = airplanePrefabs[prevAPselection].aircraft;
        currentAPselection = prevAPselection;
    }

    private void CheckArrowButtons()
    {
        if (currentAPselection == airplanePrefabs.Length -1)
        {
            rightButton.GetComponent<Button>().interactable = false;
            currentAPselection = airplanePrefabs.Length -1;
        }

        if (currentAPselection == 0)
        {
            leftButton.GetComponent<Button>().interactable = false;
        }

        if (currentAPselection >0 && currentAPselection < airplanePrefabs.Length -1)
        {
            leftButton.GetComponent<Button>().interactable = true;
            rightButton.GetComponent<Button>().interactable = true;
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
