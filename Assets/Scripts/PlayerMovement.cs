using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    float horizontalMovement;
    bool faceRight = true;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float smoothValue = 0.1f;
    [SerializeField] float jumpForce = 7f;

    [SerializeField] int lives = 3;
    [SerializeField] Transform respawnPoint;
    [SerializeField] LifeUI lifeUI;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource gameOverSound;

    public int score = 0;

    bool isGrounded = false;
    bool isRespawning = false;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        lifeUI.UpdateLives(lives);
    }

    private void Update()
    {
        if (isRespawning) return;

        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);

            if (jumpSound != null && jumpSound.clip != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip);
            }
        }

        if (horizontalMovement > 0f && !faceRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0f && faceRight)
        {
            Flip();
        }

        myAnimator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    private void Flip()
    {
        faceRight = !faceRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1f;
        transform.localScale = myScale;
    }

    private void FixedUpdate()
    {
        if (isRespawning) return;

        Vector2 nextVelocity = new Vector2(
            horizontalMovement * movementSpeed,
            myRigidbody.velocity.y
        );

        myRigidbody.velocity = Vector2.Lerp(
            myRigidbody.velocity,
            nextVelocity,
            smoothValue * Time.deltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && !isRespawning)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        isRespawning = true;

        if (deathSound != null && deathSound.clip != null)
        {
            deathSound.PlayOneShot(deathSound.clip);
        }

        LoseLife();

        if (lives <= 0)
        {
            yield break;
        }

        myRigidbody.velocity = Vector2.zero;
        myRigidbody.simulated = false;

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }

        yield return new WaitForSeconds(0.7f);

        transform.position = respawnPoint.position;

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = true;
        }

        myRigidbody.simulated = true;
        isRespawning = false;
    }

    public void LoseLife()
    {
        lives--;
        lifeUI.UpdateLives(lives);

        Debug.Log("Nyawa tersisa: " + lives);

        if (lives <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public int GetLives()
    {
        return lives;
    }

    IEnumerator GameOver()
    {
        if (gameOverSound != null && gameOverSound.clip != null)
        {
            gameOverSound.PlayOneShot(gameOverSound.clip);
        }

        gameOverPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}