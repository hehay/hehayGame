namespace komal.puremvc
{
    public interface INotification
    {
        string name { get; }
        object body { get; set; }
        string type { get; set; }
        object[] param { get; set; }
        string ToString();
    }
}
