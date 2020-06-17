using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс врага
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float health; //Кол-во здоровья
    public float damage; // Кол-во урона
    public int gold; // Сколько золота дается после убийства
    [Range(0,6)]
    public float speed; // Скорость движения

    private float startHealth; //Для отображения слайдера

    [SerializeField] Slider slider_health;

    private void Start()
    {
        slider_health.value = health/health;
        startHealth = health;
    }
    /// <summary>
    /// Функция нанесения урона по врагу
    /// </summary>
    /// <param name="damage">Получаемый урон</param>
    public void SetDamage(int damage)
    {
        health -= damage;
        slider_health.value = health/startHealth;
        if (health <= 0)
        {
            Player.Instance.Gold += gold;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Функция увеличения характиристик врага
    /// </summary>
    /// <param name="up_health">Увеличение здоровья</param>
    /// <param name="up_damage">Увеличение урона</param>
    /// <param name="up_speed">Увеличение скорости</param>
    /// <param name="up_gold">Увеличение золота</param>
    public void UpAttributes(float up_health,float up_damage,float up_speed,int up_gold)
    {
        damage += damage * up_damage;
        health += up_health;
        speed += up_speed;
        gold += up_gold;
    }
}
