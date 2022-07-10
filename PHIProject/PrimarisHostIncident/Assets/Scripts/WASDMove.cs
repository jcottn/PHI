using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class WASDMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector3.forward * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.left * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector3.back * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector3.right * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _speed * Time.deltaTime);
        }
    }
}
