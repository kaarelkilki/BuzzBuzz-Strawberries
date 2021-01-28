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
    public TMP_Text _highScoreText;
    public Canvas canvasPlay;
    public Canvas canvasMenu;
    public Canvas canvasPreScreenshot;
    public Canvas canvasScreenshot;
    public Image imgUp;

    // Score count
    public int count;
    public int addCount;
    public int highScore;
    public int shareHighScore;
    public float preShareTime = 3.0f;
    public bool preShareTimeRunning = false;
    public float shareTime = 4.0f;
    public bool shareTimeRunning = false;

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
        PreShareTimer();
        ShareTimer();
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

    void SetScoreText()
    {
        scoreText.text = count.ToString() + " <sprite=7>";
        if (count >= highScore)
        {
            highScore = count;
            highScoreText.text = count.ToString() + " <sprite=7>";
            _highScoreText.text = count.ToString() + " <sprite=7>";
        }
        else if (count < highScore)
        {
            highScoreText.text = highScore.ToString() + " <sprite=7>";
            _highScoreText.text = highScore.ToString() + " <sprite=7>";
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
        canvasScreenshot.enabled = false;
        canvasPreScreenshot.enabled = false;
        transform.position = new Vector3(-13, 1, 0);

        SetScoreText();
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

    public void Exit()
    {
        SavePlayer();
        Application.Quit();
    }

    public void PlayGame()
    {
        canvasMenu.enabled = false;
        canvasPlay.enabled = true;
        canvasScreenshot.enabled = false;
        canvasPreScreenshot.enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        speed = 3;
        force = 400;
        // Fly towards the right
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    void PreShareTimer()
    {
        if (preShareTimeRunning == true)
        {
            preShareTime -= Time.deltaTime;
            if (preShareTime > 0)
            {
                canvasMenu.enabled = false;
                canvasPlay.enabled = false;
                canvasScreenshot.enabled = false;
                canvasPreScreenshot.enabled = true;
            }
            else if (preShareTime < 0)
            {
                shareTimeRunning = true;
                ShareTimer();
                preShareTimeRunning = false;
                preShareTime = 3.0f;
            }
        }
    }

    void ShareTimer()
    {
        if (shareTimeRunning == true)
        {
            canvasMenu.enabled = false;
            canvasPlay.enabled = false;
            canvasScreenshot.enabled = true;
            canvasPreScreenshot.enabled = false;
            shareTime -= Time.deltaTime;
            if (shareTime < 0)
            {
                MenuCanvas();
                shareTimeRunning = false;
                shareTime = 4.0f;
            }
        }   
    }

    public void PreShare()
    {
        preShareTimeRunning = true;
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