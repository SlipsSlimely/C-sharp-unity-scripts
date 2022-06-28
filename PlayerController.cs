using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int coinCount = 0;
    public int PlayerHealth = 1;

    public Text CoinCounter;

    public float jumpForce;

    // Used for jumping for now, but may have other uses later
    private Rigidbody rb;
    private bool jumpRequest;
    private int jumpCount;

    private GameObject shield;
    public GameObject coin;
    private bool isInvincible;

    private AudioSource audioSource;
    public AudioClip coinNoise;
    public AudioSource powerupPlayer;
    public AudioClip jumpNoise;
    public AudioClip powerNoise;
    public float volume = .5f;
    //public PauseMenu script;
    //private Collider col;

    //public Animation jumpAnimation;

    // Get the canvas to access the pause script there.
    public GameObject canvas;
    private bool isPaused;

    // Variable to hold the Animor component
    private Animator anim;
    int jumpHash;
    int deathHash;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        jumpHash = Animator.StringToHash("Jump");
        deathHash = Animator.StringToHash("Death");

        isPaused = false;
        rb = this.GetComponent<Rigidbody>();
        jumpRequest = false;
        jumpCount = 0;

        shield = gameObject.transform.Find("Shield").gameObject;
        shield.SetActive(false);
        // May be used to pause and play footsteps when jumping
        audioSource = GetComponent<AudioSource>();

        coinCount = 0;
        UpdateScore();

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        isPaused = canvas.GetComponent<PauseMenu>().paused;

        // Jump
        if (!isPaused && Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || jumpCount <= 1) /**&& script.paused != true**/)
        {
            jumpRequest = true;
        }
        else if (!IsGrounded()) 
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    void FixedUpdate()
    {
        if (jumpRequest) 
        {
            jumpCount++;


            // handle the jump
            powerupPlayer.PlayOneShot(jumpNoise, .3f);
            anim.SetTrigger(jumpHash);
            rb.velocity = jumpForce * Vector3.up;
            jumpRequest = false;
        }
    }

    //this lets our skeleton collide with coins and traps
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            //audioSource.PlayOneShot(coinNoise, volume);
            powerupPlayer.PlayOneShot(coinNoise, 1f);
            coinCount = coinCount + 1;
            coin.SetActive(true);
            UpdateScore();
        }
        else if (other.tag == "Trap")
        {
            if (isInvincible) 
            {
                shield.SetActive(false);
                StartCoroutine(invincibility());
            }
            else 
            {
                PlayerHealth = PlayerHealth - 1;
                rb.velocity = Vector3.up * 5;
                anim.ResetTrigger(jumpHash);
                anim.SetTrigger(deathHash);
            }
        }
        else if (other.tag == "Health-up")
        {
            if (PlayerHealth == 1)
            {
                powerupPlayer.PlayOneShot(powerNoise, volume);
                isInvincible = true;
                other.gameObject.SetActive(false);
                shield.SetActive(true);
            }
        }
        else if (other.tag == "Money up")
        {
            audioSource.PlayOneShot(coinNoise, volume);
            audioSource.PlayOneShot(coinNoise, volume);
            coinCount = coinCount + 10;
        }
    }
    void UpdateScore()
    {
        coin.SetActive(false);
        CoinCounter.text = "Money Count: " + coinCount.ToString();
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 0.15f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance)) 
        {
            jumpCount = 0;
            return true;
        }

        else
            return false;
    }

    IEnumerator invincibility() 
    {
        yield return new WaitForSeconds(1);
        isInvincible = false;
    }
}