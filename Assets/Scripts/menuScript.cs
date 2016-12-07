using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    public Button playText;
    public Button exitText;

	void Start () {
        playText = playText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

	}

    public void startGame()
    {
        SceneManager.LoadScene(1);
        
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
