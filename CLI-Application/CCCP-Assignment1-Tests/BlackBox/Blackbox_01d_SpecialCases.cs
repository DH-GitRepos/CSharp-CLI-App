using DatabaseGateway;
using DTOs;
using CommandLineUI.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.BlackBox.SpecialCases
{
    [TestClass]
    public class Blackbox_01d_SpecialCases
    {
        [TestMethod]
        public void T28_TestCorrectReportIsReturned_InventoryReport()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            CommandFactory cf_trLog = new CommandFactoryBuilder().Build();
            Command_DTO report_response = cf_trLog.CreateCommand(UI_Command<Command_DTO>.VIEW_INVENTORY_REPORT).Start_Command_Process();

            Assert.AreEqual("OK", report_response.Status);
        }


        [TestMethod]
        public void T29_TestCorrectReportIsReturned_FinancialReport()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            CommandFactory cf_trLog = new CommandFactoryBuilder().Build();
            Command_DTO report_response = cf_trLog.CreateCommand(UI_Command<Command_DTO>.VIEW_FINANCIAL_REPORT).Start_Command_Process();

            Assert.AreEqual("OK", report_response.Status);
        }


        [TestMethod]
        public void T30_TestCorrectReportIsReturned_TransactionReport()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            CommandFactory cf_trLog = new CommandFactoryBuilder().Build();
            Command_DTO report_response = cf_trLog.CreateCommand(UI_Command<Command_DTO>.VIEW_TRANSACTION_LOG).Start_Command_Process();

            Assert.AreEqual("OK", report_response.Status);
        }

        /*
        [TestMethod]
        public void T31_TestCorrectReportIsReturned_PersonalUsageReport()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            string employee_name = "Phil";

            CommandFactory cf_trLog = new CommandFactoryBuilder().WithEmployeeName(employee_name).Build();
            Command_DTO report_response = cf_trLog.CreateCommand(UI_Command<Command_DTO>.VIEW_PERSONAL_USAGE_REPORT).Start_Command_Process();

            Assert.AreEqual("OK", report_response.Status);
        }
        */

    }

}
