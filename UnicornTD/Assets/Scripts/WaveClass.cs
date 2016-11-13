using UnityEngine;
using System.Collections;

public class WaveClass : MonoBehaviour {

    public string cName; //what is this enemies name
    public UnityEngine.GameObject cEnemy; //what enemy in this wave
    public int cEnemyCount; //how many enemies in this wave
    public float cCadence; //what is the spawn delay between enemies in this wave

    /// <summary>
    /// Object for holding a waves info
    /// </summary>
    /// <param name="WaveCount">Total waves</param>
    /// <param name="cEnemy">Current Enemy</param>
    /// <param name="cEnemyCount">How many of current enemy will spawn in this wave</param>
    /// <param name="cCadence">What is the delay between spawns</param>
    public WaveClass(string cName, UnityEngine.GameObject cEnemy, int cEnemyCount, float cCadence)
    {
        this.cName = cName;
        this.cEnemy = cEnemy;
        this.cEnemyCount = cEnemyCount;
        this.cCadence = cCadence;
    }
}
