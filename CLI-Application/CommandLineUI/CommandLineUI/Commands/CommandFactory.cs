using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    // This class implements the Factory Method design pattern
    public class CommandFactory
    {
        public List<string> Output { get; private set; }
        public string EmployeeName { get; private set; }
        public string ItemName { get; private set; }
        public double Price { get; private set; }
        public int ItemId { get; private set; }
        public int ItemQty { get; private set; }
        public DateTime Date { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }
        public TransactionLogEntry Transaction { get; private set; }
        public string TransactionType { get; private set; }
        public Report_DTO ReportDTO { get; private set; }


        public CommandFactory(List<string> output, string employeeName, string itemName, double price, int itemId, int itemQty, DateTime date, Dictionary<string, string> employeeNameObject, TransactionLogEntry transaction, string transactionType, Report_DTO reportDTO)
        {
            Output = output;
            EmployeeName = employeeName;
            ItemName = itemName;
            Price = price;
            ItemId = itemId;
            ItemQty = itemQty;
            Date = date;
            EmployeeNameObject = employeeNameObject;
            Transaction = transaction;
            TransactionType = transactionType;
            ReportDTO = reportDTO;
        }


        public UI_Command<Command_DTO> CreateCommand(int commandCode)
        {
            switch (commandCode)
            {
                case UI_Command<Command_DTO>.ADD_ITEM_TO_STOCK:
                    return new Command_Menu_AddItemToStock();
                case UI_Command<Command_DTO>.ADD_QUANTITY_TO_ITEM:
                    return new Command_Menu_AddQuantityToItem();
                case UI_Command<Command_DTO>.TAKE_QUANTITY_FROM_ITEM:
                    return new Command_Menu_TakeQuantityFromItem();
                case UI_Command<Command_DTO>.VIEW_INVENTORY_REPORT:
                    return new Command_Menu_ViewInventoryReport();
                case UI_Command<Command_DTO>.VIEW_FINANCIAL_REPORT:
                    return new Command_Menu_ViewFinancialReport();
                case UI_Command<Command_DTO>.VIEW_TRANSACTION_LOG:
                    return new Command_Menu_ViewTransactionLog();
                case UI_Command<Command_DTO>.VIEW_PERSONAL_USAGE_REPORT:
                    // return new Command_Menu_ViewPersonalUsageReport(EmployeeName);
                    return new Command_Menu_ViewPersonalUsageReport();
                case UI_Command<Command_DTO>.DISPLAY_MENU:
                    return new Command_Support_DisplayMenu();
                case UI_Command<Command_DTO>.INITIALISE_DATABASE:
                    return new Command_Support_InitialiseDatabase();
                case UI_Command<Command_DTO>.DISPLAY_RESULTS:
                    return new Command_Support_DisplayResults(Output);
                case UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS:
                    return new Command_Support_CheckEmployeeExists(EmployeeNameObject);
                case UI_Command<Command_DTO>.ADD_EMPLOYEE:
                    return new Command_Support_AddEmployee(EmployeeName);
                case UI_Command<Command_DTO>.CHECK_PRICE:
                    return new Command_Support_CheckPriceIsValid(Price);
                case UI_Command<Command_DTO>.ADD_TRANSACTION_LOG_ENTRY:
                    return new Command_Support_AddTransactionLogEntry(Transaction);
                case UI_Command<Command_DTO>.FIND_ITEM:
                    return new Command_Support_FindItem(ItemId);
                case UI_Command<Command_DTO>.GET_ITEMS:
                    return new Command_Support_GetItems();
                case UI_Command<Command_DTO>.POPULATE_TEST_DATA:
                    return new Command_Support_PopulateTestData();
                default:
                    return new Command_Null();
            }
        }
    }
}
