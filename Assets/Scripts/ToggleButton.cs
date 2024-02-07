using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Button button; // Reference to the Button component
    public Sprite onSprite; // Sprite to show when button is "on"
    public Sprite offSprite; // Sprite to show when button is "off"

    private Image buttonImage; // Reference to the Image component of the button
    private bool isOn = true; // Flag to track the state of the button

    private void Start()
    {
        buttonImage = button.GetComponent<Image>();
        UpdateButtonImage();
    }

    public void OnButtonClick()
    {
        Debug.Log("button clcik");
        isOn = !isOn; // Toggle the state
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        buttonImage.sprite = isOn ? onSprite : offSprite;
    }
}
