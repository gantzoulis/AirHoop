using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    public bool specialHoop = false;
    public bool passedHoop = false;
   
    [SerializeField]
    private float angleSoFar;
    private float angleLastFrame;
    private int loopPassScore;
    [SerializeField]
    private bool loopPassedHoop = false;

    void Update()
    {
        CheckIfLoopBonus();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == DataManager.Instance.playerColliderName)
        {
            passedHoop = true;
        }
    }

    private void CheckIfLoopBonus()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            CountTurn();
        }
        else
        {
            StartCountTurn();
        }
    }

    private void StartCountTurn()
    {
        angleSoFar = 0f;
        angleLastFrame = DataManager.Instance.playerObject.transform.eulerAngles.z;
        angleLastFrame = (angleLastFrame > 180) ? 360 - angleLastFrame : angleLastFrame;
    }

    private void CountTurn()
    {
        float angle = DataManager.Instance.playerObject.transform.eulerAngles.z;
        angle = (angle > 180) ? 360 - angle : angle;

        angleSoFar += Mathf.Abs(angle - angleLastFrame);
        angleLastFrame = angle;

        if (angleSoFar > 180 && angleSoFar < 361 && passedHoop && !loopPassedHoop)
        {
           Debug.Log("GRATZ For The Loop & Ring");
           AudioSource loopSound = GameObject.Find("LoopSound").GetComponent<AudioSource>();
           loopSound.Play();

           DataManager.Instance.playerScore += loopPassScore;
           angleSoFar = 0f;
           loopPassedHoop = true;
        }
    }

    public void ResetHoop()
    {
        passedHoop = false;
    }
}
