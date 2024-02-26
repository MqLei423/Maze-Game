using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverReset : MonoBehaviour
{
    public string sceneToLoad = "MazeGeneration";

    // Call this method to reset the scene
    public void ResetScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
