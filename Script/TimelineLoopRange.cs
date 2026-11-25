using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Mengulang sebagian Timeline selama PlayableDirector sedang bermain.
/// Rentang waktu dalam detik dapat disesuaikan di Inspector, misalnya untuk animasi idle atau berbicara.
/// </summary>
public class TimelineLoopRange : MonoBehaviour
{
    public PlayableDirector director;
    // Atur 0 <= loopStart < loopEnd dan pastikan rentang berada dalam durasi Timeline.
    public double loopStart = 3.0;
    public double loopEnd = 8.0;

    /// <summary>Mengembalikan waktu ke awal rentang dan mengevaluasi pose pada frame yang sama.</summary>
    void Update()
    {
        if (director.state != PlayState.Playing) return;

        if (director.time >= loopEnd)
        {
            director.time = loopStart;
            director.Evaluate(); // langsung lompat tanpa freeze
        }
    }
}
