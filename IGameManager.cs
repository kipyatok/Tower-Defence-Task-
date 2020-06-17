using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Менеджер управления игрой7 Паттерн Одиночка
/// </summary>
public class IGameManager : Singleton<IGameManager>
{
    public GameObject wave; // Объект волны
    //public int maxCountWave;
    [Header("Time Spawn Wave")] // Время создание волн
    [Range(10,20)]public float timeSpawnWave;
    private float time;
    [Header("Count Enemy")] // Кол-во врагов в волне
    public int minCount;
    public int maxCount;
    public UpdateAttributesEnemy updateEnemy { get; private set; } // Для улучшения характирестик
    [Header("UI Panel")]
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        updateEnemy = new UpdateAttributesEnemy();
    }

    private void Update()
    {

        //if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        if (Time.time > time)
        {
            CreateWave();
            time += timeSpawnWave;
        }
    }
    //Функция создания волн, с рандомным количеством врагов
    private void CreateWave()
    {
        int randomCount = Random.Range(minCount, maxCount);
        wave.GetComponent<Wave>().count_in_Wave = randomCount;
        Instantiate(wave);
    }
    /// <summary>
    /// Увелечения характирестик врагов
    /// </summary>
    public void IncrementUpAttributes()
    {
        updateEnemy.Increment();
    }
    /// <summary>
    /// Конец игры
    /// </summary>
    public void EndGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    /// <summary>
    /// Кнопка для рестарта игры
    /// </summary>
    public void BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
