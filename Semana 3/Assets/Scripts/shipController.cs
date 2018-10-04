using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{


    private LineRenderer laser;
    int shootableMask;                     // A layer mask so the raycast only hits things on the shootable layer.
    private float range = 1000f;                      // The distance the gun can fire.
    private float movement = 1.5f;
    private float timer = 0f;
    private float timeBetweenShoots = 0.1f;
    private float shootEffect = 0.05f;

    private AudioSource laserSound;

    private Light resplandor;


    // Use this for initialization
    void Start()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        laser = GetComponent<LineRenderer>();
        laser.enabled = false;

        laserSound = GetComponent<AudioSource>();

        resplandor = GetComponentInChildren<Light>();

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        float vMov = Input.GetAxisRaw("Vertical");
        if (vMov != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y + (vMov * movement * Time.deltaTime), transform.position.z);
        if (Input.GetButton("Fire1") && timer >= timeBetweenShoots)
        {

            Shoot();
        }

        if (timer >= shootEffect && laser.enabled == true)
        {

            DisableShoot();
        }
    }

    void Shoot()
    {

        laser.SetPosition(0, transform.position);

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootableMask);
        if (hit.collider != null)
        {
            laser.SetPosition(1, hit.point);
            EnemyController enemyHit = hit.collider.GetComponent<EnemyController>();
            enemyHit.destroy();
        }
        else
        {
            laser.SetPosition(1, transform.position + Vector3.forward * range);
        }

        laser.enabled = true;
        resplandor.enabled = true;
        laserSound.Play();
        timer = 0f;
    }

    void DisableShoot()
    {
        timer = 0f;
        laser.enabled = false;
        resplandor.enabled = false;
    }
}






