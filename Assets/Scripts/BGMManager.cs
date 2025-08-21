using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    private AudioSource audioSource;
    private float savedTime = 0f; // müziğin kaldığı zamanı tutar

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // sahne geçişinde yok olmasın
            audioSource = GetComponent<AudioSource>();
            audioSource.Play(); // oyun başlarken çalsın
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Müzik durdurulacaksa çağır
    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            savedTime = audioSource.time; // o anki süreyi kaydet
            audioSource.Pause();
        }
    }

    // Yeni level başlayınca kaldığı yerden devam ettir
    public void ResumeMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.time = savedTime; // kaldığı süreye sar
            audioSource.Play();
        }
    }

    // Eğer oyunun tamamen bitmesini istiyorsan (MainMenu vs.)
    public void StopMusic()
    {
        audioSource.Stop();
        savedTime = 0f;
    }
}
