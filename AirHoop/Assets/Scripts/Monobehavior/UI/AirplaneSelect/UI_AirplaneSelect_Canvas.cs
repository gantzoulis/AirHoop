using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AirplaneSelect_Canvas : MonoBehaviour
{

    [SerializeField]
    private GameObject[] airplanePrefabs;
    private int currentAPselection = 0;

    [SerializeField]
    private AnimationClip movePlaneLeft;
    [SerializeField]
    private AnimationClip movePlaneRight;
    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;

    // Use this for initialization
    void Start ()
    {
        leftButton.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShowNextPlane()
    {
        if (currentAPselection == 0)
        {
            
            airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
            currentAPselection++;
            leftButton.GetComponent<Button>().interactable = false;

        }
        else
        {
            airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            if (currentAPselection < airplanePrefabs.Length - 1)
            {
                currentAPselection++;
            }
            else
            {
                rightButton.GetComponent<Button>().interactable = false;
            }
            
        }
        
        //airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        
    }

    public void ShowPrevPlane()
    {
        if (currentAPselection == airplanePrefabs.Length)
        {
            rightButton.GetComponent<Button>().interactable = false;
            leftButton.GetComponent<Button>().interactable = true;
            airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimation");
            currentAPselection--;
        }
        else
        {
            leftButton.GetComponent<Button>().interactable = true;
            airplanePrefabs[currentAPselection ].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimation");
            if (currentAPselection > 0)
            {
                currentAPselection--;
            }
        }
        
    }
}
