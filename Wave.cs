using System.Collections;
using UnityEngine;
/// <summary>
/// Класс волны
/// </summary>
public class Wave : MonoBehaviour
{
    [Space]
    public GameObject obj_Enemy; // Объект врага
    [HideInInspector]
    public int count_in_Wave; // кол-во врагов в волне
    [Range(0,2)]
    public float time_Spawn; // Время появления

    private UpdateAttributesEnemy _update = new UpdateAttributesEnemy(); //Переменная для улучшения характеристик врага

    public Transform[] path_Points; // Массив точке, точки находятся в дочкам волны

    private FollowThePath follow_Component; // Компанент движения
    private GameObject parentEnemyWave; // Объект чтоб все враги создавались дочками (Для удобного отображения в редакторе)

    private void Start()
    {
        parentEnemyWave = new GameObject();
        parentEnemyWave.AddComponent<DestroyWave>(); // Добавляем компанент, для уничтожения
        parentEnemyWave.name = "Wave " + count_in_Wave.ToString();
        StartCoroutine(CreateEnemyWave());
    }
    /// <summary>
    /// Субпрограмма для создания врагов в волне
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateEnemyWave()
    {
        Debug.Log(count_in_Wave);
        for (int i = 0; i < count_in_Wave; i++)
        {
            GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);
            new_enemy.transform.SetParent(parentEnemyWave.transform);
            _update = IGameManager.Instance.updateEnemy; // Улучшаем характеристики
            new_enemy.GetComponent<Enemy>().UpAttributes(_update.UpHealth, _update.UpDamage, _update.UpSpeed, _update.UpGold);
            follow_Component = new_enemy.GetComponent<FollowThePath>();
            follow_Component.path_Points = path_Points;
            yield return new WaitForSeconds(time_Spawn);
        }
        IGameManager.Instance.IncrementUpAttributes(); // Увеличваем характеристики
        Destroy(gameObject); // Уничтожаем волну (чтоб не захламлят редактор)
    }

    

}
