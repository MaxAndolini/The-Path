using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("DontDestroy").Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);

        SceneManager.LoadScene(1);
    }
}