using UnityEngine;
using System.Collections;

public class PlayerShootingController : MonoBehaviour
{
    public float Range = 100;
    public float ShootingDelay = 0.1f;
    public AudioClip ShotSfxClips;
    public Transform GunEndPoint;
    public float MaxAmmo = 10f;

    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float _timer;
    private AudioSource _audioSource;
    private Animator _animator;
    private bool _isShooting;
    private bool _isReloading;
    private LineRenderer _lineRenderer;
    private float _currentAmmo;
    private ScreenManager _screenManager;


    void Start () {
		_camera = Camera.main;
	    _particle = GetComponentInChildren<ParticleSystem>();
	    Cursor.lockState = CursorLockMode.Locked;
	    _shootableMask = LayerMask.GetMask("Shootable");
	    _timer = 0;
        SetupSound();
        _animator = GetComponent<Animator>();
        _isShooting = false;
        _isReloading = false;
        _lineRenderer = GetComponent<LineRenderer>();
        _currentAmmo = MaxAmmo;
        _screenManager = GameObject.FindWithTag("ScreenManager").GetComponent<ScreenManager>();
    }
	
	void Update ()
	{
	    _timer += Time.deltaTime;

	    // Create a vector at the center of our camera's viewport
	    Vector3 lineOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

	    // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
	    Debug.DrawRay(lineOrigin, _camera.transform.forward * Range, Color.green);

        if (Input.GetMouseButton(0) && _timer >= ShootingDelay && !_isReloading && _currentAmmo > 0)
	    {
            Shoot();
	        if (!_isShooting)
	        {
	            TriggerShootingAnimation();
	        }
	    }
        else if (!Input.GetMouseButton(0) || _currentAmmo <= 0)
	    {
            StopShooting();
	        if (_isShooting)
	        {
	            TriggerShootingAnimation();
            }
	    }

	    if (Input.GetKeyDown(KeyCode.R))
	    {
	        StartReloading();
	    }
	}

    private void StartReloading()
    {
        _animator.SetTrigger("DoReload");
        StopShooting();
        _isShooting = false;
        _isReloading = true;
    }

    private void TriggerShootingAnimation()
    {
        _isShooting = !_isShooting;
        _animator.SetTrigger("Shoot");
        print("trigger shoot animation");
    }

    private void StopShooting()
    {
        _audioSource.Stop();
        _particle.Stop();
    }

    private void Shoot()
    {

        print("shoot called");
        _timer = 0;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        _audioSource.Play();
        _particle.Play();
        _currentAmmo--;
        _screenManager.UpdateAmmoText(_currentAmmo, MaxAmmo);

        _lineRenderer.SetPosition(0, GunEndPoint.position);
        StartCoroutine(FireLine());

        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {
            print("hit " + hit.collider.gameObject);
            _lineRenderer.SetPosition(1, hit.point);
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            EnemyMovement enemyMovement = hit.collider.GetComponent<EnemyMovement>();
            print(health);
            if (enemyMovement != null)
            {
                enemyMovement.KnockBack();
            }
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, ray.GetPoint(Range));
        }
    }

    private IEnumerator FireLine()
    {
        _lineRenderer.enabled = true;

        yield return ShootingDelay - 0.05f;

        _lineRenderer.enabled = false;
    }

    // called from the animation finished
    public void ReloadFinish()
    {
        _isReloading = false;
        _currentAmmo = MaxAmmo;
        _screenManager.UpdateAmmoText(_currentAmmo, MaxAmmo);
    }

    private void SetupSound()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
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
