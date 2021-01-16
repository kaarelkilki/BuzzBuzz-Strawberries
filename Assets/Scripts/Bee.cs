using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bee : MonoBehaviour
{
    // Movement speed
    [SerializeField] float speed = 0;
    // Flap force
    [SerializeField] float force = 500;

    public AudioSource collectStrawberry;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public Canvas canvasPlay;
    public Canvas canvasMenu;
    public Image imgUp;

    // Score count
    public int count;
    public int addCount;
    public int highScore;
    void Start()
    {
        collectStrawberry.Stop();
        LoadPlayer();
        MenuCanvas();
        SetScoreText();
    }

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
            count += addCount;
            SetScoreText();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        SavePlayer();
        count = 0;
        SceneManager.LoadScene("Game");
    }

    void Flap()
    {
        // Flap
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
#endif

#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
            }
        }
#endif
    }

    void AndroidQuit()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SavePlayer();
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
        addCount = 1;
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
        force = 400;
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