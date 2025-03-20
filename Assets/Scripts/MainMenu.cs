using UnityEngine; 
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene("Sandbox"); 
    }

    public void QuitGame() {
        Debug.Log("Quit game!"); 
        Application.Quit(); 
    }
}