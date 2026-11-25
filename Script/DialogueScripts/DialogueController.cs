using UnityEngine;
using static OVRPlugin;
using UnityEngine.EventSystems;

/// <summary>
/// Registri dialog bernama yang mencari DialogueTrigger pada root dan child aktif saat Awake.
/// Komponen ruangan memanggil registri ini menggunakan nama dialog yang diatur pada Inspector.
/// </summary>
public class DialogueController : MonoBehaviour
{
    // Diisi dari root dan hierarki child saat Awake; child nonaktif tidak ikut tanpa includeInactive.
    public DialogueTrigger[] dialogues;

    public static DialogueController Instance;

    /// <summary>Mempertahankan registri pertama lintas scene lalu mengambil daftar pemicu dialog.</summary>
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
        dialogues = GetComponentsInChildren<DialogueTrigger>();
    }

    /// <summary>
    /// Memicu semua dialog dengan nama yang cocok persis (peka huruf besar/kecil).
    /// Gunakan nama unik agar satu permintaan tidak menjalankan beberapa pemicu sekaligus.
    /// </summary>
    public void TriggerDialogueByName(string eventName)
    {
        foreach (DialogueTrigger go in dialogues)
        {
            if (go.dialogue.dialogueName == eventName)
            {
                go.TriggerDialogue();
            }
        }
    }
}
