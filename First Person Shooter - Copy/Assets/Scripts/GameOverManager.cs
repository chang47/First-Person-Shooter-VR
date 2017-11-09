using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private Animator _animator;
        
	void Start ()
	{
	    _animator = GetComponent<Animator>();
	}

    public void GameOver()
    {
        _animator.SetBool("IsGameOver", true);
    }
	
}
