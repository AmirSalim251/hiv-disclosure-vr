using UnityEngine;

/// <summary>
/// Membungkus data DialogueSequence agar dapat dipicu dari tombol, UnityEvent, atau DialogueController.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    // Dialog, sumber suara, UI teks, serta event awal/akhir diatur pada Inspector.
    public DialogueSequence dialogue;

    /// <summary>Meneruskan urutan yang terpasang ke singleton DialogueManager.</summary>
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
