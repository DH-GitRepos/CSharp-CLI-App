using DatabaseGateway;
using DTOs;
using Entities;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_ViewTransactionLog : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ProcessReturnDTO { get; private set; }

        public Command_Menu_ViewTransactionLog()
        {
            DB_Facade = new DataGatewayFacade();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            Command_DTO systemFeedack = Execute_Command_Process();
            List<string> reportContents = systemFeedack.ReportDTO.ReportContents;

            CommandFactory cf_1 = new CommandFactoryBuilder().WithOutput(reportContents).Build();
            cf_1.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();

            ProcessReturnDTO = new Command_DTO("OK", "Inventory report completed.");

            return ProcessReturnDTO;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            List<string> returnResult = new List<string>();

            Report_DTO executeReturnDTO;

            Template_DTO transactionLogReport = DB_Facade.Transaction_Read_All();
            List<TransactionLogEntry> transactionList = (List<TransactionLogEntry>)transactionLogReport.ReturnObject;


            if (transactionLogReport != null && transactionList != null)
            {
                returnResult.Add(string.Format("\nTransaction Log:"));
                returnResult.Add(string.Format(
                    "\n{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                    "Date",
                    "Type",
                    "ID",
                    "Name",
                    "Quantity",
                    "Employee",
                    "Price"));

                foreach (TransactionLogEntry entry in transactionList)
                {
                    returnResult.Add(string.Format(
                        "\n{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                        entry.DateAdded.ToString("dd/MM/yyyy"),
                        entry.TypeOfTransaction,
                        entry.ItemID,
                        entry.ItemName,
                        entry.Quantity,
                        entry.EmployeeName,
                        entry.TypeOfTransaction.Equals("Quantity Removed") ? "" : string.Format("{0:C}", entry.ItemPrice)
                        ));
                }

                string status = "OK";
                string message = "Report generated successfully.";
                string type = "TRANSACTION_LOG";

                executeReturnDTO = new Report_DTO(status, message, type, returnResult);
            }
            else
            {
                string status = "ERROR";
                string message = "Unable to generate report.";
                string reportType = "TRANSACTION_LOG";

                executeReturnDTO = new Report_DTO(status, message, reportType, returnResult);
            }

            return new Command_DTO(executeReturnDTO);
        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }

    }
}