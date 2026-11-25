using Oculus.Interaction;
using UnityEngine;

/// <summary>
/// Menampilkan karakter resepsionis dan membuka dialog pengantar sesuai pilihan tenaga kesehatan.
/// Kedua model dan dialog "Introduction Male/Female" perlu disiapkan pada scene klinik.
/// </summary>
public class ReceptionistSceneDialogue : MonoBehaviour
{
    // Referensi model diatur melalui Inspector; status awal model lain mengikuti konfigurasi scene.
    [SerializeField]
    GameObject FemaleModel;
    [SerializeField]
    GameObject MaleModel;

    /// <summary>Memulai pengantar laki-laki untuk nilai 0; nilai lain/tanpa GameData memakai perempuan.</summary>
    void Start()
    {
        if (GameData.Instance?.jenisKelaminNakes == 0)
        {
            MaleModel.SetActive(true);
            DialogueController.Instance.TriggerDialogueByName("Introduction Male");
        } else
        {
            FemaleModel.SetActive(true);
            //DialogueManager.Instance.anim = FemaleModel.GetComponent<Animator>();
            DialogueController.Instance.TriggerDialogueByName("Introduction Female");
        }
    }

    /// <summary>
    /// Mengakhiri dialog aktif dan memicu nama baru hanya untuk pilihan selain tenaga kesehatan laki-laki.
    /// Kondisi gender ini merupakan perilaku handler yang saat ini dihubungkan dari event scene.
    /// </summary>
    public void ChangeVoiceLine(string dialogueName)
    {
        if (GameData.Instance?.jenisKelaminNakes != 0)
        {
            DialogueManager.Instance.EndDialogue();
            DialogueController.Instance.TriggerDialogueByName(dialogueName);
        }
    }
}
