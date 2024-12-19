using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPlatform : MonoBehaviour
{
    [Tooltip("The required color for the player to pass through this platform.")]
    public string requiredColor;

    private Collider platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
            playerColor playerColor = collision.gameObject.GetComponent<playerColor>();

            if (playerColor != null)
            {
                if (playerColor.GetCurrentColor() == requiredColor)
                {
                    AllowPass();
                }
                else
                {
                    EnsureSolidPlatform();
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            EnsureSolidPlatform();
        }
    }

    private void AllowPass()
    {
       
        if (platformCollider != null && platformCollider.enabled)
        {
            platformCollider.enabled = false;
            Debug.Log("Color matched! Platform collider disabled.");
        }
    }

    private void EnsureSolidPlatform()
    {
       
        if (platformCollider != null && !platformCollider.enabled)
        {
            platformCollider.enabled = true;
            Debug.Log("Color mismatched! Platform collider re-enabled.");
        }
    }
}