using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Set position using transform.position.  We can set individual
        // coordinates, or do all three with a new Vector3 call.
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {

        // get position information
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // set next position
        transform.Translate(direction * _speed * Time.deltaTime);

        //check screen boundaries
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (Mathf.Abs(transform.position.x) >= 11.5f) { transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0); }

    }
}
