using UnityEngine;
using TMPro;

public class Bee : MonoBehaviour
{
    // Movement speed
    [SerializeField] float speed = 0;
    // Flap force
    [SerializeField] float force = 300;

    //public AudioSource collectStrawberry;
    //public GameObject strawberry;    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Canvas canvasPlay;
    public Canvas canvasMenu;

    // Score count
    private int count;
    private int highScore;
    
    void Start()
    {
        MenuCanvas();
        count = 0;

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
            //collectStrawberry.Play();
            other.gameObject.SetActive(false);
            count += 1;
            SetScoreText();
            //TotalScore();
            
            //use courutine and timer to set gameObject to true again
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(count);
        MenuCanvas();
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
        count = 0;
        canvasMenu.enabled = false;
        canvasPlay.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        speed = 3;
        force = 300;

        // Fly towards the right
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        
        
    }
}