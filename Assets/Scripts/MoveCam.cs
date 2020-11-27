using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public float Speed = 0.05f;

    private PlayerController _player;


    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!_player.IsDie)
        {
            transform.position += new Vector3(0, 0, Speed);
        }
    }
}
