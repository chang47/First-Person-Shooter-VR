using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public FistCollider LeftFist;
    public FistCollider RightFist;
    public AudioClip[] AttackSfxClips;

    private Animator _animator;
    private GameObject _player;
    private AudioSource _audioSource;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        SetupSound();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _animator.SetBool("IsNearPlayer", true);
        }
        print("enter trigger with _player");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _animator.SetBool("IsNearPlayer", false);
        }
        print("exit trigger with _player");
    }

    private void Attack()
    {
        print("attack called");
        if (LeftFist.IsCollidingWithPlayer() || RightFist.IsCollidingWithPlayer())
        {
            print("enemy attacked the player");
            PlayRandomHit();
            _player.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }

    private void SetupSound()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.2f;
    }

    private void PlayRandomHit()
    {
        int index = Random.Range(0, AttackSfxClips.Length);
        _audioSource.clip = AttackSfxClips[index];
        _audioSource.Play();
    }
}