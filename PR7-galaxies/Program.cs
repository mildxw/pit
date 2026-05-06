/// <summary>
/// Класс хранит информацию о галактике.
/// </summary>
public class Galaxy
{
    public string Name { get; set; }
    public double MegaLightYears { get; set; }
    public GType GalaxyType { get; set; }
}

/// <summary>
/// Класс определяет тип галактики.
/// </summary>
public class GType
{
    /// <summary>
    /// Создаёт тип галактики по буквенному обозначению.
    /// </summary>
    /// <param name="type">Буквенное обозначение типа галактики.</param>
    public GType(char type)
    {
        switch (type)
        {
            case 'S':
                MyGType = Type.Spiral;
                break;
            case 'E':
                MyGType = Type.Elliptical;
                break;
            case 'I':
                MyGType = Type.Irregular;
                break;
            case 'L':
                MyGType = Type.Lenticular;
                break;
        }
    }

    public object MyGType { get; set; }

    private enum Type
    {
        Spiral,
        Elliptical,
        Irregular,
        Lenticular
    }
}