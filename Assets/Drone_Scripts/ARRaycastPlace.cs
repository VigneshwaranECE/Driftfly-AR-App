using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARRaycastPlace : MonoBehaviour
{
    public GameObject dronePrefab;          // The drone prefab to instantiate
    private GameObject spawnedDrone;        // To keep track of the spawned drone
    private ARRaycastManager raycastManager; // To detect planes
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // We only want to place the drone if it has not been spawned yet
        if (spawnedDrone == null && raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinBounds))
        {
            // Get the hit pose (position and rotation) where the plane was detected
            Pose hitPose = hits[0].pose;

            // Instantiate the drone prefab at the hit pose
            spawnedDrone = Instantiate(dronePrefab, hitPose.position, hitPose.rotation);
        }
    }
}
