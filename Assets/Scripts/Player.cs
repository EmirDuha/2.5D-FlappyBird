using System;
using TMPro;
using UnityEditor.ShaderGraph;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        // Yön tuşlarını sürekli oku
        horizontalInput = Input.GetAxis("Horizontal");

        // Space tuşuna basılırsa zıplat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpSound.Play();
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
            Debug.Log("Your Point: " + point);
        }



        else if (collision.gameObject.CompareTag("Finish"))
        {

            if (point == 100)
            {
                messageText.text = "Level Completed!";
                messageText.color = Color.green;
            }
            else
            {
                messageText.text = "You Failed!";
                messageText.color = Color.red;
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
        // TMP Text nesnesinin içeriğini değiştir
        pointText.text = text;
        pointText.gameObject.SetActive(true);

        // 1 saniye sonra gizle
        CancelInvoke(nameof(HidePopup));
        Invoke(nameof(HidePopup), 1f);
    }

    private void HidePopup()
    {
        pointText.gameObject.SetActive(false);
    }
}
