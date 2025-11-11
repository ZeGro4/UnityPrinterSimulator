using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    // Чувствительность мыши
    public float mouseSensitivity = 100f;

    // Ссылка на Transform всего персонажа
    public Transform playerBody;

    // Переменная для хранения вращения по оси X (вертикальное вращение)
    private float xRotation = 0f;

    void Start()
    {
        // Блокируем курсор в центре экрана, чтобы он не мешал
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Получаем ввод от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Уменьшаем xRotation на значение движения мыши по Y
        // (движение вверх-вниз)
        xRotation -= mouseY;

        // Ограничиваем вращение по вертикали, чтобы игрок не мог "сломать шею"
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Применяем вращение к камере по оси X (вертикально)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращаем всего персонажа по оси Y (горизонтально)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
