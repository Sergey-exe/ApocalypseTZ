using UnityEngine;

public class KeepWorldRotation : MonoBehaviour
{
    [SerializeField] private Vector3 _fixedRotation = Vector3.zero;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(_fixedRotation);
    }
}