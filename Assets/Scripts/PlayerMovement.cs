using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private FloatingJoystick joystick;

    private Vector3 movement;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        Vector2 keyboard = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if(keyboard != Vector2.zero)
            movement = new Vector3(keyboard.x, 0, keyboard.y);
        /*if (movement != Vector3.zero)
        {
            // Плавный поворот в сторону движения
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }*/
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        if (movement != Vector3.zero)
        {
            rb.velocity = movement * Speed;
            //rb.Move(rb.position + movement, rb.rotation);
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }
        else
        {
            rb.velocity = Vector3.down * Speed;
        }
    }


}
