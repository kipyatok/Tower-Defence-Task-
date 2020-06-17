using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для отображения UI элементов башни
/// </summary>
[RequireComponent(typeof(LineRenderer))] // Для рисования радиуса поражения
public class TowerUI : MonoBehaviour
{
    [Header("Panel Tower UI")]
    public GameObject ui; //Панелька для отображения информации
    [Header("Text Attributes")]
    public Text textInfoTower; // Текст для отображения информации атрибутов башни
    [Header("Button")]
    public GameObject upgradeButton; // Кнопка апгрейда
    private bool down=false; // Для проверки нажамали ли на башню
    private LineRenderer lineRenderer; // Для рисования окружности
    

    private void Awake()
    {
       lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        InfoUpgrade();
        UpdateTextInfoTower();
    }

    //Обновления информации по атрибутам башни
    private void UpdateTextInfoTower()
    {
        Tower tower = gameObject.GetComponent<Tower>();
        textInfoTower.text = "Damage - " + tower.damage.ToString()+"\n" + "Range - " + tower.range.ToString() + "\n" + "Spawn - " + tower.bulletSpawn.ToString();
    }

    //Нажатие на башню
    private void OnMouseDown()
    {
        if(!down)
        {
            ShowPanel();
            UpdateTextInfoTower();
            down = !down;
        }
        else
        {
            HidePanel();
            down = !down;
        }
    }

    /// <summary>
    /// Кнопка для апгрейда башни
    /// </summary>
    public void BtnUpgrade()
    {
        gameObject.GetComponent<Tower>().UpgradeTower();
        int cost = gameObject.GetComponent<Tower>().cost;
        InfoUpgrade();
        Player.Instance.Gold -= cost;
        DrawCircle();
    }
    //Отображении информации по атрибутам апгрейда
    private void InfoUpgrade()
    {
        Text textBtn = upgradeButton.GetComponentInChildren<Text>();
        int cost = gameObject.GetComponent<Tower>().cost;
        bool isUpgrade = gameObject.GetComponent<Tower>().isUpgrade;
        if (cost <= Player.Instance.Gold && !isUpgrade) // Если есть деньги и еще не улучшена
        {
            textBtn.text = "Upgrade \n $" + cost.ToString();
            upgradeButton.GetComponent<Image>().color = Color.white;
            upgradeButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            if (isUpgrade) // Если уже улучшена
            {
                textBtn.text = "Already Upgrade";
                upgradeButton.GetComponent<Image>().color = Color.grey;
                upgradeButton.GetComponent<Button>().enabled = false;
            }
            else // Если не хватает денег и не улучшена
            {
                textBtn.text = "Upgrade \n $" + cost.ToString();
                upgradeButton.GetComponent<Image>().color = Color.grey;
                upgradeButton.GetComponent<Button>().enabled = false;
            }
        }
    }
    //Открытие панели UI по башне
    private void ShowPanel()
    {
        ui.SetActive(true);
        DrawCircle();
        InfoUpgrade();
    }
    //Скрытие панели по башне
    private void HidePanel()
    {
        ui.SetActive(false);
        lineRenderer.enabled = false;
    }

    //Рисование круга с диапозоном поражения
    private void DrawCircle()
    {
        lineRenderer.enabled = true;
        float radius = gameObject.GetComponent<Tower>().range;
        int numSegments = 120;
        Color c1 = Color.red;
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c1;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount= numSegments;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;
            Vector3 pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
        
}



