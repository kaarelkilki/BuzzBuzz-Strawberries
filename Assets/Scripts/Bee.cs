using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Bee : MonoBehaviour
{
    // Movement speed
    [SerializeField] float speed = 0;
    // Flap force
    [SerializeField] float force = 300;

    public AudioSource collectStrawberry;
    //public GameObject strawberry;    
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public Canvas canvasPlay;
    public Canvas canvasMenu;

    // Score count
    public int count;
    private int highScore;
    void Start()
    {
        collectStrawberry.Stop();
        LoadPlayer();
        MenuCanvas();
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Flap();
        
        AndroidQuit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            collectStrawberry.Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetScoreText();
            //TotalScore();            
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        SavePlayer();
        Debug.Log(count);
        count = 0;
        SceneManager.LoadScene("Game");
    }

    void Flap()
    {
        // Flap
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
    }

    void AndroidQuit()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + " <sprite=7>";
        if (count >= highScore)
        {
            highScore = count;
            highScoreText.text = count.ToString() + " <sprite=7>";
            //SavePlayer();
        }
        else if (count < highScore)
        {
            highScoreText.text = highScore.ToString() + " <sprite=7>";
        }
    }

    void MenuCanvas()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        speed = 0;
        force = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        canvasMenu.enabled = true;
        canvasPlay.enabled = false;
        transform.position = new Vector3(-13, 1,0);

        SetScoreText();
    }

    public void PlayGame()
    {
        canvasMenu.enabled = false;
        canvasPlay.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        speed = 3;
        force = 300;

        // Fly towards the right
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    void SavePlayer()
    {
        ES3.Save("highScore", highScore);
    }

    void LoadPlayer()
    {
        if (ES3.KeyExists("highScore"))
            highScore = ES3.Load<int>("highScore");
    }
}