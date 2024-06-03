namespace Entities
{
    public class Employee
    {
        public string EmpName { get; }

        public Employee(string empName)
        {
            EmpName = empName;
        }

        public override string ToString()
        {
            return EmpName;
        }
    }
}
