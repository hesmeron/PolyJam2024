using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] 
    private RoadGenerator _roadGenerator;
    [SerializeField] 
    private Transform _headTransform;
    [SerializeField] 
    private PropellingHand _left;
    [SerializeField]
    private PropellingHand _right;

    private float _horizontalSpeed = 0f;

    private void Update()
    {
        float leftSpeed = _left.Evaluate();
        float rightSpeed = _right.Evaluate();
        float speed = leftSpeed + rightSpeed;
        _horizontalSpeed -= rightSpeed - leftSpeed;
        float radians = (_headTransform.rotation.eulerAngles.x / 180 * Mathf.PI);
        float verticalSpeed = -Mathf.Sin(radians);
        Debug.Log("Unscaled speed " +speed);
        _roadGenerator.AddSpeed(speed * 4f);
        Move(speed, _horizontalSpeed, verticalSpeed);
        _horizontalSpeed = Mathf.Lerp(_horizontalSpeed, 0, Time.deltaTime * 3f);
        _left.Reset();
        _right.Reset();
    }

    private void Move(float speed, float horizontalSpeed, float verticalSpeed)
    {
        float x = (horizontalSpeed * Time.deltaTime) + transform.position.x;
        x = Mathf.Clamp(x, _roadGenerator.MinX(), _roadGenerator.MaxX());
        float y = (verticalSpeed * (Mathf.Abs(_horizontalSpeed+speed))* Time.deltaTime) + transform.position.y;
        y = Mathf.Clamp(y, 0.1f, 4f);
        transform.position = new Vector3(x, y, 0);
    }
}
