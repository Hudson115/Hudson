using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ScoreControl.totalScore = 0;
            SceneManager.LoadScene("Scene1");
        }
    }

}
