using PinePie.SimpleJoystick;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private JoystickController _joystickController;

    private bool _isActivate;

    private void Update()
    {
        if (!_isActivate)
            return;

        Vector2 input = _joystickController.InputDirection;
        
        transform.position += _moveSpeed * Time.deltaTime * (Vector3)input;
        
        if (Mathf.Abs(input.x) > 0.1f)
        {
            float targetX = input.x >= 0 ? 0f : 180f; 
            transform.rotation = Quaternion.Euler(0f, targetX, 0f);
        }
    }

    public void Activate()
    {
        _isActivate = true;
    }
}