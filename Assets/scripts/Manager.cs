using UnityEngine;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    [FormerlySerializedAs("Width")] public int width;
    [FormerlySerializedAs("Height")] public int height;
    [FormerlySerializedAs("Decoration")] public int decoration;
    [FormerlySerializedAs("EnemiesPerWave")] public int enemiesPerWave;
    [FormerlySerializedAs("Waves")] public int waves;
    [FormerlySerializedAs("Enemy")] public GameObject enemy;

    private void Awake()
    {
        Platform.Width = width;
        Platform.Height = height;
        Platform.Decoration = decoration;
        Spawner.Waves = waves;
        Spawner.EnemiesPerWave = enemiesPerWave;
        Spawner.Enemy = enemy;
    }
}
