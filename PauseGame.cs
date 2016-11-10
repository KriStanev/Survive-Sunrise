using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{

    public Canvas pauseMenu;
    bool paused = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused == true)
            {
                Time.timeScale = 0;
                pauseMenu.gameObject.SetActive(true);
                print("game paused");

            }
            else if (paused == false)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                print("game unpaused");
            }
        }
    }
}
