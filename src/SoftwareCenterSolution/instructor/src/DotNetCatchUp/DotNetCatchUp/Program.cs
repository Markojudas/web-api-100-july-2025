
Console.WriteLine("Hello, World!");


var carl = new Employee();

carl.Name = "Carl Smith";
carl.Salary = 100_000M;

Console.WriteLine($"Salary is {carl.Salary}");
Console.WriteLine(carl.Name.ToUpper());


var carl2 = new Employee();

carl2.Name = "Carl Smith";
carl2.Salary = 100_000;

if(carl == carl2)
{
    Console.WriteLine("The carls are the same");

}
else
{
    Console.WriteLine("The carls are different");

}
Console.WriteLine(carl);
Console.WriteLine(carl2);


var bob = EmployeeRepository.GetById(13);

Console.WriteLine(bob.Name);

//bob.Salary = bob.Salary * 2;

Console.WriteLine(bob.Salary);


var bobUpdated = bob with { Salary = 8000 };

Console.WriteLine("After the Update");
Console.WriteLine(bob);
Console.WriteLine(bobUpdated);