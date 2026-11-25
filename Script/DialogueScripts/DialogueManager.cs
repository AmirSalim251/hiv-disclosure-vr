using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Oculus.Interaction.Context;
using UnityEngine.Events;

/// <summary>
/// Menjalankan urutan teks, voice-over, efek ketik, serta UnityEvent awal/akhir dialog.
/// Teks dan audio diproses sebagai dua antrean sejajar dengan satu klip untuk setiap baris teks.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // Target teks berasal dari DialogueSequence yang sedang dimainkan.
    TextMeshProUGUI textBox;
    public static DialogueManager Instance;
    // Jika aktif, baris berikutnya dimulai setelah efek ketik dan suara baris saat ini selesai.
    public bool isAuto;

    // Antrean bersifat publik agar statusnya dapat dibaca oleh GameManager.
    public Queue<string> dialogueTexts;
    public Queue<AudioClip> dialogueVoiceLines;
    private string currentLine;
    private AudioClip currentVoiceLine;
 

    // Aksi penyelesaian terakhir yang disimpan ketika StartDialogue menerima urutan dialog.
    UnityEvent postDialogAction;

    /// <summary>Menjaga singleton lintas scene dan menyiapkan antrean teks serta audio.</summary>
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
        dialogueTexts = new Queue<string>();
        dialogueVoiceLines = new Queue<AudioClip>();
    }

    /// <summary>
    /// Memilih sumber suara dan UI, memasukkan isi dialog ke antrean, lalu memulai baris pertama.
    /// Pemanggil perlu menyiapkan AudioSource, textBox, dan pasangan teks/audio yang lengkap.
    /// </summary>
    public void StartDialogue(DialogueSequence dialogue)
    {
        SoundManager.Instance.SetAudioSource(dialogue.audioSource);
        textBox = dialogue.textBox;

        // Jangan mulai urutan jika sumber suara yang dipilih masih memutar audio.
        // Pemeriksaan berlaku pada AudioSource milik urutan baru yang baru saja dipilih.
        if (SoundManager.Instance.source.isPlaying)
        {
            return;
        }

        // Isi ditambahkan ke antrean yang ada; hentikan dialog lama bila menginginkan penggantian.
        foreach (string line in dialogue.dialogueLines)
        {
            dialogueTexts.Enqueue(line);
        }
        foreach (AudioClip voiceLine in dialogue.dialogueAudio)
        {
            dialogueVoiceLines.Enqueue(voiceLine);
        }

        // Event dapat menyiapkan ekspresi, animasi, atau panel sebelum teks mulai ditampilkan.
        dialogue.preDialogueAction?.Invoke();

        DisplayNextLine();

        //dialogue.preDialogueAction?.Invoke();
        // Callback disimpan setelah baris pertama dimulai, mengikuti urutan eksekusi komponen ini.
        if (dialogue.postDialogueAction != null)
            postDialogAction = dialogue.postDialogueAction;
    }

    /// <summary>
    /// Mengambil pasangan teks/audio berikutnya atau mengakhiri dialog ketika teks habis.
    /// Jumlah klip audio harus mencukupi seluruh baris karena keduanya di-Dequeue bersamaan.
    /// </summary>
    public void DisplayNextLine()
    {
        if (dialogueTexts.Count == 0)
        {
            EndDialogue();
            return;
        }

        

        currentLine = dialogueTexts.Dequeue();
        currentVoiceLine = dialogueVoiceLines.Dequeue();

        StartCoroutine(WriteText(currentLine, currentVoiceLine, textBox));
    }

    /// <summary>
    /// Memutar voice-over sambil menulis karakter bertahap menggunakan jeda berbasis waktu permainan.
    /// Newline mengganti halaman setelah satu detik; tanda | memberi jeda tanpa ditampilkan.
    /// </summary>
    IEnumerator WriteText(string text, AudioClip voiceLine, TextMeshProUGUI textBox)
    {
        textBox.text = "";
        SoundManager.Instance.PlayAudio(voiceLine);
        foreach (char c in text)
        {
            if (c == '\n')
            {
                yield return new WaitForSeconds(1f);
                textBox.text = "";
            }else if (c == '|')
            {
                yield return new WaitForSeconds(1f);
            // Tanda baca ditampilkan dengan jeda lebih panjang agar teks mudah diikuti.
            } else if (c == ',' || c == '.' || c == ':' || c == '?' || c == '!')
            {
                textBox.text += c;
                yield return new WaitForSeconds(0.75f);
            } else 
            {
                textBox.text += c;
                yield return new WaitForSeconds(0.03f);
            }
        }
        // Audio yang lebih panjang daripada animasi ketik tetap ditunggu sebelum maju otomatis.
        while (SoundManager.Instance.source.isPlaying)
        {
            yield return null;
        }
        if (isAuto)
            DisplayNextLine();
    }

    /// <summary>
    /// Mengosongkan antrean dan UI, menghentikan suara serta coroutine, lalu memanggil event akhir.
    /// Penghentian manual juga menjalankan callback akhir; metode ini bukan pembatalan tanpa event.
    /// </summary>
    public void EndDialogue()
    {
        dialogueTexts.Clear();
        dialogueVoiceLines.Clear();

        textBox.text = "";
        SoundManager.Instance.source.Stop();
    

        StopAllCoroutines();
        postDialogAction?.Invoke();
    }
}
