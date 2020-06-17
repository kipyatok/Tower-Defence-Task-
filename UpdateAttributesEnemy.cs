/// <summary>
/// Класс для увеличения характеристик
/// </summary>
public class UpdateAttributesEnemy
{
    //Инициализация
    public UpdateAttributesEnemy()
    {
        UpHealth = 0;
        UpDamage = 0;
        UpSpeed = 0;
        UpGold = 0;
    }

    public float UpHealth { get; set; } // Увелю здоровья
    public float UpDamage { get; set; }//Увел. урона
    public float UpSpeed { get; set; }//Увел. скорости
    public int UpGold { get; set; }//Увел. золота

    //Увеличение с стандарными параметрами
    public void Increment()
    {
        UpHealth += 0.4f;
        UpDamage += 0.1f;
        UpSpeed += 0.05f;
        //UpGold += 1;
    }
}
