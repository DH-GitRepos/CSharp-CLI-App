using Entities;
using System.Collections.Generic;

namespace DTOs
{
    public class Command_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public bool Check { get; private set; }

        public object ReturnObject { get; private set; }

        public Dictionary<string, object> ReturnItems { get; private set; }

        public Employee Employee { get; private set; }

        public Item ItemEntity { get; private set; }

        public Dictionary<int, Item> Items { get; private set; }

        public Null_DTO NullDTO { get; private set; }

        public Report_DTO ReportDTO { get; private set; }

        public Item_DTO ItemDTO { get; private set; }

        public Validation_DTO ValidationDTO { get; private set; }

        public Employee_DTO EmployeeDTO { get; private set; }


        public Command_DTO(Employee_DTO employee_DTO)
        {
            this.EmployeeDTO = employee_DTO;
        }
        public Command_DTO(Validation_DTO validation_DTO)
        {
            this.ValidationDTO = validation_DTO;
        }

        public Command_DTO(Item_DTO item_DTO)
        {
            this.ItemDTO = item_DTO;
        }

        public Command_DTO(Report_DTO report_DTO)
        {
            this.ReportDTO = report_DTO;
        }

        public Command_DTO(Null_DTO null_DTO)
        {
            this.NullDTO = null_DTO;
        }

        public Command_DTO(bool check)
        {
            this.Check = check;
        }

        public Command_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public Command_DTO(string status, string message, bool check)
        {
            this.Status = status;
            this.Message = message;
            this.Check = check;
        }

        public Command_DTO(string status, string message, object returnObject)
        {
            this.Status = status;
            this.Message = message;
            this.ReturnObject = returnObject;
        }

        public Command_DTO(string status, string message, Employee employee)
        {
            this.Status = status;
            this.Message = message;
            this.Employee = employee;
        }

        public Command_DTO(string status, string message, Item itemEntity)
        {
            this.Status = status;
            this.Message = message;
            this.ItemEntity = itemEntity;
        }

        public Command_DTO(string status, string message, Dictionary<int, Item> items)
        {
            this.Status = status;
            this.Message = message;
            this.Items = items;
        }

        public Command_DTO(string status, string message, Dictionary<string, object> returnItems)
        {
            this.Status = status;
            this.Message = message;
            this.ReturnItems = returnItems;
        }
    }
}
