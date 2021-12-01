namespace Zamagon.Model;

public class Employee
{
    public int ID { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TimeCard> TimeCards { get; set; }
}
