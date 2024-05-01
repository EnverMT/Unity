using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * rotateSpeed * Vector3.up * Time.deltaTime);
    }

    private void Move()
    {
        float direction = Input.GetAxis(Vertical);
        float distance = direction * moveSpeed * Time.deltaTime;

        transform.Translate(distance * Vector3.forward);
    }
}
