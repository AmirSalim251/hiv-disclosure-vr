using UnityEngine;
using System.Collections;

/// <summary>
/// Menempatkan pemain pada spawn point setelah memberi tracking XR dua frame untuk inisialisasi.
/// Root rig digeser dengan kompensasi offset kepala agar posisi kepala tepat pada tujuan di bidang XZ.
/// </summary>
public class VRSpawner : MonoBehaviour
{
    /// <summary>Memilih apakah tinggi root rig dipertahankan, ditetapkan, atau mengikuti spawn point.</summary>
    public enum HeightMode
    {
        // Pertahankan koordinat Y rig yang sudah digunakan.
        KeepCurrent,
        // Gunakan fixedHeight sebagai koordinat Y absolut.
        FixedHeight,
        // Tambahkan heightOffset pada koordinat Y spawn point.
        TargetPlusOffset
    }

    [Header("References")]
    public Transform xrRig;          // OVRCameraRig
    public Transform head;           // CenterEyeAnchor
    public Transform spawnPoint;

    [Header("Rotation")]
    public bool faceSpawnDirection = true;

    [Header("Height")]
    public HeightMode heightMode = HeightMode.KeepCurrent;

    [Tooltip("Digunakan jika HeightMode = FixedHeight")]
    public float fixedHeight = 0f;

    [Tooltip("Digunakan jika HeightMode = TargetPlusOffset")]
    public float heightOffset = 0f;

    /// <summary>Menunggu dua frame sebelum membaca posisi tracking kepala dan menempatkan pemain.</summary>
    IEnumerator Start()
    {
        // Tunggu tracking Oculus selesai
        yield return null;
        yield return null;

        SetPlayerPosition();
    }

    /// <summary>
    /// Menyelaraskan arah pandang horizontal dan menempatkan rig sesuai kebijakan tinggi.
    /// Tidak melakukan perpindahan jika xrRig, head, atau spawnPoint belum dihubungkan.
    /// </summary>
    public void SetPlayerPosition()
    {
        if (xrRig == null || head == null || spawnPoint == null)
            return;

        //--------------------------------------------------
        // ROTATE
        //--------------------------------------------------

        if (faceSpawnDirection)
        {
            // Proyeksikan arah ke bidang lantai agar kemiringan kepala tidak memiringkan rig.
            Vector3 currentForward = head.forward;
            currentForward.y = 0f;
            currentForward.Normalize();

            Vector3 targetForward = spawnPoint.forward;
            targetForward.y = 0f;
            targetForward.Normalize();

            float angle = Vector3.SignedAngle(
                currentForward,
                targetForward,
                Vector3.up);

            xrRig.Rotate(Vector3.up, angle, Space.World);
        }

        //--------------------------------------------------
        // OFFSET (XZ ONLY)
        //--------------------------------------------------

        // Offset harus dibaca setelah rotasi agar kompensasinya sesuai posisi kepala yang baru.
        Vector3 headOffset = head.position - xrRig.position;
        headOffset.y = 0f;

        //--------------------------------------------------
        // POSITION
        //--------------------------------------------------

        Vector3 newPos = spawnPoint.position;

        // Supaya posisi kepala tepat berada di spawn point
        newPos.x -= headOffset.x;
        newPos.z -= headOffset.z;

        // Tinggi diatur pada root rig, sementara tinggi lokal head tetap mengikuti perangkat XR.
        switch (heightMode)
        {
            case HeightMode.KeepCurrent:
                newPos.y = xrRig.position.y;
                break;

            case HeightMode.FixedHeight:
                newPos.y = fixedHeight;
                break;

            case HeightMode.TargetPlusOffset:
                newPos.y = spawnPoint.position.y + heightOffset;
                break;
        }

        xrRig.position = newPos;
    }
}
