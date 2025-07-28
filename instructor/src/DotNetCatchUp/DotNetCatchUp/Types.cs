
public record Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }


}

public record Employee2(int Id, string Name, decimal Salary);


public class EmployeeRepository
{
    public static Employee2 GetById(int id)
    {
        return new Employee2(id, "Bob Smith", 32_000);
    }
}