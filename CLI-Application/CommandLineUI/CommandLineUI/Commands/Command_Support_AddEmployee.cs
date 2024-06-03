using DatabaseGateway;
using DTOs;
using Entities;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Support_AddEmployee : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public string EmployeeName { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public Command_DTO ReturnData { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }

        public Command_Support_AddEmployee(string employeeName)
        {
            DB_Facade = new DataGatewayFacade();
            EmployeeName = employeeName;
            Status = string.Empty;
            Message = string.Empty;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            EmployeeNameObject = new Dictionary<string, string>
            {
                { "name", EmployeeName }
            };

            Command_DTO validated = Command_Process_Validator();

            if (validated.Status == "ERROR")
            {
                ReturnData = new Command_DTO(Status, Message);
            }
            else
            {
                Employee_DTO employeeResponse = Execute_Command_Process().EmployeeDTO;

                if (employeeResponse != null)
                {
                    Status = employeeResponse.Status;
                    Message = employeeResponse.Message;
                    ReturnData = new Command_DTO(Status, Message, employeeResponse.Employee);
                }
                else
                {
                    ReturnData = new Command_DTO(Status, Message);
                }
            }
            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            Template_DTO action_dto = DB_Facade.Employee_Create(new Employee(EmployeeName));
            return new Command_DTO(action_dto.Status, action_dto.Message, action_dto.ReturnObject);
        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator()
        {
            CommandFactory cf = new CommandFactoryBuilder().WithEmployeeNameObject(EmployeeNameObject).Build();
            Command_DTO dto = cf.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();
            return dto;
        }

    }
}