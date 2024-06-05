using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoadeer : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
