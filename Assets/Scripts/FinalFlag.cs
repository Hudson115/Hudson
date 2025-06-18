using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreControl.totalScore = 0;
            Application.Quit();
        }
    }

}
