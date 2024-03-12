using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public GameObject ring;
    public GameObject zone2;
    private bool ball_check1 = true;
    public UIController controller;
    public PLayerMovement pLayerMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (gameObject.tag == "zoneController1")
        {
            ring.GetComponent<MeshCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Debug.Log(gameObject.tag);
            ball_check1 = true;
            Debug.Log(ball_check1);
            StartCoroutine(ResetCollider());
            
            
        }

        else
        {
            zone2.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(PlusOchki());
        }
    }
 
    
    IEnumerator ResetCollider()
    {

        yield return new WaitForSeconds(2);
        ring.GetComponent<MeshCollider>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        zone2.GetComponent<BoxCollider>().enabled = true;
        //ball_check1 = false;
        yield return null;
    }
    IEnumerator PlusOchki ()
    {
        Debug.Log(ball_check1);
        /* while (!ball_check1)
         {
             yield return null;
             Debug.Log("Wait");
         }  */
        if (ball_check1 == true)
        {
            controller.serializing.playerData.score++;
            if(controller.serializing.playerData.score > controller.serializing.playerData.bestScore)
            {
                controller.serializing.playerData.bestScore = controller.serializing.playerData.score;
                controller.UpdateBestScoreUI();
            }
            controller.UpdateScoreUI();
            if (controller.serializing.worldData.isPlayerMoving)
            {
                pLayerMovement.StartCoroutine("MovePlayerToPos");
            }
            
            Debug.Log(controller.score);
            //ball_check1 = false;
        }
        Debug.Log("Wait");
        yield return null;
    }
}
