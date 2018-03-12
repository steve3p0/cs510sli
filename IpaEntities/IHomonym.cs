namespace IpaEntities
{
    public interface IHomonym
    {
        bool isOov { get; set; }
        string phonentic { get; set; }
        string pos { get; set; }
    }
}