using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public static event Action<Enemy> Died;
    [SerializeField] float attackRange = 3f;
    [SerializeField] public float _health = 200;
    [SerializeField] public float _currentHealth;
    [SerializeField] ParticleSystem deathSFX;
    public NavMeshAgent _navMeshAgent;
    Animator _anime;
    bool IsAlive => _currentHealth > 0;


    void Awake()
    {
        instance = this;
        _currentHealth = _health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anime = GetComponent<Animator>();
        _navMeshAgent.enabled = true;
    }

    internal void UpdateStatsAfterReEnebling()
    {
        _currentHealth = _health;
        _navMeshAgent.enabled = true;
        GetComponent<Collider>().enabled = true;
    }
    internal void TakeHit(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    internal void StartWalking()
    {
        _navMeshAgent.enabled = true;
    }

    void Die()
    {
        Instantiate(deathSFX, this.transform.position, Quaternion.identity);
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        _anime.SetTrigger("Died");
        Died?.Invoke(this);

        EnemyPool.Instance.Return(this);
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!IsAlive) return;

        FollowPlayer();
    }
    void FollowPlayer()
    {
        var player = FindObjectOfType<PlayerController>();

        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(player.transform.position);
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            Attack();
        }
    }
    void Attack()
    {
        _anime.SetTrigger("Attack");
        _navMeshAgent.enabled = false;
    }
    #region  animation callbacks
    void AttackComplete()
    {
        if (IsAlive)
        {
            _navMeshAgent.enabled = true;
        }
    }
    void AttackHit()
    {
        PlayerHealth.playerStartHealth--;
    }
    void HitComplete()
    {
        if (IsAlive)
        {
            _navMeshAgent.enabled = true;
        }
    }
    #endregion
}
