
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowChange : MonoBehaviour
{
    public string sceneName;
    // This method is used to change the scene one from another
    public void StartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
