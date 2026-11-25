using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Data satu urutan dialog yang dapat disunting di Inspector sebagai bagian dari DialogueTrigger.
/// Urutan elemen dialogueLines dan dialogueAudio harus berpasangan, satu klip untuk setiap baris.
/// </summary>
[System.Serializable]
public class DialogueSequence
{
    // ID unik yang harus sama persis dengan nama yang dikirim ke TriggerDialogueByName.
    public string dialogueName;

    // Sumber suara karakter dan komponen teks tempat dialog ditampilkan.
    [SerializeField]
    public AudioSource audioSource;

    [SerializeField]
    public TextMeshProUGUI textBox;

    [SerializeField]
    public AudioClip[] dialogueAudio;

    // Newline menghapus halaman setelah jeda; | memberi jeda tanpa mencetak simbol pada layar.
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [SerializeField]
    public UnityEvent preDialogueAction;

    // Event akhir juga dipanggil saat EndDialogue diminta secara manual.
    [SerializeField]
    public UnityEvent postDialogueAction;
}
