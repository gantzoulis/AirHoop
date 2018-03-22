using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AirplaneSelect_Canvas : MonoBehaviour
{

    [SerializeField]
    private GameObject[] airplanePrefabs;
    private int currentAPselection = 0;

    [SerializeField]
    private AnimationClip movePlaneLeft;
    [SerializeField]
    private AnimationClip movePlaneRight;

    // Use this for initialization
    void Start ()
    {
		
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
        }
        else
        {
            airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            if (currentAPselection < airplanePrefabs.Length - 1)
            {
                currentAPselection++;
            }
            
        }
        
        //airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        
    }

    public void ShowPrevPlane()
    {
        if (currentAPselection == airplanePrefabs.Length)
        {
            airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimation");
            currentAPselection--;
        }
        else
        {
            airplanePrefabs[currentAPselection ].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
            airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimation");
            if (currentAPselection > 0)
            {
                currentAPselection--;
            }
        }
        
    }
}
