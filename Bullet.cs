using UnityEngine;
/// <summary>
/// Класс пули
/// </summary>
public class Bullet : MonoBehaviour
{
    private int damage; // Урон от башни по врагам
    private Transform target; // По какой цели стреляем
     
    private float speed = 5f; // Скорость движения

    /// <summary>
    /// Цель по которой будем стрелять
    /// </summary>
    /// <param name="_target">Объект врага</param>
    public void GetTarget(Transform _target)
    {
        target = _target;
    }
    /// <summary>
    /// Присваение урона от башни к урону пули
    /// </summary>
    /// <param name="_damage">Урон</param>
    public void GetDamage(int _damage)
    {
        damage = _damage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
    //Если пуля достигла врага то наносим ему урон
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().SetDamage(damage);
            Destroy(gameObject);
        }
    }
}
