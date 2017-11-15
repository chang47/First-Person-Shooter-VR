using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 3f;
    public AudioClip[] WalkingClips;
    public float WalkingDelay = 0.3f;

    private Vector3 _movement;
    private Rigidbody _playerRigidBody;
    private AudioSource _walkingAudioSource;
    private float _timer;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _timer = 0f;
        SetupSound();
    }

    private void SetupSound()
    {
        _walkingAudioSource = gameObject.AddComponent<AudioSource>();
        _walkingAudioSource.volume = 0.8f;
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0f || vertical != 0f)
        {
            Move(horizontal, vertical);
        }
    }

    private void Move(float horizontal, float vertical)
    {
        if (_timer >= WalkingDelay)
        {
             PlayRandomFootstep();
            _timer = 0f;
        }
        _movement = (vertical * transform.forward) + (horizontal* transform.right);
        _movement = _movement.normalized * Speed * Time.deltaTime;
        _playerRigidBody.MovePosition(transform.position + _movement);
    }

    private void PlayRandomFootstep()
    {
        int index = Random.Range(0, WalkingClips.Length);
        _walkingAudioSource.clip = WalkingClips[index];
        _walkingAudioSource.Play();
    }
}
