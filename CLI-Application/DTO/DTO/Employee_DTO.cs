using Entities;

namespace DTOs
{
    public class Employee_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public Employee Employee { get; private set; }

        public Employee_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public Employee_DTO(string status, string message, Employee employee)
        {
            this.Status = status;
            this.Message = message;
            this.Employee = employee;
        }
    }
}
