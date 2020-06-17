using UnityEngine;
/// <summary>
/// Основной класс башни. Можно наследоваться от него при создании других типов башен
/// </summary>
public class Tower : MonoBehaviour
{
    public int damage; // Урон наносимой башней
    [Range(0,4)]
    public float bulletSpawn; // Скорострельность башни
    private float timeShoot; // Время выстрела
    public GameObject bullet; // Игровой объект пули
    private Transform target; // Объект врага для захвата
    public int cost; // Стоимость улучшения
    [HideInInspector]public bool isUpgrade; // Проверка на улучшение

    [Range(0,5)]
    public float range = 1.5f; // Диапозон выстрела

    // Функция для проверки зашел ли враг в область выстрела
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            //Определяем ближайшего врага
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            //Если враг еще жив и в диапозоне то делаем выстрел
            target = nearestEnemy.transform;
            MakeShoot();
        }
        else
        {
            target = null;
        }

    }
    //Функция улучшения башни
    public void UpgradeTower()
    {
        damage += 1;
        range += 0.5f;
        bulletSpawn -= 0.2f;
        isUpgrade = true;
    }

    private void Update()
    {
        UpdateTarget();
    }
    //выстрел с учетом скорострельности
    private void MakeShoot()
    {
        if (Time.time > timeShoot)
        {
            CreateBullet();
            timeShoot =Time.time+ bulletSpawn ;
        }
    }
    //Создание пули
    private void CreateBullet()
    {
        GameObject bulletStart = Instantiate(bullet, transform.position, Quaternion.Euler(Vector3.zero));
        Bullet _bullet = bulletStart.GetComponent<Bullet>();
        if (_bullet != null)
        {
            _bullet.GetTarget(target); // Стреляем по ближайшем врагу которого определили
            _bullet.GetDamage(damage); // Передача урона башни в пулю, для нанесения урона врагу
        }
    }


    //Рисуем диапозон выстрела
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
