using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public CameraFollow Camera;
    public Transform[] Points;

    private void Start()
    {
        MovePlayerRandom();
    }

    public void MovePlayerRandom()
    {
        transform.gameObject.SetActive(false);
        int pointIndex = Random.Range(0, Points.Length);
        transform.position = Points[pointIndex].position;
        Camera.SetCameraToTarjetPos();
        transform.gameObject.SetActive(true);

    }
}
