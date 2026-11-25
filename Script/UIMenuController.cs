using UnityEditor;
using UnityEngine;

/// <summary>
/// Mengelola visibilitas kelompok panel menu menggunakan indeks dari UnityEvent tombol.
/// Semua panel dalam array diperiksa agar hanya panel yang dipilih yang aktif.
/// </summary>
public class UIMenuController : MonoBehaviour
{
    // Urutan panel sesuai nomor yang diberikan tombol; index tersimpan belum dipakai oleh showMenu.
    public GameObject[] menus;
    public int index;
   /// <summary>Menampilkan panel pada menuIndex; indeks di luar array menyembunyikan semua panel.</summary>
   public void showMenu(int menuIndex)
   {
  
       for (int i = 0; i < menus.Length; i++)
       {
        
           if (i == menuIndex)
           {
         
               menus[i].SetActive(true);
           }
           else
           {
               menus[i].SetActive(false);
           }
       }
   }
}
