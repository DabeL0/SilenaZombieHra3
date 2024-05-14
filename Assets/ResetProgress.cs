using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        PlayerPrefs.SetInt("bodiky", 0);
        List<string> levels = new List<string>();
        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings; i++)
        {

            PlayerPrefs.SetFloat("Level" + i, 0);

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
