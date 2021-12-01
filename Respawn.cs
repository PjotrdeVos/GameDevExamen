using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;

    private void Collision(Collider collider)    
    {
        if (collider.gameObject.tag == "Enemy")
        {
            PauseMenuUI.SetActive(true);
            
        }
    }


    // When you touch enemy PauseMenuUI pops up
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            PauseMenuUI.SetActive(true);
        }
    }
}
