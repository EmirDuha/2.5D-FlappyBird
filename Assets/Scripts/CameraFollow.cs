using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek nesne (oyuncu)
    public Vector3 offset = new Vector3(0, 5, -10); // Kamera ile oyuncu arasındaki mesafe
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Hedef pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;

        // Kamerayı yumuşakça oraya kaydır
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // Oyuncuya bakmasını istiyorsan:
         transform.LookAt(target);
    }
}
