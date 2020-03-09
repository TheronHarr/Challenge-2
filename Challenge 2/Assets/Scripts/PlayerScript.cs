using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    public Text score;

    public Text lives;

    private int livesValue = 3;

    private int scoreValue = 0;

    private Rigidbody2D rd2d;

    public float speed;

    public Text winText;

    public Animator anim;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    private bool facingRight = true;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winText.text = "";
        anim = GetComponent<Animator>();
        print (transform.position);
        (transform.position) = new Vector3(-20, 0, 0);
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W) && isOnGround == true)
       // {
           // anim.SetBool("isJumping", true);
        //}
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (transform.position.y <= -30)
        {
            livesValue = 0;
            SetLivesText();
         }


    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        anim.SetFloat("Speed", Mathf.Abs(hozMovement));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }
        
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetLivesText();
        }
       if (collision.collider.tag == "Ground")
       {
            anim.SetBool("IsJumping", false);
       }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            if (Input.GetKey(KeyCode.W))
            {
               rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
               anim.SetBool("IsJumping", true);
            }
        }
    }

    private void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Theron Harrison.";
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(210, 1, 0);
        }
    }

    private void SetLivesText()
    {
        
        lives.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            lives.text = "You lose! Press Esc to exit.";
            Destroy(rd2d);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            if (transform.position.x == 210)
            {
                livesValue = 3;
                SetLivesText();
            }
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void LateUpdate()
    {
        
        
    }
}
