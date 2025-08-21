using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.ShaderGraph;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private Vector3 startPosition;
    public AudioSource jumpSound;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int point = 0;
    private bool level1Completed = false;
    private bool level2Completed = false;
    private bool level3Completed = false;
    private bool levelFailed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpSound.Play();
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (level1Completed && Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("Level2");
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (level2Completed && Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("Level3");
            }
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (level3Completed && Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                // Burada yeni level varsa aç, yoksa MainMenu’ye dön
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (levelFailed && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void FixedUpdate()
    {
        // Rigidbody’ye yatay hız ver (Unity 6+ için linearVelocity kullan)
        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, 0);
    }

    public TextMeshProUGUI messageText;
    public AudioSource winSound;
    public AudioSource BGMSound;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Column"))
        {
            // Oyuncunun hızını sıfırla
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Pozisyonu başa al
            transform.position = startPosition;

            // Tüm Food objelerini yeniden aktif et
            Food[] allFoods = UnityEngine.Object.FindObjectsByType<Food>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

            foreach (Food food in allFoods)
            {
                food.gameObject.SetActive(true);
            }

            point = 0;
        }

        else if (collision.gameObject.CompareTag("Finish"))
        {
            if (point == 100)
            {
                messageText.text = "Level Completed!\nPress Space to continue.";
                messageText.color = Color.green;

                if (SceneManager.GetActiveScene().name == "Level1")
                    level1Completed = true;
                if (SceneManager.GetActiveScene().name == "Level2")
                    level2Completed = true;
                if (SceneManager.GetActiveScene().name == "Level3")
                {
                    messageText.text = "Game Completed!\nreturn to menu";
                    level3Completed = true;
                }
            }
            else
            {
                messageText.text = "You Failed!\nPress Space to retry.";
                messageText.color = Color.red;
                levelFailed = true; 
            }

            messageText.gameObject.SetActive(true);
            winSound.Play();
            BGMSound.Stop();
            Time.timeScale = 0f;
        }

    }


    public TextMeshProUGUI pointText;
    public void ShowPopup(string text)
    {
        pointText.text = text;
        pointText.gameObject.SetActive(true);

        CancelInvoke(nameof(HidePopup));
        Invoke(nameof(HidePopup), 1f);
    }

    private void HidePopup()
    {
        pointText.gameObject.SetActive(false);
    }
}
