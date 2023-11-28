using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MummyController : MonoBehaviour
{
     public static event Action<MummyController> OnMechDestroyed;   
    [SerializeField] private float _attackRange = 1.0f;
    [SerializeField] private int _health = 2;

    private int _currentHealth;
    
   private NavMeshAgent _navMeshAgent;
   private Animator _animator;

   private bool Alive => _currentHealth > 0;

    private void Awake()
    {
        _currentHealth = _health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.enabled = false;

    }

    void Update()
    {
        if (!Alive)
            return;
        
        var player = FindObjectOfType<PlayerMovement>();
       if (_navMeshAgent.enabled)
        _navMeshAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
           Attack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var blasterShot = collision.collider.GetComponent<BlasterShot>();
        if (blasterShot != null)
        {
            _currentHealth--;
            if (_currentHealth <= 0)
                Die();
            else
                TakeHit();
        }
    }

    private void TakeHit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Hit");
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Death");
        OnMechDestroyed?.Invoke(this);
        Destroy(gameObject, 5.0f);
        
    }

    private void Attack()
    {
        if (Alive)
        {
            _animator.SetTrigger("Attack");
            _navMeshAgent.enabled = false;
        }
    }

    public void StartWalking()
    {
        _navMeshAgent.enabled = true;
        _animator.SetBool("Moving", true);
    }

    public void ScreamAnimation()
    {
        _animator.SetTrigger("Scream");
    }

    #region Animation Callbacks

    //Animation Callback region

   
    void AttackComplete()
    {
        
    }

    void AttackHit()
    {
        Debug.Log("Killed player");
        SceneManager.LoadScene(0);
    }

    void HitComplete()
    {
        if (Alive)
            _navMeshAgent.enabled = true;
    }

    #endregion Animation Callbacks
    
    
    //private bool InAttackRange() => Vector3.Distance(transform.position,)
   
}
