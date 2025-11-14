using UnityEngine;
using UnityEngine.UI; // Необходимо, если вы хотите использовать кнопку UI

public class SimpleCameraSwitcher : MonoBehaviour
{
    [Header("Камеры")]
    // 1. Камера, прикрепленная к игроку (на Capsule)
    public GameObject playerCamera; 
    // 2. Камера для общего вида установки
    public GameObject inspectionCamera;

    void Start()
    {
        // Устанавливаем начальное состояние: активна камера игрока, камера осмотра выключена.
        if (playerCamera != null)
        {
            playerCamera.SetActive(true);
        }
        if (inspectionCamera != null)
        {
            inspectionCamera.SetActive(false);
        }
    }

    // Эта функция вызывается по нажатию на UI-кнопку
    public void ToggleCamera()
    {
        // Проверяем текущее состояние и переключаем
        bool isPlayerCameraActive = playerCamera.activeSelf;

        if (playerCamera != null && inspectionCamera != null)
        {
            // Если активна камера игрока, переключаемся на камеру осмотра
            if (isPlayerCameraActive)
            {
                playerCamera.SetActive(false);
                inspectionCamera.SetActive(true);
                Debug.Log("Переключено на камеру осмотра.");
            }
            // Иначе, возвращаемся к камере игрока
            else
            {
                inspectionCamera.SetActive(false);
                playerCamera.SetActive(true);
                Debug.Log("Переключено на камеру игрока.");
            }
        }
        else
        {
            Debug.LogError("SimpleCameraSwitcher: Не назначены ссылки на одну или обе камеры!");
        }
    }
}