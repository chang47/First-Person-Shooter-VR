using UnityEngine;

public class FistCollider : MonoBehaviour
{
    private GameObject _player;
    private bool _collidedWithPlayer;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _collidedWithPlayer = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _player)
        {
            _collidedWithPlayer = true;
        }
        print("enter collided with _player");
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == _player)
        {
            _collidedWithPlayer = false;
        }
        print("exit collided with _player");
    }

    public bool IsCollidingWithPlayer()
    {
        return _collidedWithPlayer;
    }
}
