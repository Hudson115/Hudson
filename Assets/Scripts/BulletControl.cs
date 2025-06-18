using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletControl : MonoBehaviour
{
    [SerializeField] GameObject Bullet;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ScoreControl.totalScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            // Implement behavior for bullet collision, e.g., ignore or bounce
            // For example, we can simply ignore the collision:
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
