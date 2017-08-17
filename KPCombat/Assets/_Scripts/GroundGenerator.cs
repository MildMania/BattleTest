using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public int GroundCount;
    public GameObject GroundPrefab;
    public Transform GroundCarrier;

    public float StartY;

    List<Transform> _groundList;

    private void Awake()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        _groundList = new List<Transform>();

        for (int i = _groundList.Count; i < GroundCount; i++)
        {
            Transform newFloor = CreateNewFloor();

            Transform prevFloor = null;

            if (i > 0)
                prevFloor = _groundList[i - 1];

            newFloor.SetParent(GroundCarrier);

            RepositionFloor(newFloor, i, StartY);

            _groundList.Add(newFloor);
        }
    }

    Transform CreateNewFloor()
    {
        GameObject newFloor = GameObject.Instantiate(GroundPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        return newFloor.transform;
    }

    void RepositionFloor(Transform floor, int index, float startY)
    {
        float floorY = startY + index * 2.0f;

        Vector3 floorPos = new Vector3(0, floorY, 0);

        floorPos.z = floor.parent.transform.position.z;

        floor.position = floorPos;
    }
}
