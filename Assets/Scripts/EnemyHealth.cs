using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100;
    public AudioClip[] HitSfxClips;
    public float HitSoundDelay = 0.5f;

    private SpawnManager _spawnManager;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _hitTime;

    void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _animator = GetComponent<Animator>();
        _hitTime = 0f;
        SetupSound();
    }

    void Update()
    {
        _hitTime += Time.deltaTime;
    }
	
    public void TakeDamage(float damage)
    {
        if (Health <= 0)
        {
            return;
        }

        Health -= damage;
        if (_hitTime > HitSoundDelay)
        {
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
        _audioSource = gameObject.AddComponent<AudioSource>();
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
}
