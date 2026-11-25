using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Mengatur latihan HIV disclosure: memilih lawan bicara, menyampaikan status, memutar reaksi,
/// dan menampilkan tanggapan lanjutan. Nama dialog dan nama child model harus sesuai dengan scene.
/// </summary>
public class SimulationRoomScript : MonoBehaviour
{
    // Panel pilihan, panel teks, serta parent model lawan bicara dihubungkan melalui Inspector.
    [SerializeField]
    GameObject ScenarioSelector;
    [SerializeField]
    GameObject TextPanel;
    [SerializeField]
    GameObject SimRespondent;
    // Nilai dropdown: 0 = positif, 1 = netral, 2 = negatif, 3 = acak.
    public TMP_Dropdown scenarioDropdown;
  

    // Indeks reaksi dan pilihan gender pemain yang dibaca dari GameData.
    int index = 0;
    int? gender = 0;
    string genderString;
    // Sufiks nama dialog reaksi; urutannya juga menentukan pemetaan nilai dropdown.
    string[] scenarios;

    /// <summary>Menyiapkan daftar delapan reaksi dan tombol yang sesuai dengan gender pemain.</summary>
    private void Awake()
    {
        gender = GameData.Instance?.jenisKelamin;
        scenarios = new string[] {
            "Positive Supportive",
            "Positive Sad",
            "Positive Flat",
            "Neutral Empty",
            "Neutral Shocked",
            "Neutral Denial",
            "Negative Angry",
            "Negative Panic"
        };
        this.scenarioUITrigger();
    }

    /// <summary>
    /// Mengaktifkan tombol pasangan dan teman sesuai gender, dengan default laki-laki jika tanpa GameData.
    /// Hierarki ScenarioSelector harus memiliki child "ButtonW 0/1" dan "ButtonF 0/1".
    /// </summary>
    public void scenarioUITrigger()
    {
        if(gender == null)
        {
            gender = 0;
        }

        ScenarioSelector.transform.Find("ButtonW "+gender.ToString()).gameObject.SetActive(true);
        ScenarioSelector.transform.Find("ButtonF "+gender.ToString()).gameObject.SetActive(true);

    }

    /// <summary>
    /// Memulai pengungkapan status kepada orang tua, pasangan, atau teman dan menampilkan modelnya.
    /// ID dari tombol adalah Ortu, Suami, Istri, atau Teman; ID pasangan mengikuti penamaan event scene.
    /// </summary>
    public void ConfessionTrigger(string scenario)
    {
        ScenarioSelector.SetActive(false);
        TextPanel.SetActive(true);
        genderString = (gender == 0) ? "Male" : "Female";

        switch (scenario)
        {
            case "Ortu":
                SimRespondent.transform.Find("Model Father").gameObject.SetActive(true);
                DialogueController.Instance.TriggerDialogueByName("Parent Confession " + genderString);
                break;
            // "Suami" berarti pemain suami mengungkapkan status kepada model istri.
            case "Suami":
                SimRespondent.transform.Find("Model Wife").gameObject.SetActive(true);
                DialogueController.Instance.TriggerDialogueByName("Husband Confession");
                break;
            // "Istri" berarti pemain istri mengungkapkan status kepada model suami.
            case "Istri":
                SimRespondent.transform.Find("Model Husband").gameObject.SetActive(true);
                DialogueController.Instance.TriggerDialogueByName("Wife Confession");
                break;
            case "Teman":
                SimRespondent.transform.Find("Model Friend " + genderString).gameObject.SetActive(true);
                DialogueController.Instance.TriggerDialogueByName("Friend Confession " + genderString);
                break;
            default:
                break;  
        }
    }

    /// <summary>Memutar reaksi lawan bicara sesuai kategori dropdown atau pilihan acak.</summary>
    public void ScenarioTrigger(string scenario)
    {
        string selectedScenario = "";
       ;
        if(scenarioDropdown.value == 3)
        {
            // Batas atas overload integer bersifat eksklusif: hasilnya indeks 0 sampai 7.
            index = Random.Range(0, 8);
        }

        else
        {
            // Kategori tetap memilih reaksi pertama kelompok: 0 (positif), 3 (netral), 6 (negatif).
            // Reaksi lain dalam kelompok saat ini hanya terpilih melalui mode acak.
            index = (scenarioDropdown.value  * 3);
        }
        selectedScenario = scenarios[index];
        switch (scenario)
        {
            case "Ortu":
                DialogueController.Instance.TriggerDialogueByName("Parent " + selectedScenario);
                break;
            case "Istri":
                DialogueController.Instance.TriggerDialogueByName("Wife " + selectedScenario);
                break;
            case "Suami":
                DialogueController.Instance.TriggerDialogueByName("Husband " + selectedScenario);
                break;
            case "Teman":
                DialogueController.Instance.TriggerDialogueByName(genderString + " Friend " +selectedScenario);
                break;
            default:
                break;
        }
    }

    /// <summary>Menjalankan tanggapan lanjutan pemain untuk ID lawan bicara yang diberikan.</summary>
    public void ResponseTrigger(string scenario)
    {
        genderString = (gender == 0) ? "Male" : "Female";

        switch (scenario)
        {
            case "Ortu":
                DialogueController.Instance.TriggerDialogueByName("Parent Response " + genderString);
                break;
            case "Suami":
                DialogueController.Instance.TriggerDialogueByName("Husband Response");
                break;
            case "Istri":
                DialogueController.Instance.TriggerDialogueByName("Wife Response");
                break;
            case "Teman":
                DialogueController.Instance.TriggerDialogueByName("Friend Response " + genderString);
                break;
            default:
                break;
        }
    }

    /// <summary>Mengembalikan panel pemilihan dan menyembunyikan seluruh model lawan bicara.</summary>
    public void EndScenario()
    {
        ScenarioSelector.SetActive(true);
        TextPanel.SetActive(false);
        SimRespondent.transform.Find("Model Friend Male").gameObject.SetActive(false);
        SimRespondent.transform.Find("Model Friend Female").gameObject.SetActive(false);
        SimRespondent.transform.Find("Model Father").gameObject.SetActive(false);
        SimRespondent.transform.Find("Model Husband").gameObject.SetActive(false);
        SimRespondent.transform.Find("Model Wife").gameObject.SetActive(false);
    }


}
