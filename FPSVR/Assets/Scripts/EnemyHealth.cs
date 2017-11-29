using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100;
    public AudioClip[] HitSfxClips;
    public float HitSoundDelay = 0.1f;

    private SpawnManager _spawnManager;
    private Animator _animator;
    private GvrAudioSource _audioSource;
    private float _hitTime;
    private Boolean _isEnter;


    void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _animator = GetComponent<Animator>();
        _hitTime = 0f;
        _isEnter = false;
        SetupSound();
    }

    void Update()
    {
        _hitTime += Time.deltaTime;
        if (Input.GetButton("Fire1") && _isEnter)
        {
            TakeDamage(1);
        }
    }
	
    private void TakeDamage(float damage)
    {
        if (Health <= 0)
        {
            return;
        }
        
        if (_hitTime > HitSoundDelay)
        {
            Health -= damage;
            PlayRandomHit();
            _hitTime = 0;
        }

        if (Health <= 0)
        {
            Death();
        } 
    }

    private void SetupSound()
    {
        _audioSource = gameObject.AddComponent<GvrAudioSource>();
        _audioSource.volume = 0.2f;
    }

    private void PlayRandomHit()
    {
        int index = Random.Range(0, HitSfxClips.Length);
        _audioSource.clip = HitSfxClips[index];
        _audioSource.Play();
    }

    private void Death()
    {
        _animator.SetTrigger("Death");
        _spawnManager.EnemyDefeated();
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
    }

    public void HealthEnter()
    {
        _isEnter = true;
        print("enter");
    }

    public void HealthExit()
    {
        _isEnter = false;
        print("exit");
    }
}
