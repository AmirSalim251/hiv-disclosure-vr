using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Handler tombol perpindahan ruangan dengan offset posisi tetap pada scene klinik.
/// Objek yang dipindahkan diberikan oleh UnityEvent tombol, lalu dialog aktif diakhiri.
/// </summary>
public class ButtonScript : MonoBehaviour
{
    /// <summary>
    /// Menggeser objek ke ruang edukasi dengan offset yang disesuaikan dengan layout scene.
    /// Translate memakai ruang lokal objek; ini perpindahan relatif, bukan koordinat tujuan absolut.
    /// </summary>
    public void TeleportToEdRoom(GameObject obj)
    {
        obj.transform.Translate(48, -5, 48);
        DialogueManager.Instance.EndDialogue();
    }
    /// <summary>Menggeser objek ke ruang simulasi dan menjalankan penyelesaian dialog.</summary>
    public void TeleportToSimRoom(GameObject obj)
    {
        obj.transform.Translate(-10, 0, -40);
        DialogueManager.Instance.EndDialogue();
    }
}
