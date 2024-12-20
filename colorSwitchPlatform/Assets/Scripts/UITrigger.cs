using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITrigger : MonoBehaviour
{
    public GameObject uiPanel; 
    public Image displayImage;

    private void Start()
    {
        uiPanel.SetActive(false);
    }

    [System.Serializable]
    public class TriggerData
    {
        public GameObject triggerObject;
        public Sprite image; 
    }

    public TriggerData[] triggers; 

    private void OnTriggerEnter(Collider other)
    {
        foreach (TriggerData trigger in triggers)
        {
            if (other.gameObject == trigger.triggerObject)
            {
                ActivateImage(trigger.image);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (TriggerData trigger in triggers)
        {
            if (other.gameObject == trigger.triggerObject)
            {
                DeactivateImage();
                return;
            }
        }
    }

    private void ActivateImage(Sprite image)
    {
        if (uiPanel != null && displayImage != null)
        {
            uiPanel.SetActive(true); 
            displayImage.sprite = image; 
        }
    }

    private void DeactivateImage()
    {
        if (uiPanel != null && displayImage != null)
        {
            uiPanel.SetActive(false);
            displayImage.sprite = null; 
        }
    }
}

   

  
