using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Bee : MonoBehaviour
{
    // Movement speed
    public float speed = 2;
    // Flap force
    public float force = 300;

    public AudioSource collectStrawberry;
    public GameObject strawberry;    
    public Text countText;
    public TextMeshProUGUI scoreText;

    // Score count
    private int count;

    // Use this for initialization
    void Start()
    {
        // Fly towards the right
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        count = 0;
        SetScoreText();
        collectStrawberry.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Flap
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            collectStrawberry.Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetScoreText();
            
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Restart
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("AfterGameMenu");
    }

    void SetScoreText ()
    {
        scoreText.text = "SCORE: " + count.ToString() + " <sprite=7>";

    }
}