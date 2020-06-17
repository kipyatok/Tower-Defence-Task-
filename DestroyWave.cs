using UnityEngine;
/// <summary>
/// Класс для уничтожения родительского объекта врагов.
/// </summary>
public class DestroyWave : MonoBehaviour
{
    private void Update()
    {
        if (transform.childCount == 0) //Если все враги в данной волне уничтоженны то удаляем волну
            Destroy(gameObject);
    }
}
