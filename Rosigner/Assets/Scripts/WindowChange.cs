
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowChange : MonoBehaviour
{
    public string sceneName;
    public void StartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
