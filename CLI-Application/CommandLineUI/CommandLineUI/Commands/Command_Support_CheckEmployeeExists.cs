using DatabaseGateway;
using DTOs;
using Entities;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Support_CheckEmployeeExists : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Dictionary<string, string> EmployeeName { get; private set; }
        public string ExtractedName { get; private set; }
        public Command_DTO ReturnData { get; private set; }
        public Command_Support_CheckEmployeeExists(Dictionary<string, string> employeeNameObject)
        {
            DB_Facade = new DataGatewayFacade();
            EmployeeName = employeeNameObject;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            ExtractedName = EmployeeName["name"];

            if (ExtractedName == null || ExtractedName == "")
            {
                ReturnData = new Command_DTO("ERROR", "Employee name is null or blank.");
            }
            else
            {
                bool validateNameValue = Command_Process_Validator().Check;

                if (validateNameValue)
                {
                    ReturnData = Execute_Command_Process();
                }
                else
                {
                    ReturnData = new Command_DTO("ERROR", "Employee name is blank.");
                }
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            Command_DTO execute_response;

            Template_DTO employeeResponse = DB_Facade.Employee_Read_One(ExtractedName);

            Employee employee = (Employee)employeeResponse.ReturnObject;

            if (employeeResponse.Status == "OK")
            {
                execute_response = new Command_DTO(employeeResponse.Status, employeeResponse.Message, employee);
            }
            else
            {
                execute_response = new Command_DTO("ERROR", "Employee not found.");
            }

            return execute_response;
        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator()
        {
            if (ExtractedName == "")
            {
                return new Command_DTO(false);
            }
            else
            {
                return new Command_DTO(true);
            }
        }

    }
}