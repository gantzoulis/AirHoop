using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{

    
    public GameObject playButton;
    
    public GameObject difficultyMenu;
    private bool difficultyMenuOn;

    public AudioClip UI_AirplaneEffect;

    public GameObject useridPanel;
    private bool userIDAnimation = false;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && difficultyMenuOn)
        {
            PlayMainMenuAnimationReverse();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Debug.Log("* is Pressed");
            if (userIDAnimation == false)
            {
                PlayUserIdAnimation();
                userIDAnimation = !userIDAnimation;
            }
            else
            {
                PlayUserIdAnimationReverse();
                userIDAnimation = !userIDAnimation;
            }

        }
	}

    public void PlayMainMenuAnimation()
    {
        playButton.GetComponent<Animator>().SetTrigger("PlayAnimation");
        difficultyMenu.GetComponent<Animator>().SetTrigger("PlayAnimation");
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(UI_AirplaneEffect);
        difficultyMenuOn = true;
    }

    public void PlayMainMenuAnimationReverse()
    {
        playButton.GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        difficultyMenu.GetComponent<Animator>().SetTrigger("PlayAnimationReverse");
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(UI_AirplaneEffect);
        difficultyMenuOn = false;
    }

    public void PlayUserIdAnimation()
    {
        useridPanel.GetComponent<Animator>().SetTrigger("PlayAnim");
    }

    public void PlayUserIdAnimationReverse()
    {
        useridPanel.GetComponent<Animator>().SetTrigger("PlayAnimReverse");
    }

}
