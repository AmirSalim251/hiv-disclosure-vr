using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Menghubungkan VideoPlayer edukasi dengan tombol pemutaran dan slider waktu.
/// Slider menampilkan waktu dalam detik; metode RW/FF menggeser playback sebanyak lima detik.
/// </summary>
public class VideoPlayerScript : MonoBehaviour
{
    // Referensi pemutar dan slider diatur dari Inspector pada panel video ruang edukasi.
    [SerializeField] VideoPlayer player;
    [SerializeField] Slider timeSlider;
    // Status lokal yang menentukan apakah slider mengikuti waktu pemutar setiap frame.
    bool isVideoPlaying = false;

    /// <summary>Menggunakan durasi yang tersedia dari VideoPlayer sebagai batas slider.</summary>
    private void Start()
    {
        timeSlider.maxValue = (float)player.length;
    }

    /// <summary>Menyinkronkan slider selama pemutaran dimulai melalui PlayVideo.</summary>
    void Update()
    {
        if (isVideoPlaying)
        {
            timeSlider.value = (float)player.time;
        }
    }

    /// <summary>Memulai atau melanjutkan video dan mengaktifkan pembaruan slider.</summary>
    public void PlayVideo()
    {
        player.Play();
        isVideoPlaying = true;
    }

    /// <summary>Menjeda video pada waktu saat ini dan menghentikan pembaruan slider.</summary>
    public void PauseVideo()
    {
        player.Pause();
        isVideoPlaying = false;
    }

    /// <summary>Menghentikan video dan mengembalikan indikator waktu ke awal.</summary>
    public void ResetVideo()
    {
        player.Stop();
        timeSlider.value = 0;
        isVideoPlaying = false;
    }

    /// <summary>Mundur lima detik dan langsung memperbarui indikator waktu.</summary>
    public void RW()
    {
        player.time -= 5.0f;
        timeSlider.value = (float)player.time;
    }

    /// <summary>Maju lima detik dan langsung memperbarui indikator waktu.</summary>
    public void FF()
    {
        player.time += 5.0f;
        timeSlider.value = (float)player.time;
    }
}
