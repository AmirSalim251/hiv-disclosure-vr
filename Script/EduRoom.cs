using UnityEngine;

/// <summary>
/// Memilih dialog FAQ HIV disclosure sesuai gender tenaga kesehatan yang dipilih di menu.
/// Nama dialog di DialogueController harus mengikuti pola "Male/Female" diikuti nama pertanyaan.
/// </summary>
public class EduRoom : MonoBehaviour
{
    // Nullable karena scene dapat dibuka langsung tanpa GameData dari scene menu.
    int? gender = 0;
    // Prefiks yang dipakai untuk menemukan urutan dialog tenaga kesehatan.
    string genderName = "";
    
    /// <summary>Membaca pilihan tenaga kesehatan; tanpa GameData memakai karakter perempuan.</summary>
    void Awake()
    {
        gender = GameData.Instance?.jenisKelaminNakes;
        if  (gender == null)
        {
            gender = 1;
        }
       

        if(gender == 0)
        {
            genderName = "Male";
        }
        else
        {
            genderName = "Female";
        }
        
    }

    /// <summary>Menjalankan FAQ dengan nama gabungan prefiks gender dan ID pertanyaan.</summary>
    public void playFQA(string qSomething)
    {
        DialogueController.Instance.TriggerDialogueByName(genderName + " " + qSomething);
        
    }
}
