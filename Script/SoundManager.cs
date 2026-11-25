using UnityEngine;

/// <summary>
/// Menyediakan pemutaran voice-over dialog melalui AudioSource yang dipilih oleh DialogueSequence.
/// Singleton dipertahankan lintas scene; sumber audio perlu diatur sebelum PlayAudio dipanggil.
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Sumber aktif juga diperiksa DialogueManager untuk menunggu suara selesai atau menghentikannya.
    public AudioSource source;

    /// <summary>Menjaga satu manajer suara sepanjang perpindahan scene dan menghapus duplikat.</summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>Mengganti AudioSource aktif, misalnya ke sumber milik karakter yang sedang berbicara.</summary>
    public void SetAudioSource(AudioSource audioSource)
    {
        source = audioSource;
    }

    /// <summary>Memainkan klip satu kali pada sumber aktif menggunakan PlayOneShot.</summary>
    public void PlayAudio(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
