using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс игрока, паттерн одиночка
/// </summary>
public class Player : Singleton<Player>
{
    private int gold; // Кол-во золота
    public int Gold { get { return gold; } set { gold = value; } } //Для чтения других классов золота
    [SerializeField]private float health; // Здороье башни
    public float Health { get { return health; }  } //Для чтения других классов здоровья

    private float startHealth; // Для слайдера
    [Header("Slider")]
    public Slider sliderHealth; //Слайдер с отображением здоровья
    public Text textHealth; // Текст здоровья
    [Header("Gold")] 
    [SerializeField]private Text textGold; //Текст золота

    private void Start()
    {
        startHealth = health;
        UpdateSlider();
    }

    private void Update()
    {
        textGold.text = gold.ToString();
    }
    /// <summary>
    /// Получения урона от врагов
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(float damage)
    {
        health -= damage;
        UpdateSlider();
        if(health<=0) // Если меньше 0 , то проиграли и вызов конца игры
        {
            IGameManager.Instance.EndGame();
        }
    }

    //Обновление информации по здоровью
    private void UpdateSlider()
    {
        sliderHealth.value = health / startHealth;
        textHealth.text = Mathf.Round(health) + "/" + startHealth;
    }

}
