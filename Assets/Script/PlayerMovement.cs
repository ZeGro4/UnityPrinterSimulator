using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Ссылка на компонент CharacterController
    private CharacterController controller;

    // Скорость передвижения персонажа
    public float speed = 12f;

    // Гравитация
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        // Получаем компонент CharacterController при старте
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        // Проверяем, стоит ли персонаж на земле
        if (controller.isGrounded && velocity.y < 0)
        {
            // Сбрасываем вертикальную скорость, чтобы персонаж не "утонул" под землю
            velocity.y = -2f;
        }

        // Получаем ввод с клавиатуры (оси "Horizontal" и "Vertical")
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        // Создаем вектор движения на основе направления, куда смотрит персонаж
        Vector3 move = transform.right * x + transform.forward * z;

        // Перемещаем персонажа с помощью CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Применяем гравитацию
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
