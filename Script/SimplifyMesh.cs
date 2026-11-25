using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine.Serialization;

/// <summary>
/// Mengurangi kompleksitas mesh saat runtime menggunakan UnityMeshSimplifier.
/// Mendukung MeshFilter atau SkinnedMeshRenderer pada GameObject yang sama, dengan prioritas MeshFilter.
/// </summary>
public class SimplifyMesh : MonoBehaviour
{
    // Rasio kualitas target library (umumnya 0..1); 0.5 meminta sekitar separuh jumlah segitiga.
    public float quality = 0.5f;

    /// <summary>Mencari mesh sumber, menyederhanakannya, lalu memasang mesh hasil pada renderer.</summary>
    void Start()
    {
        Mesh meshToSimplify = null;
        MeshFilter mf = GetComponent<MeshFilter>();
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();

        // --- CHECKING ---
        if (mf != null && mf.sharedMesh != null)
        {
            meshToSimplify = mf.sharedMesh;
        }
        else if (smr != null && smr.sharedMesh != null)
        {
            meshToSimplify = smr.sharedMesh;
        }
        else
        {
            Debug.LogWarning($"[{name}] Tidak ada MeshFilter atau SkinnedMeshRenderer yang memiliki mesh.");
            return; // langsung keluar
        }

        // --- SIMPLIFY ---
        // ToMesh menghasilkan mesh baru sehingga data mesh sumber tidak diedit secara langsung.
        var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
        meshSimplifier.Initialize(meshToSimplify);
        meshSimplifier.SimplifyMesh(quality);
        var destMesh = meshSimplifier.ToMesh();

        // --- APPLY KEMBALI ---
        if (mf != null)
        {
            mf.sharedMesh = destMesh;
        }
        else if (smr != null)
        {
            smr.sharedMesh = destMesh;
        }
    }
}
