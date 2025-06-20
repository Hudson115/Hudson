using UnityEngine;

public class TurretControl : MonoBehaviour
{
    Transform _Player;
    float dist;
    public float maxDistance;
    public Transform head, barrel;
    public GameObject _projectile;
    public float bulletSpeed = 1500;
    public float FireRate, nextFire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        dist = Vector3.Distance(_Player.position, transform.position);
        if(dist <= maxDistance)
        {
            // Aim at the player's center (adjusting the Y position)
            Vector3 aimPoint = new Vector3(_Player.position.x, _Player.position.y + (_Player.GetComponent<Collider>().bounds.size.y / 2), _Player.position.z);
            head.LookAt(aimPoint);
            
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f/FireRate;
                shoot();
            }
        }
    }

    void shoot()
    {
        GameObject clone = Instantiate(_projectile, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * bulletSpeed);
        Destroy(clone, 10f);
    }
}
