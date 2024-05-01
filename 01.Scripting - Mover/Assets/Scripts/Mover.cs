using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    private void Start()
    {
        speed = new Vector3(0, 0, 0.01f);
    }

    // Update is called once per frame
    private void Update()
    {        
        transform.Translate(speed, Space.Self);
    }
}
