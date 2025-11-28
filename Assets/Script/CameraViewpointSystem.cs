using UnityEngine;
using System.Collections;

public class CameraViewpointSystem : MonoBehaviour
{
    [System.Serializable]
    public class Viewpoint
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public float fieldOfView = 60f;
        public bool isConfigured = false;
    }
    
    public Viewpoint[] viewpoints;
    public Viewpoint defaultViewpoint;
    public float moveDuration = 1f;
    
    private Camera cam;
    private Coroutine currentCoroutine;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log("CameraViewpointSystem запущен. Всего viewpoints: " + viewpoints.Length);
        
        // Автопроверка конфигурации
        for (int i = 0; i < viewpoints.Length; i++)
        {
            viewpoints[i].isConfigured = viewpoints[i].position != Vector3.zero;
            Debug.Log("Viewpoint " + i + ": " + viewpoints[i].name + " - настроен: " + viewpoints[i].isConfigured + " позиция: " + viewpoints[i].position);
        }
    }
    
    public void MoveToViewpoint(int viewpointIndex)
    {
        Debug.Log("=== НАЖАТА КНОПКА ===");
        Debug.Log("Получен индекс: " + viewpointIndex);
        Debug.Log("Всего viewpoints: " + viewpoints.Length);
        
        // Проверяем валидность индекса
        if (viewpointIndex < 0 || viewpointIndex >= viewpoints.Length)
        {
            Debug.LogError("Индекс " + viewpointIndex + " вне диапазона! Допустимо: 0-" + (viewpoints.Length - 1));
            return;
        }
        
        // Проверяем настройку viewpoint
        if (!viewpoints[viewpointIndex].isConfigured)
        {
            Debug.LogError("Viewpoint " + viewpointIndex + " не настроен! Имя: " + viewpoints[viewpointIndex].name);
            return;
        }
        
        Debug.Log("Перемещаемся к: " + viewpoints[viewpointIndex].name + " позиция: " + viewpoints[viewpointIndex].position);
        
        if (currentCoroutine != null)
        {
            Debug.Log("Останавливаем предыдущую корутину");
            StopCoroutine(currentCoroutine);
        }
        
        currentCoroutine = StartCoroutine(MoveToPosition(viewpoints[viewpointIndex]));
        Debug.Log("Корутина запущена");
    }
    
    private IEnumerator MoveToPosition(Viewpoint target)
    {
        Debug.Log("Начало перемещения к: " + target.name);
        
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float startFOV = cam.fieldOfView;
        
        Debug.Log("Стартовая позиция: " + startPos);
        Debug.Log("Целевая позиция: " + target.position);
        
        float time = 0f;
        
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;
            
            transform.position = Vector3.Lerp(startPos, target.position, t);
            transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(target.rotation), t);
            cam.fieldOfView = Mathf.Lerp(startFOV, target.fieldOfView, t);
            
            yield return null;
        }
        
        // Финальная точная позиция
        transform.position = target.position;
        transform.rotation = Quaternion.Euler(target.rotation);
        cam.fieldOfView = target.fieldOfView;
        
        Debug.Log("Перемещение завершено: " + target.name + " текущая позиция: " + transform.position);
    }
    
    public void ReturnToDefaultView()
    {
        Debug.Log("Возврат к общему виду");
        
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        
        currentCoroutine = StartCoroutine(MoveToPosition(defaultViewpoint));
    }
    
    // ТЕСТОВЫЙ МЕТОД - мгновенное перемещение
    public void TestInstantMove(int viewpointIndex)
    {
        if (viewpointIndex >= 0 && viewpointIndex < viewpoints.Length)
        {
            Debug.Log("ТЕСТ: Мгновенное перемещение к " + viewpoints[viewpointIndex].name);
            transform.position = viewpoints[viewpointIndex].position;
            transform.rotation = Quaternion.Euler(viewpoints[viewpointIndex].rotation);
            cam.fieldOfView = viewpoints[viewpointIndex].fieldOfView;
        }
    }
}