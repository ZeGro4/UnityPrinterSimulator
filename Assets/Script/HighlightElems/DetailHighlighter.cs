using UnityEngine;

public class DetailHighlighter : MonoBehaviour
{
    private Renderer detailRenderer;
    private Color originalColor;

    // Цвет для подсветки, который удобно настроить в Инспекторе
    public Color highlightColor = new Color(1f, 1f, 0f); // Желтый в диапазоне 0-1

    private void Start()
    {
        // Получаем Renderer один раз
        detailRenderer = GetComponent<Renderer>();
        
        if (detailRenderer != null)
        {
            // Сохраняем оригинальный цвет. .material создает копию!
            originalColor = detailRenderer.material.color;
        }
    }

    // Включает подсветку
    public void HighlightOn()
    {
        if (detailRenderer != null)
        {
            // Устанавливаем цвет из Инспектора
            detailRenderer.material.color = highlightColor;
            
            // ИСПРАВЛЕНИЕ: Используем конкатенацию (+)
            Debug.Log("Подсветка " + gameObject.name + " включена.");
        }
    }

    // Выключает подсветку
    public void HighlightOff()
    {
        if (detailRenderer != null)
        {
            // Возвращаем оригинальный цвет
            detailRenderer.material.color = originalColor;
            
            // ИСПРАВЛЕНИЕ: Используем конкатенацию (+)
            Debug.Log("Подсветка " + gameObject.name + " выключена.");
        }
    }
}