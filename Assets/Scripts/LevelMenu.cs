using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    public void OpenLevel(int levelID)
    {
        string levelName = "Scene" + levelID;
        SceneManager.LoadScene(levelName);
    }

}
