using UnityEngine;
/// <summary>
/// Класс движения по заданным точкам
/// </summary>
public class FollowThePath : MonoBehaviour
{
	[HideInInspector]
    public Transform[] path_Points; //Массив точек по которым будем двигаться

	private float speed_Enemy; // Скорость движения, берется из скорости Врага
    private Vector3[] _new_Position; // Массив переопределения движения

	private int cur_Pos=1; // Позиция в движении

	private void Start()
	{
        speed_Enemy = gameObject.GetComponent<Enemy>().speed;
        _new_Position = NewPositionByPath(path_Points);
        transform.position = _new_Position[0];
    }


    private void Update()
    {
        speed_Enemy = gameObject.GetComponent<Enemy>().speed;
        transform.position = Vector3.MoveTowards(transform.position, _new_Position[cur_Pos], speed_Enemy * Time.deltaTime);
        //Если приближаемся к точке, то меняем позицию к другой точки
        if (Vector3.Distance(transform.position, _new_Position[cur_Pos]) < 0.2f)
        {
            cur_Pos++;
            //Если дошли до конца, то уничтожаем объект
            if (Vector3.Distance(transform.position, _new_Position[_new_Position.Length - 1]) < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// Переопределение массива точек в вектора
    /// </summary>
    /// <param name="pathPos">Массив точек</param>
    /// <returns></returns>
    Vector3[] NewPositionByPath(Transform[] pathPos)
    {
        Vector3[] pathPosition = new Vector3[pathPos.Length];
        for (int i = 0; i < pathPos.Length; i++)
        {
            pathPosition[i] = pathPos[i].position;
        }
        return pathPosition;
    }
}
