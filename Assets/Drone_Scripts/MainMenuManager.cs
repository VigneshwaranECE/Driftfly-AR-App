using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Method to load the AR_DRONE scene
    public void StartTraining()
    {
        SceneManager.LoadScene("AR_DRONE"); // Make sure the scene name matches exactly
    }

    // Method to quit the application
    public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the editor
#endif
    }
}
