using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    // Method to load the MainMenu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ensure the scene name matches exactly
    }
}
