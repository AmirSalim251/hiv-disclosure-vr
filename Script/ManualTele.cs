using UnityEngine;
using Oculus.Interaction.Locomotion;

/// <summary>
/// Memindahkan XR rig ke TeleportInteractable sambil mengompensasi offset kepala pengguna.
/// Tujuannya menempatkan kepala pada titik tujuan di bidang XZ tanpa mengubah tinggi tracking.
/// </summary>
public class ManualTele : MonoBehaviour
{
    /// <summary>Menentukan sumber tinggi rig setelah teleport, bukan tinggi lokal headset.</summary>
    public enum HeightMode
    {
        // Pertahankan tinggi rig yang sedang digunakan.
        KeepCurrent,
        // Gunakan koordinat Y absolut dari fixedHeight.
        FixedHeight,
        // Gunakan tinggi target ditambah heightOffset.
        TargetPlusOffset
    }

    [Header("References")]
    public Transform xrRig;              // OVRCameraRig
    public Transform head;               // CenterEyeAnchor
    public TeleportInteractable teleportTarget;

    [Header("Rotation")]
    public bool faceTargetDirection = true;

    [Header("Height")]
    public HeightMode heightMode = HeightMode.KeepCurrent;

    [Tooltip("Digunakan jika Height Mode = FixedHeight")]
    public float fixedHeight = 0f;

    [Tooltip("Digunakan jika Height Mode = TargetPlusOffset")]
    public float heightOffset = 0f;

    /// <summary>
    /// Menyelaraskan arah pandang horizontal bila diminta, lalu memindahkan root rig.
    /// xrRig, head, dan teleportTarget wajib diisi; referensi kosong membatalkan perpindahan.
    /// </summary>
    public void Teleport()
    {
        if (xrRig == null || head == null || teleportTarget == null)
            return;

        Transform target = teleportTarget.transform;

        //----------------------------------------------------
        // ROTATE
        //----------------------------------------------------

        if (faceTargetDirection)
        {
            // Abaikan kemiringan kepala agar rig hanya berputar pada sumbu vertikal (yaw).
            Vector3 currentForward = head.forward;
            currentForward.y = 0f;
            currentForward.Normalize();

            Vector3 targetForward = target.forward;
            targetForward.y = 0f;
            targetForward.Normalize();

            float angle = Vector3.SignedAngle(currentForward, targetForward, Vector3.up);

            xrRig.Rotate(Vector3.up, angle, Space.World);
        }

        //----------------------------------------------------
        // OFFSET (XZ ONLY)
        //----------------------------------------------------

        // Hitung setelah rotasi karena posisi kepala terhadap root ikut berubah saat rig diputar.
        Vector3 headOffset = head.position - xrRig.position;
        headOffset.y = 0f;

        //----------------------------------------------------
        // POSITION
        //----------------------------------------------------

        Vector3 newPos = target.position;

        // supaya kepala tepat berada di teleport point (XZ)
        newPos.x -= headOffset.x;
        newPos.z -= headOffset.z;

        // Kebijakan tinggi diterapkan pada root rig; posisi lokal head tetap berasal dari tracking XR.
        switch (heightMode)
        {
            case HeightMode.KeepCurrent:
                newPos.y = xrRig.position.y;
                break;

            case HeightMode.FixedHeight:
                newPos.y = fixedHeight;
                break;

            case HeightMode.TargetPlusOffset:
                newPos.y = target.position.y + heightOffset;
                break;
        }

        xrRig.position = newPos;
    }
}
