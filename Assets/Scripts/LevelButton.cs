using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text levelName;

    [SerializeField]
    private TMPro.TMP_Text bestTime;

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void SetData(string name)
    {
        levelName.text = name;
        bestTime.text = "Best Time\n" + (PlayerPrefs.HasKey(name) ? PlayerPrefs.GetFloat(name).ToString() : "N/A");
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName.text);
    }
}
