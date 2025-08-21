using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyunu başlat
    public void PlayGame()
    {
        // Build Settings'te MainMenu 0. indexteyse, Level1 1. indextedir
        SceneManager.LoadScene("Level1");
    }

    // Oyundan çık
    public void QuitGame()
    {
        Debug.Log("Oyundan çıkış"); // Editör'de sadece log yazar
        Application.Quit();         // Build alınca gerçekten çıkar
    }
}
