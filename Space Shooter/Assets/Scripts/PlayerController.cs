using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;

    private Rigidbody rb;
    private AudioSource aSource;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach (var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            aSource.Play();
        }

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moviment = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = moviment * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

    }
}
