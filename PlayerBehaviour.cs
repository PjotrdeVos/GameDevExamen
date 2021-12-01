using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip cameraNoise;

    [Header("Page System Settings")]
    public List<GameObject> pages = new List<GameObject>();
    public int collectedPages;

    [Header("UI Settings")]
    public GameObject inGameMenuUI;
    public GameObject pickUpUI;
    public GameObject finishedGameUI;
    public GameObject pagesCount;
    public bool paused;

	void Update ()
    {
        
        pagesCount.GetComponent<Text>().text = "Collected Pages: " + collectedPages + "/8";

        if (Input.GetKey(KeyCode.LeftShift))
            this.gameObject.GetComponent<Animation>().CrossFade("Run", 1);
        else
            this.gameObject.GetComponent<Animation>().CrossFade("Idle", 1);

        // Wanneer je alles pages hebt show end screen
        if (collectedPages >= 8)
        {
            Debug.Log("you finished");
            Cursor.visible = true;

            this.gameObject.GetComponent<FirstPersonController>().enabled = false;
            inGameMenuUI.SetActive(false);
            finishedGameUI.SetActive(true);      

            Button playAgainBtn = finishedGameUI.gameObject.transform.Find("PlayAgainBtn").GetComponent<Button>();
            playAgainBtn.onClick.AddListener(this.gameObject.GetComponent<MenuInGame>().PlayAgain);

            Button quitBtn = finishedGameUI.gameObject.transform.Find("QuitBtn").GetComponent<Button>();
            quitBtn.onClick.AddListener(this.gameObject.GetComponent<MenuInGame>().QuitGame);
        } 
    }

    // page systeem
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.transform.tag == "Page")
        {
            Debug.Log("You Found a Page: " + collider.gameObject.name + ", Press E to pick up");
            pickUpUI.SetActive(true);      
        }
    }

    // page systeem
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.transform.tag == "Page")
        {       
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("You get this page: " + collider.gameObject.name);

                pickUpUI.SetActive(false);

                pages.Add(collider.gameObject);
                collectedPages ++;

                collider.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        // disable UI
        if (collider.gameObject.transform.tag == "Page")
            pickUpUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<GameManager>();

    }
}
