using DatabaseGateway;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_ViewPersonalUsageReport : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }
        public Dictionary<string, object> ExecuteData { get; private set; }
        public string EmpName { get; private set; }

        public Command_Menu_ViewPersonalUsageReport()
        {
            DB_Facade = new DataGatewayFacade();
            ExecuteData = new Dictionary<string, object>();
            EmpName = null;
        }

        public Command_Menu_ViewPersonalUsageReport(string empName)
        {
            DB_Facade = new DataGatewayFacade();
            ExecuteData = new Dictionary<string, object>();
            EmpName = empName;

        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            string employeeName;

            if (EmpName != null)
            {
                employeeName = EmpName;
            }
            else
            {
                employeeName = ConsoleReader.ReadString("Employee name");
            }


            try
            {
                ExecuteData["employeeName"] = employeeName;

                EmployeeNameObject = new Dictionary<string, string>
                {
                    { "name", employeeName }
                };

                Validation_DTO validator = Command_Process_Validator().ValidationDTO;

                if (validator.Status == "ERROR")
                {
                    CommandFactory cf_1 = new CommandFactoryBuilder().WithOutput(validator.Feedback).Build();
                    cf_1.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();
                    ReturnData = new Command_DTO(validator.Status, validator.Message);

                }
                else
                {
                    Report_DTO reportContents = Execute_Command_Process().ReportDTO;

                    CommandFactory cf_2 = new CommandFactoryBuilder().WithOutput(reportContents.ReportContents).Build();
                    cf_2.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();
                    ReturnData = new Command_DTO(validator.Status, validator.Message);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION_ERROR: " + e.Message);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            string employeeName = (string)ExecuteData["employeeName"];

            List<string> returnResult = new List<string>();
            Report_DTO return_dto;

            Template_DTO transactionLogReport = DB_Facade.Transaction_Read_All();
            List<TransactionLogEntry> transactionList = (List<TransactionLogEntry>)transactionLogReport.ReturnObject;

            if (transactionLogReport != null && transactionList != null)
            {
                returnResult.Add(string.Format("\nPersonal Usage Report for {0}:", employeeName));
                returnResult.Add(string.Format(
                    "\n{0, -20} {1, -10} {2, -12} {3, -12}",
                    "Date Taken",
                    "ID",
                    "Name",
                    "Quantity Removed"));

                foreach (TransactionLogEntry entry in transactionList)
                {
                    if (entry.TypeOfTransaction.Equals("Quantity Removed") && entry.EmployeeName == employeeName)
                    {
                        returnResult.Add(string.Format(
                            "\n{0, -20} {1, -10} {2, -12} {3, -12}",
                            entry.DateAdded,
                            entry.ItemID,
                            entry.ItemName,
                            entry.Quantity
                            ));
                    }
                }

                string status = "OK";
                string message = "Report generated successfully.";
                string type = "PERSONAL_USAGE";

                return_dto = new Report_DTO(status, message, type, returnResult);
            }
            else
            {
                string status = "ERROR";
                string message = "Unable to generate report.";
                string reportType = "PERSONAL_USAGE";

                return_dto = new Report_DTO(status, message, reportType, returnResult);
            }

            return new Command_DTO(return_dto);
        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator()
        {
            List<string> errorList = new List<string>();
            string status = "OK";
            string message = "";
            string msg_prefix = "\nPERSONAL-USAGE-REPORT: ";
            string employeeName = EmployeeNameObject["name"];
            Validation_DTO return_dto;

            // check name is not empty
            CommandFactory cf = new CommandFactoryBuilder().WithEmployeeNameObject(EmployeeNameObject).Build();
            Command_DTO check_employee_dto = cf.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();

            if (check_employee_dto != null && check_employee_dto.Status == "ERROR")
            {
                status = "ERROR";
                errorList.Add(msg_prefix + check_employee_dto.Status + ": " + check_employee_dto.Message);
            }

            if (status == "OK")
            {
                message = $"\nEmployee found.";
            }

            return_dto = new Validation_DTO(status, message, errorList);

            return new Command_DTO(return_dto);
        }
    }
}