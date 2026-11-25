using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Menyimpan pilihan avatar pemain dan tenaga kesehatan agar tetap tersedia saat berganti scene.
/// GameData dibuat dari scene menu dan dipertahankan menggunakan pola singleton.
/// </summary>
public class GameData : MonoBehaviour
{
    // Instance bersama yang dibaca komponen menu, resepsionis, edukasi, dan simulasi.
    public static GameData Instance;
    public int jenisKelamin = 0; //(0 = laki-laki, 1 = perempuan)
    // Konvensi tenaga kesehatan sama dengan pemain: 0 = laki-laki, 1 = perempuan.
    public int jenisKelaminNakes = 0;

    /// <summary>Mempertahankan instance pertama lintas scene dan menghapus GameObject duplikat.</summary>
    void Awake()
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
    }
}
