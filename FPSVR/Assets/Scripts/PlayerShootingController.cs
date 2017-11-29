using UnityEngine;
using System.Collections;

public class PlayerShootingController : MonoBehaviour
{
    public float Range = 100;
    public float ShootingDelay = 0.1f;
    public AudioClip ShotSfxClips;
    public Transform GunEndPoint;

    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float _timer;
    private GvrAudioSource _audioSource;
    private Animator _animator;
    private bool _isShooting;

    void Start () {
		_camera = Camera.main;
	    _particle = GetComponentInChildren<ParticleSystem>();
	    Cursor.lockState = CursorLockMode.Locked;
	    _shootableMask = LayerMask.GetMask("Shootable");
	    _timer = 0;
        SetupSound();
        _animator = GetComponent<Animator>();
        _isShooting = false;
    }
	
	void Update ()
	{
	    _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= ShootingDelay)
	    {
            Shoot();
	        if (!_isShooting)
	        {
	            TriggerShootingAnimation();
	        }
	    }
        else if (!Input.GetButton("Fire1"))
	    {
            StopShooting();
	        if (_isShooting)
	        {
	            TriggerShootingAnimation();
            }
	    }
	}

    private void TriggerShootingAnimation()
    {
        _isShooting = !_isShooting;
        _animator.SetTrigger("Shoot");
    }

    private void StopShooting()
    {
        _audioSource.Stop();
        _particle.Stop();
    }

    public void Shoot()
    {
        _timer = 0;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit = new RaycastHit();
        _audioSource.Play();
        _particle.Play();

        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {
            print("hit " + hit.collider.gameObject);
            EnemyMovement enemyMovement = hit.collider.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.KnockBack();
            }
        }
    }

    private void SetupSound()
    {
        _audioSource = gameObject.AddComponent<GvrAudioSource>();
        _audioSource.volume = 0.2f;
        _audioSource.clip = ShotSfxClips;
    }

    public void GameOver()
    {
        _animator.SetTrigger("GameOver");
        StopShooting();
        print("game over called");
    }
}
