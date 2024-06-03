using DatabaseGateway;
using DTOs;
using Entities;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_ViewFinancialReport : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }

        public Command_Menu_ViewFinancialReport()
        {
            DB_Facade = new DataGatewayFacade();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            Report_DTO systemFeedback = Execute_Command_Process().ReportDTO;

            CommandFactory cf_1 = new CommandFactoryBuilder().WithOutput(systemFeedback.ReportContents).Build();
            cf_1.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();

            ReturnData = new Command_DTO("OK", "Financial report completed.");

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            double total = 0;
            List<string> returnResult = new List<string>();
            Report_DTO return_dto = null;

            Template_DTO transactionLogReport = DB_Facade.Transaction_Read_All();

            List<TransactionLogEntry> transactions = (List<TransactionLogEntry>)transactionLogReport.ReturnObject;

            if (transactionLogReport != null && transactions != null)
            {
                returnResult.Add(string.Format("\nFinancial Report:"));

                foreach (TransactionLogEntry entry in transactions)
                {
                    if (entry.TypeOfTransaction.Equals("Item Added")
                        || entry.TypeOfTransaction.Equals("Quantity Added"))
                    {
                        double cost = entry.ItemPrice * entry.Quantity;
                        returnResult.Add(string.Format(
                            "\n{0}: Total price of item: {1:C}", entry.ItemName, cost
                            ));
                        total += cost;
                    }
                }

                returnResult.Add(string.Format(
                    "\n{0}: {1:C}", "Total price of all items", total
                    ));

                string status = "OK";
                string message = "Report generetad successfully.";
                string reportType = "FINANCIAL_REPORT";

                return_dto = new Report_DTO(status, message, reportType, returnResult);

            }
            else
            {
                string status = "ERROR";
                string message = "Unable to generate report.";
                string reportType = "FINANCIAL_REPORT";

                return_dto = new Report_DTO(status, message, reportType, returnResult);
            }

            return new Command_DTO(return_dto);
        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}