using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{

    CharacterController controller;
    float speed = 5f;
    float rotationSpeed = 1f;
    float gravity = -9.81f;
    float jumpSpeed = 8f;
    Vector3 moveDirection  = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Giữ lại giá trị y cũ để gravity tác động liên tục
        float y = moveDirection.y;

        // Tạo vector di chuyển mới, giữ lại trục y
        moveDirection = new Vector3(x, 0, z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y = y;

        // Nếu nhân vật đang đứng trên mặt đất, reset vận tốc rơi
        if (controller.isGrounded)
        {
            moveDirection.y = -1f; // Đảm bảo nhân vật dính mặt đất
                                   // Nếu muốn nhảy, thêm code nhảy ở đây
                                   // if (Input.GetButtonDown("Jump"))
                                   //     moveDirection.y = jumpSpeed;
        }

        // Áp dụng gravity
        moveDirection.y += gravity * Time.deltaTime;

        // Luôn gọi Move, kể cả khi không có input
        controller.Move(moveDirection * Time.deltaTime);




    }
}



//if (Input.GetKey(KeyCode.LeftArrow))
//{
//    transform.Translate(Vector3.left * Time.deltaTime * GM.instance.SpeedGame);
//}
//if (Input.GetKey(KeyCode.RightArrow))
//{
//    transform.Translate(Vector3.right * Time.deltaTime * GM.instance.SpeedGame);
//}
//if (Input.GetKey(KeyCode.UpArrow))
//{
//    transform.Translate(Vector3.forward * Time.deltaTime * GM.instance.SpeedGame);
//}
//if (Input.GetKey(KeyCode.DownArrow))
//{
//    transform.Translate(Vector3.back * Time.deltaTime * GM.instance.SpeedGame);
//}
//if (Input.GetKey(KeyCode.R))
//{
//    transform.Rotate(Vector3.up * 3 * Time.deltaTime * GM.instance.SpeedGame);
//}