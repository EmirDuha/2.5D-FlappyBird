using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        // Oyunun başındaki konumu sakla
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void ResetFood()
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        transform.rotation = startRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("+50");

            // Player scriptini bul
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.point += 50;
                playerScript.ShowPopup("+50");
            }

            gameObject.SetActive(false); // Bu objeyi (küreyi) yok eder
        }
    }
}
