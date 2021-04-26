using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for shack menu canvas
public class ShackMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Mute()
    {
        if(AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            Debug.Log("AudioListener Volume not 0 or 1");
        }
    }
}
