using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public bool FirstPerson;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstPerson == true)
        {
            transform.localPosition = new Vector3(-0.02f, 3.2f, 0.81f);
        }

        else
        {
            transform.localPosition = new Vector3(-0.21f, 4.24f, -3.7f);
        }
    }
}
