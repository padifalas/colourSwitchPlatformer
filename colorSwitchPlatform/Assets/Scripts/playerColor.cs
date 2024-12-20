using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerColor : MonoBehaviour
{
    public Material playerMaterial; 
    private string[] colors = { "Red", "Blue", "Yellow" };
    private int currentIndex = 0; 
    private string currentColor;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.ChangeColor.performed += _ => CycleColor(); 
    }

    void OnDisable()
    {
        controls.Player.ChangeColor.performed -= _ => CycleColor();
        controls.Player.Disable();
    }

    void Start()
    {
        ChangeColor(0); // Set initial color 
    }

    void CycleColor()
    {
        currentIndex = (currentIndex + 1) % colors.Length; 
        ChangeColor(currentIndex);
    }

    void ChangeColor(int index)
    {
        currentColor = colors[index];

        switch (currentColor)
        {
            case "Red":
                playerMaterial.color = Color.red;
                break;
            case "Blue":
                playerMaterial.color = Color.blue;
                break;
            case "Yellow":
                playerMaterial.color = Color.yellow;
                break;
        }

        Debug.Log($"Player color changed to: {currentColor}");
    }

    public string GetCurrentColor()
    {
        return currentColor;
    }
}