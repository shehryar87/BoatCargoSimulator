using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class InGameAds : MonoBehaviour
{

    public Image[] buttonImage;
    public Sprite[] images;
    public string[] links; 
    private int currentImageIndex = 0;
    

    private void Start()
    {
       for(int i =0; i< buttonImage.Length; i++)
        {
            buttonImage[i].sprite = images[currentImageIndex];
        }
       

        StartCoroutine(SwapImg(5.0f));
    }


    IEnumerator SwapImg(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentImageIndex = (currentImageIndex + 1) % images.Length;


        for (int i = 0; i < buttonImage.Length; i++)
        {
            buttonImage[i].sprite = images[currentImageIndex];
        }
        StartCoroutine(SwapImg(5.0f));

    }
   /* private void Update()
    {
       
        timeSinceLastSwap += Time.deltaTime;

        
        if (timeSinceLastSwap >= swapInterval)
        {
          
           

           
            timeSinceLastSwap = 0f;
        }
    }
*/
    public void OpenLink()
    {
        // Check if the current image index is within the range of links array
        if (currentImageIndex >= 0 && currentImageIndex < links.Length)
        {
            // Open the corresponding link in a web browser
            Application.OpenURL(links[currentImageIndex]);
        }
    }





   
}
