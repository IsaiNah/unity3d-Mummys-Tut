using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private float _nextFireTime;
    [SerializeField] private float _delay = 0.25f;
    [SerializeField] private BlasterShot _blasterShotPrefab;
    [SerializeField] private Transform _blasterInstantiateTransform;
    [SerializeField] private LayerMask _aimLayerMask;
  
    private List<PowerUp> _powerups = new List<PowerUp>();

    void Update()
    {
        AimTowardsMouse();
        
        if (ReadyToFire())
            Fire();
    }

    private void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask)) ;
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y; // removes the y point of hit to hit directly at ground

            Vector3 direction = destination - transform.position;
            destination.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    private bool ReadyToFire() => Time.time >= _nextFireTime;


    private void Fire()
    {
        float delay = _delay;
        foreach (var powerup in _powerups)
        {
           Debug.Log("Powerup Added");
            delay *= powerup.DelayMultiplier;
        }
        
        _nextFireTime = Time.time + delay;
        BlasterShot shot = Instantiate(_blasterShotPrefab, _blasterInstantiateTransform.position + Vector3.up, transform.rotation);
      //  shot.velocity = transform.forward * 5.0f;
      shot.Launch(transform.forward);

      if (_powerups.Any(t => t.SpreadShot)) // Performance not perfect
      { 
          shot = Instantiate(_blasterShotPrefab, _blasterInstantiateTransform.position, Quaternion.Euler(transform.forward + transform.right) );
          shot.Launch((transform.forward + transform.right));
          
          shot = Instantiate(_blasterShotPrefab, _blasterInstantiateTransform.position, Quaternion.Euler(transform.forward - transform.right) );
          shot.Launch((transform.forward - transform.right));
      }
    }

// Public Methods
    public void AddPowerup(PowerUp powerUp) // => _powerups.Add(powerUp);
    {
        _powerups.Add(powerUp);
        Debug.Log("Powerup added");
    }

    public void RemovePowerup(PowerUp powerUp) // => _powerups.Remove(powerUp);
    {
        _powerups.Remove(powerUp);
        Debug.Log("Powerup removed");
    }

}
