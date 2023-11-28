using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _duration = 5.0f;
    [SerializeField] private float _delayMultiplier = 0.5f;
    [SerializeField] private float _coolDown = 10.0f;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool _spreadShot;
    public float DelayMultiplier => _delayMultiplier; // Public Accessor Read Only
    public bool SpreadShot => _spreadShot;

    private void OnTriggerEnter(Collider other)
    {
        var playerWeapon = other.GetComponent<PlayerWeapon>();

        if (playerWeapon)
        {
            Debug.Log("Ontrigger Powerup");
            playerWeapon.AddPowerup(this);
            StartCoroutine(DisableAfterDelay(playerWeapon));
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;


        }
    }

    private IEnumerator DisableAfterDelay(PlayerWeapon playerWeapon)
    {
        yield return  new WaitForSeconds(_duration);
        playerWeapon.RemovePowerup(this);
        
        
        yield return  new WaitForSeconds(_coolDown);
        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        transform.position = _spawnPoints[randomIndex].position;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        
        
    }
}
