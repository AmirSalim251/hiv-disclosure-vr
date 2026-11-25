using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handler pilihan gender avatar pemain atau tenaga kesehatan pada menu.
/// Pasang komponen sesuai kelompok tombolnya; pilihan disimpan dalam GameData lintas scene.
/// </summary>
public class GenderController : MonoBehaviour
{
    // Tombol pilihan aktif dibuat tidak interactable sebagai penanda pilihan saat ini.
    public Button maleButton;
    public Button femaleButton;
    /// <summary>Memilih avatar pemain laki-laki (0) dan memperbarui status kedua tombol.</summary>
    public void changeToMale()
    {
        maleButton.interactable = false;
        femaleButton.interactable = true;
        
        GameData.Instance.jenisKelamin = 0;
    }

    /// <summary>Memilih avatar pemain perempuan (1) dan memperbarui status kedua tombol.</summary>
    public void changeToFemale()
    {
        maleButton.interactable = true;
        femaleButton.interactable = false;

        GameData.Instance.jenisKelamin = 1;
    }

    /// <summary>Memilih tenaga kesehatan laki-laki (0) untuk dialog resepsionis dan edukasi.</summary>
    public void changeNurseToMale()
    {
        maleButton.interactable = false;
        femaleButton.interactable = true;

        GameData.Instance.jenisKelaminNakes = 0;
    }

    /// <summary>Memilih tenaga kesehatan perempuan (1) untuk dialog resepsionis dan edukasi.</summary>
    public void changeNurseToFemale()
    {
        maleButton.interactable = true;
        femaleButton.interactable = false;

        GameData.Instance.jenisKelaminNakes = 1;
    }
}
