using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed = 5f;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}