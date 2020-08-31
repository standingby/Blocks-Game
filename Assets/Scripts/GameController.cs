using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CubePosition _currentCube = new CubePosition(0, 1, 0);
    public float cubeChangePlaceSpeed = 0.5f;
    public Transform cubeToPlace;

    public GameObject cubeToCreate, allCubes;

    private Rigidbody _allCubesBody;

    private readonly List<Vector3> _allCubePositions = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(1, 0, -1),
        new Vector3(-1, 0, 1)
    };

    private void Start()
    {
        _allCubesBody = allCubes.GetComponent<Rigidbody>();
        StartCoroutine(ShowCubePosition());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
#if !UNITY_EDITOR
if (Input.GetTouch(0).phase != TouchPhase.Began)
    return;
#endif

            GameObject newCube = Instantiate(cubeToCreate, cubeToPlace.position, Quaternion.identity) as GameObject;
            newCube.transform.SetParent(allCubes.transform);
            _currentCube.SetVector(cubeToPlace.position);
            _allCubePositions.Add(_currentCube.GetVector());

            //Update kinematic physics behavior
            _allCubesBody.isKinematic = true;
            _allCubesBody.isKinematic = false;
            
            SpawnPositions();
        }
    }

    IEnumerator ShowCubePosition()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    private void SpawnPositions()
    {
        var positions = new List<Vector3>();

        if (IsPositionEmpty(new Vector3(_currentCube.x + 1, _currentCube.y, _currentCube.z)) &&
            _currentCube.x + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x + 1, _currentCube.y, _currentCube.z));
        if (IsPositionEmpty(new Vector3(_currentCube.x - 1, _currentCube.y, _currentCube.z)) &&
            _currentCube.x - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x - 1, _currentCube.y, _currentCube.z));
        if (IsPositionEmpty(new Vector3(_currentCube.x, _currentCube.y + 1, _currentCube.z)) &&
            _currentCube.y + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x, _currentCube.y + 1, _currentCube.z));
        if (IsPositionEmpty(new Vector3(_currentCube.x, _currentCube.y - 1, _currentCube.z)) &&
            _currentCube.y - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x, _currentCube.y - 1, _currentCube.z));
        if (IsPositionEmpty(new Vector3(_currentCube.x, _currentCube.y, _currentCube.z + 1)) &&
            _currentCube.z + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x, _currentCube.y, _currentCube.z + 1));
        if (IsPositionEmpty(new Vector3(_currentCube.x, _currentCube.y, _currentCube.z - 1)) &&
            _currentCube.z - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(_currentCube.x, _currentCube.y, _currentCube.z - 1));

        cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
    }

    private bool IsPositionEmpty(Vector3 targetPosition)
    {
        if (targetPosition.y == 0)
        {
            return false;
        }

        foreach (var position in _allCubePositions)
        {
            if (position.x == targetPosition.x && position.y == targetPosition.y && position.z == targetPosition.z)
            {
                return false;
            }
        }

        return true;
    }
}

internal struct CubePosition
{
    public int x, y, z;

    public CubePosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }

    public void SetVector(Vector3 pos)
    {
        x = Convert.ToInt32(pos.x);
        y = Convert.ToInt32(pos.y);
        z = Convert.ToInt32(pos.z);
    }
}