using UnityEngine;

/// <summary>
/// Menghubungkan event dialog resepsionis dengan parameter animasi berbicara.
/// Pasang Animator karakter pada Inspector dan siapkan parameter bool "isTalking".
/// </summary>
public class AnimPlayer : MonoBehaviour
{
    // Animator karakter yang diubah oleh UnityEvent sebelum/sesudah dialog.
    public Animator anim;
    /// <summary>Menyalakan atau mematikan animasi berbicara pada karakter.</summary>
    public void setReceptionistIsTalkingAim(bool isTalking)
    {
        anim.SetBool("isTalking", isTalking);
        
    }

    
}
