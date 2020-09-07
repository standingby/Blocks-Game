using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    public GameObject restartButton;
    private bool _collisionSet;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Cube") && _collisionSet) return;

        for (var i = other.transform.childCount - 1; i >= 0; i--)
        {
            var child = other.transform.GetChild(i);
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
            child.SetParent(null);
        }

        restartButton.SetActive(true);
        Camera.main.transform.position -= new Vector3(0, 0, 3f);
        Destroy(other.gameObject);
        _collisionSet = true;
    }
}