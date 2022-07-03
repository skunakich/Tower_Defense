using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int Waves;
    public static int EnemiesPerWave;
    public static GameObject Enemy;
    public static GameObject[] enemysLine;


    private void Start()
    {
        enemysLine = new GameObject[Waves*EnemiesPerWave];
        StartCoroutine(spawn());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private static IEnumerator spawn()
    {
        for (var a = 1; a < Waves; a++)
        {
            for (var b = 1; b < EnemiesPerWave; b++)
            {
                enemysLine[b + a * b] = Instantiate(Enemy,Platform.Startingposition.transform.position,Quaternion.identity);
                yield return new WaitForSeconds(1);
            }
        }
    }
    
}
