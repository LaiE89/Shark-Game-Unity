using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform camPosition;

    void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, camPosition.position, Time.deltaTime * 50);
    }
}
