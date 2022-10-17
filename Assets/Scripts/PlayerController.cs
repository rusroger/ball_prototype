using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public GameObject focalPoint;
    public GameObject powerupIndicator;

    public int lives = 3;
    public bool hasPowerup = false;
    private float powerupStrenght = 15f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tienes " + lives + " vidas");
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(transform.position.y < -5)
        {
            if(lives != 0)
            {
                transform.position = new Vector3(0, 0, 0);
                lives--;
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("YOU LOSE!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFormPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Colisión con: " + collision.gameObject.name + "con PowerUp = " + hasPowerup);

            enemyRb.AddForce(awayFormPlayer * powerupStrenght, ForceMode.Impulse);
        }
    }
}
