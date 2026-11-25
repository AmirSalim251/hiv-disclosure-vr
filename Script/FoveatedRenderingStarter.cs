using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

/// <summary>
/// Menetapkan tingkat foveated rendering pada XR display untuk membantu kinerja aplikasi VR.
/// Pengaturan diterapkan sekali saat Start jika tepat satu display subsystem ditemukan.
/// </summary>
public class FoveatedRenderingStarter : MonoBehaviour
{    public static FoveatedRenderingStarter Instance;
    // Tingkat foveasi dari Inspector; dukungan aktual ditentukan oleh perangkat dan XR provider.
    public float foveatedLevel;

    // Daftar display yang diisi oleh SubsystemManager ketika sesi XR mulai tersedia.
    List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();
     /// <summary>Mempertahankan satu pengatur foveasi lintas scene dan membuang duplikat.</summary>
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
    /// <summary>Mengonfigurasi display tunggal dengan level foveasi dan flag GazeAllowed.</summary>
    void Start()
    {
        SubsystemManager.GetSubsystems(displaySubsystems);
        if(displaySubsystems.Count == 1)
        {
            displaySubsystems[0].foveatedRenderingLevel = foveatedLevel;
            // Flag mengizinkan gaze bila didukung; tidak membuat eye tracking tersedia di perangkat.
            displaySubsystems[0].foveatedRenderingFlags = XRDisplaySubsystem.FoveatedRenderingFlags.GazeAllowed;
        }
        
    }

    /// <summary>Callback per frame yang saat ini belum menjalankan pembaruan tambahan.</summary>
    void Update()
    {
        
    }
}
