using UnityEngine;

/// <summary>
/// Mengubah ekspresi karakter melalui parameter integer "expression" pada Animator.
/// Nilai state harus sesuai dengan kondisi transisi dalam Animator Controller karakter.
/// </summary>
public class AnimStateController : MonoBehaviour
{   private Animator anim;
    // Menyimpan ekspresi terakhir yang dipilih, sehingga dapat diperiksa di Inspector.
    public int state;
   
    /// <summary>Mengambil Animator dari GameObject tempat komponen ini dipasang.</summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    /// <summary>Menyimpan indeks ekspresi dan mengirimkannya ke Animator.</summary>
    public void setState(int newState)
    {
        state = newState;
        anim.SetInteger("expression", state);
        
    }
    
}
