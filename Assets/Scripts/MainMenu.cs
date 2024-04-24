using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private LevelButton levelButtonPrefab;

    [SerializeField]
    private Transform levelButtonsParent;

    // Start is called before the first frame update
    void Start()
    {
        var levels = GetAllLevels();
        foreach(var level in levels)
        {
            var levelButton = Instantiate(levelButtonPrefab, levelButtonsParent);
            levelButton.SetData(level);
        }
    }

    private List<string> GetAllLevels()
    {
        List<string> levels = new List<string>();
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {

            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string[] parts = path.Split('/');
            string sceneName = parts[parts.Length - 1];
            sceneName = sceneName.Replace(".unity", "");
            if (sceneName.StartsWith("Level"))
                levels.Add(sceneName);

        }
        return levels;

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
