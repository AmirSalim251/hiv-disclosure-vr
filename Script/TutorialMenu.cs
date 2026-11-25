using UnityEngine;

/// <summary>
/// Mengatur halaman tutorial kontrol VR dengan menyalakan satu GameObject halaman pada satu waktu.
/// Susun tutorialItem sesuai urutan tampilan; navigasi memakai batas indeks tetap pada implementasi ini.
/// </summary>
public class TutorialMenu : MonoBehaviour
{
    // Array halaman dan indeks halaman yang sedang ditampilkan.
    [SerializeField] GameObject[] tutorialItem;
    int tutorialItemIndex;

    /// <summary>Menampilkan halaman awal ketika komponen dimuat.</summary>
    private void Awake()
    {
        InitiateTutorial();
    }

    /// <summary>Mengatur indeks halaman ke nol lalu mengaktifkan halaman tersebut.</summary>
    public void InitiateTutorial()
    {
        tutorialItemIndex = 0;
        SetItemActive();
    }

    /// <summary>Menonaktifkan halaman lama dan maju; indeks 5 atau lebih kembali ke nol.</summary>
    public void IncrementIndex()
    {
        SetItemInactive();
        tutorialItemIndex++;
        if (tutorialItemIndex >= 5) tutorialItemIndex = 0;
        SetItemActive();
    }

    /// <summary>Mundur satu halaman; dari indeks nol berpindah ke indeks 5.</summary>
    public void DecrementIndex()
    {
        SetItemInactive();
        tutorialItemIndex--;
        if (tutorialItemIndex < 0) tutorialItemIndex = 5;
        SetItemActive();
    }

    /// <summary>Mengaktifkan halaman pada indeks saat ini; indeks harus tersedia dalam array.</summary>
    public void SetItemActive()
    {
        tutorialItem[tutorialItemIndex].gameObject.SetActive(true);
    }

    /// <summary>Menyembunyikan halaman pada indeks saat ini sebelum navigasi.</summary>
    public void SetItemInactive()
    {
        tutorialItem[tutorialItemIndex].gameObject.SetActive(false);
    }

    /// <summary>Berpindah langsung ke indeks yang diberikan oleh tombol atau UnityEvent.</summary>
    public void SetItemIndex(int index)
    {
        SetItemInactive();
        tutorialItemIndex = index;
        SetItemActive();
    }
}
