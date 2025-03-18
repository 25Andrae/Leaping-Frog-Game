using UnityEngine;
using UnityEngine.UI;

public class ImageSwapper : MonoBehaviour
{
    public Image image;
    public Sprite newImage;
    private Sprite originalImage;
    private float delay = 0.5f; 
    private bool isSwapping = false;

    void Start()
    {
        // Store the original image
        originalImage = image.sprite;
    }

    public void SwapImage()
    {
        // Change the image source to the new image
        image.sprite = newImage;

        // Start the timer for reverting back
        if (!isSwapping)
        {
            isSwapping = true;
            Invoke("RevertImage", delay);
        }
    }

    void RevertImage()
    {
        // Revert the image back to its original state
        image.sprite = originalImage;
        isSwapping = false;
    }

    // Method to change the delay value
    public void SetDelay(float newDelay)
    {
        delay = newDelay;
    }
}
