
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Menyediakan handler UnityEvent untuk berpindah scene atau menutup aplikasi hasil build.
/// Scene tujuan harus tersedia dalam daftar scene pada Build Settings atau Build Profile aktif.
/// </summary>
public class SceneManagementScript : MonoBehaviour
{
    // Nilai yang disediakan di Inspector; changeScene menerima nama dari parameter event-nya sendiri.
    public string sceneName;
    /// <summary>Memuat scene dengan nama yang diberikan dan mengganti scene aktif.</summary>
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>Menutup aplikasi hasil build; Application.Quit tidak menghentikan Play Mode Editor.</summary>
    public void quitGame()
    {
        Application.Quit();
    }
    
}
