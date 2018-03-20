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
        currentAPselection++;
        airplanePrefabs[currentAPselection - 1].GetComponent<Animator>().SetTrigger("PlayAnimation");
        airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimation");
    }

    public void ShowPrevPlane()
    {
        currentAPselection--;
        airplanePrefabs[currentAPselection + 1].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        airplanePrefabs[currentAPselection].GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
    }
}
