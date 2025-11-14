using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Используется для Button, если скрипт будет на кнопке

public class SlidingPanel : MonoBehaviour
{
    [Header("Настройки позиции")]
    // Задайте в Инспекторе: конечная позиция (видимая)
    public Vector2 shownPosition = Vector2.zero; 
    // Задайте в Инспекторе: начальная позиция (скрытая)
    public Vector2 hiddenPosition = new Vector2(250f, 0f); 

    [Header("Настройки скорости")]
    // Скорость (длительность) анимации
    public float slideDuration = 0.3f; 

    // Текущее состояние панели
    private bool isPanelShown = false;
    private RectTransform rectTransform;
    private Coroutine currentSlideRoutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Убедимся, что панель начинается со скрытой позиции
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = hiddenPosition;
        }
        else
        {
            Debug.LogError("SlidingPanel требует компонента RectTransform!");
        }
    }

    // Главная функция, вызываемая кнопкой
    public void TogglePanel()
    {
        // Определяем новую цель
        Vector2 targetPosition = isPanelShown ? hiddenPosition : shownPosition;

        // Прекращаем предыдущую анимацию, если она идет
        if (currentSlideRoutine != null)
        {
            StopCoroutine(currentSlideRoutine);
        }

        // Запускаем корутину для плавного движения
        currentSlideRoutine = StartCoroutine(SlideToPosition(targetPosition));

        // Инвертируем состояние
        isPanelShown = !isPanelShown;
    }

    // Корутина для плавного перемещения
    private IEnumerator SlideToPosition(Vector2 targetPos)
    {
        float elapsedTime = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            // Используем Lerp для плавного перехода
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / slideDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующего кадра
        }

        // Убеждаемся, что мы точно достигли конечной точки
        rectTransform.anchoredPosition = targetPos;
        currentSlideRoutine = null;
    }
}