using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _nextSpawnTime;
    [SerializeField] private float _spawnDelay = 12.0f;
    [SerializeField] MummyController[] _bioMechPrefab;
    private int _spawnCount;

    // Update is called once per frame
    void Update()
    {
        if (ReadyToSpawn())
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        float delay = _spawnDelay - _spawnCount;
        delay = Mathf.Max(1, delay); // Will prevent delay from going below 1
        
        _nextSpawnTime = Time.time + _spawnDelay;
        _spawnCount++;

        int randomIndex = UnityEngine.Random.Range(0, _bioMechPrefab.Length);
        var bioMechToSpawn = _bioMechPrefab[randomIndex];
        
        var bioMech = Instantiate(bioMechToSpawn, transform.position, transform.rotation);
       // GetComponent<Animator>().SetBool("Spawned", true); // Spawn Effects play
         bioMech.ScreamAnimation();
        yield return new WaitForSeconds(2.0f);
        bioMech.StartWalking();
        yield return new WaitForSeconds(3.0f);
        //GetComponent<Animator>().SetBool("Spawned", false);
    }

    private bool ReadyToSpawn() => Time.time >= _nextSpawnTime;
    
}
