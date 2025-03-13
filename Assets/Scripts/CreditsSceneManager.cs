using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneManager : MonoBehaviour
{

    private float delay = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadMainMenuAfterDelay(delay));
    }

    IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
