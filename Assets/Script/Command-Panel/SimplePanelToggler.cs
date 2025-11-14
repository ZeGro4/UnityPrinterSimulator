using UnityEngine;

public class SimplePanelToggler : MonoBehaviour
{
    private GameObject targetPanel;

    [Header("Скрывать при старте игры?")]
    // Если эта галочка стоит, панель скроется при запуске игры.
    public bool hideOnStart = true; 

    void Start()
    {
        targetPanel = this.gameObject;
        
        // 1. Проверяем флаг 'hideOnStart'
        if (hideOnStart)
        {
            // 2. Скрываем панель, если она сейчас активна.
            if (targetPanel.activeSelf)
            {
                targetPanel.SetActive(false);
            }
        }
        
        // Если hideOnStart = false, панель остается в том состоянии,
        // в котором вы оставили ее в Инспекторе.
    }

    // Эта функция переключает состояние активности (видимости) панели
    // Она вызывается кнопкой
    public void ToggleVisibility()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(!targetPanel.activeSelf);
        }
    }
}