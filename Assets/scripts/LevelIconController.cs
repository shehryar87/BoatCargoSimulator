using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelIconController : MonoBehaviour {
	
	public int level;
	public Sprite lockImage;
    public Sprite unLockImage;
    public GameObject glowImage;
	private int currentLevelNumber;
	
	void Start () {
		currentLevelNumber = PlayerPrefs.GetInt ("shipLevelCompleted");
        Debug.Log(level);
        Debug.Log(currentLevelNumber);

        if (level < currentLevelNumber)
        {
            gameObject.GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Button>().image.sprite = unLockImage;
        }
        else
        {
            
            gameObject.GetComponent<Button>().enabled = false;
            gameObject.GetComponent<Button>().image.sprite = lockImage;
        }

        if( level == currentLevelNumber)
        {
            gameObject.GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Button>().image.sprite = unLockImage;
            var glow = Instantiate(glowImage,transform.parent);
            glow.transform.position = transform.position;
            glow.transform.SetAsFirstSibling();
        }

        if(currentLevelNumber == 0 && level == 1)
        {
            gameObject.GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Button>().image.sprite = unLockImage;
            gameObject.GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Button>().image.sprite = unLockImage;
            var glow = Instantiate(glowImage, transform.parent);
            glow.transform.position = transform.position;
            glow.transform.SetAsFirstSibling();
        }
    }
	
	
}
