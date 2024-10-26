using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWall : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của điều hòa
    private bool isOnXYPlane = true; // Mặc định là di chuyển trên mặt phẳng XY

    void Update()
    {
        // Lấy input từ bàn phím
        float moveX = 0f; // Khởi tạo trục X
        float moveZ = 0f; // Khởi tạo trục Z

        // Di chuyển theo trục Z: W lên, S xuống
        if (Input.GetKey(KeyCode.W)) // Nhấn phím W
        {
            moveZ = 1f; // Di chuyển lên theo trục Z
        }
        else if (Input.GetKey(KeyCode.S)) // Nhấn phím S
        {
            moveZ = -1f; // Di chuyển xuống theo trục Z
        }

        // Di chuyển theo trục X: D sang trái, A sang phải
        if (Input.GetKey(KeyCode.A)) // Nhấn phím A
        {
            moveX = 1f; // Di chuyển sang phải theo trục X
        }
        else if (Input.GetKey(KeyCode.D)) // Nhấn phím D
        {
            moveX = -1f; // Di chuyển sang trái theo trục X
        }

        // Tạo vector hướng di chuyển dựa trên input từ trục X và Z
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ); // Y cố định

        // Di chuyển điều hòa dựa trên hướng tính toán
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Kiểm tra va chạm với tường bằng raycast
        DetectWallCollision();
    }

    void DetectWallCollision()
    {
        // Tạo một raycast từ vị trí của điều hòa, kiểm tra xem nó có chạm vào tường không
        RaycastHit hit;

        // Raycast từ điều hòa về phía trước (trong hướng di chuyển)
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f))
        {
            // Nếu chạm vào tường, kiểm tra và xoay 90 độ
            if (hit.collider.CompareTag("Wall")) // Gắn tag "Wall" cho các bức tường
            {
                Rotate90Degrees();
            }
        }
    }

    void Rotate90Degrees()
    {
        // Xoay điều hòa 90 độ quanh trục Y (xoay sang bên)
        transform.Rotate(0f, 90f, 0f); // Xoay quanh trục Y
        isOnXYPlane = !isOnXYPlane; // Đổi trạng thái mặt phẳng
    }
}
