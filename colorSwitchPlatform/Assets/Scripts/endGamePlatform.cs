using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endGamePlatform : MonoBehaviour
{
   
    public playerMovement playerMovement;

  
    public GameObject endGamePanel;


    public Transform playerStartPosition;

    private void Start()
    {
        if (endGamePanel != null)
        {
            
            endGamePanel.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerEndGame(collision.gameObject);
        }
    }

    private void TriggerEndGame(GameObject player)
    {
       
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

      
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }

       
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }


    public void PlayAgain()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // move player back to the starting point
        if (player != null && playerStartPosition != null)
        {
            player.transform.position = playerStartPosition.position;

          
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }

       
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
        
    }
}
