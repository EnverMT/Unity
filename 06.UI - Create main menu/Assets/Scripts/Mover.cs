using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private bool _isMoveEnabled = false;

    private void Update()
    {
        if (_isMoveEnabled)
            Move();
    }

    public void EnableMove()
    {
        _isMoveEnabled = true;
    }

    private void Move()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
