
using UnityEngine;
/// <summary>
/// Класс для условно замка до которого должны дойти враги
/// </summary>
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если враг зашел в коллайдер то отнимаем здоровье у игрока
        if (collision.tag == "Enemy")
        {
            Player.Instance.SetDamage(collision.GetComponent<Enemy>().damage);
        }
    }
}
