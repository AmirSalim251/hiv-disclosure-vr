using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Membuka akses titik teleport pertama ketika antrean teks dan suara dialog sudah kosong.
/// Referensi DialogueManager dan titik teleport dihubungkan melalui Inspector scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Titik utama yang diaktifkan saat antrean habis; dua titik lain disimpan untuk pengaturan scene.
    public GameObject Telepoint0;
    public GameObject Telepoint1;
    public GameObject Telepoint2;

    public DialogueManager dialogueManager;
    /// <summary>Callback inisialisasi yang belum memerlukan pekerjaan tambahan.</summary>
    void Start()
    {
        
    }

    /// <summary>Memeriksa antrean dialog setiap frame sebelum mengaktifkan Telepoint0.</summary>
    void Update()
    {
        // Baris dikeluarkan dari antrean saat mulai diputar, bukan setelah suara selesai.
        // Kondisi ini menyatakan antrean kosong; baris terakhir masih mungkin sedang berlangsung.
        if(dialogueManager.dialogueVoiceLines.Count == 0 && dialogueManager.dialogueTexts.Count == 0)
        {
            Debug.Log("Dialogue Finished");
            Telepoint0.SetActive(true);
            // Telepoint1.SetActive(false);
            // Telepoint2.SetActive(false);
            
          
        }
        
    }
}
