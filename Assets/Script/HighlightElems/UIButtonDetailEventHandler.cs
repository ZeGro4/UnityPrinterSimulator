using UnityEngine;
using UnityEngine.EventSystems; // Обязательно для обработки событий указателя
using UnityEngine.UI; // Для работы с UI (хотя Button тут не используется напрямую)

// Класс теперь реализует три интерфейса
public class UIButtonDetailEventHandler : MonoBehaviour, 
                                        IPointerEnterHandler, 
                                        IPointerExitHandler,
                                        IPointerClickHandler
{
    [Header("Связи с 3D-Деталями и Аудио")]
    // Сюда перетаскиваем все 3D-детали, которые должна подсвечивать эта кнопка
    public DetailHighlighter[] targetDetailsHighlighters; 
    
    // Аудиоклип описания, который будет воспроизводиться по клику
    public AudioClip audioDescription; 

    private AudioSource audioManager;

    void Start()
    {
        // Находим AudioSource (предполагаем, что он один в сцене или прикреплен к менеджеру)
        audioManager = FindObjectOfType<AudioSource>();
        
        if (targetDetailsHighlighters == null || targetDetailsHighlighters.Length == 0)
        {
            Debug.LogWarning("На кнопке " + gameObject.name + " не назначены целевые детали.");
        }
        if (audioManager == null)
        {
             Debug.LogError("В сцене не найден AudioSource для воспроизведения аудио.");
        }
    }

    // === 1. Обработка НАВЕДЕНИЯ (ПОДСВЕТКА) ===
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Включаем подсветку для всех связанных деталей
        if (targetDetailsHighlighters != null)
        {
            foreach (DetailHighlighter detail in targetDetailsHighlighters)
            {
                if (detail != null)
                {
                    detail.HighlightOn();
                }
            }
        }
    }

    // === 2. Обработка УХОДА КУРСОРА (ВЫКЛЮЧЕНИЕ ПОДСВЕТКИ) ===
    public void OnPointerExit(PointerEventData eventData)
    {
        // Выключаем подсветку для всех связанных деталей
        if (targetDetailsHighlighters != null)
        {
            foreach (DetailHighlighter detail in targetDetailsHighlighters)
            {
                if (detail != null)
                {
                    detail.HighlightOff();
                }
            }
        }
    }

    // === 3. Обработка НАЖАТИЯ (ВОСПРОИЗВЕДЕНИЕ АУДИО) ===
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioManager != null && audioDescription != null)
        {
            audioManager.Stop();
            audioManager.clip = audioDescription;
            audioManager.Play();
            Debug.Log("Воспроизведение аудио: " + audioDescription.name);
        }
    }
}