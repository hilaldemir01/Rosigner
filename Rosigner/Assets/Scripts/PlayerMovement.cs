using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 6f;
    public GameObject mainCamera;
    public GameObject firstPersonCamera;
    // Start is called before the first frame update
    private void Start()
    {
        Vector3 position = mainCamera.transform.position;
        firstPersonCamera.transform.position = new Vector3(position.x, 0f, position.z);
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
