using DatabaseGateway;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_AddItemToStock : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }
        public Dictionary<string, object> ExecuteData { get; private set; }
        public Dictionary<string, object> ValidateData { get; private set; }

        public Command_Menu_AddItemToStock(int itemId, string itemName, int itemQty)
        {
            DB_Facade = new DataGatewayFacade();
            ValidateData = new Dictionary<string, object>();
            ExecuteData = new Dictionary<string, object>();
        }

        public Command_Menu_AddItemToStock()
        {
            DB_Facade = new DataGatewayFacade();
            ValidateData = new Dictionary<string, object>();
            ExecuteData = new Dictionary<string, object>();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                int itemId = ConsoleReader.ReadInteger("Item ID");
                string itemName = ConsoleReader.ReadString("Item Name");
                int itemQuantity = ConsoleReader.ReadInteger("Item Quantity");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");

                EmployeeNameObject = new Dictionary<string, string>
                    {
                        { "name", employeeName }
                    };

                ValidateData["employeeName"] = employeeName;
                ValidateData["itemId"] = itemId;
                ValidateData["itemName"] = itemName;
                ValidateData["itemQuantity"] = itemQuantity;
                ValidateData["itemPrice"] = itemPrice;

                Validation_DTO validator = Command_Process_Validator().ValidationDTO;

                if (validator.Status == "ERROR")
                {
                    CommandFactory cf_display = new CommandFactoryBuilder().WithOutput(validator.Feedback).Build();
                    cf_display.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();

                    ReturnData = new Command_DTO(validator.Status, validator.Message);
                }
                else
                {
                    ExecuteData["itemId"] = itemId;
                    ExecuteData["itemName"] = itemName;
                    ExecuteData["itemQuantity"] = itemQuantity;

                    Command_DTO executeData = Execute_Command_Process();

                    if (executeData.Status == "OK")
                    {
                        CommandFactory findNewItem = new CommandFactoryBuilder().WithItemID(itemId).Build();
                        Item newItem = findNewItem.CreateCommand(UI_Command<Command_DTO>.FIND_ITEM).Start_Command_Process().ItemEntity;

                        // show inventory report
                                                
                        TransactionLogEntry newTransactionLogEntry = new TransactionLogEntry("Item Added", newItem.ID, newItem.Name, itemPrice, newItem.Quantity, employeeName, DateTime.Now);
                        CommandFactory cf_invReport = new CommandFactoryBuilder().Build();
                        cf_invReport.CreateCommand(UI_Command<Command_DTO>.VIEW_INVENTORY_REPORT).Start_Command_Process();

                        // add transaction to log
                        TransactionLogEntry transaction = new TransactionLogEntry("Item Added", newItem.ID, newItem.Name, itemPrice, newItem.Quantity, employeeName, DateTime.Now);
                        CommandFactory cf_trLog = new CommandFactoryBuilder().WithTransaction(transaction).Build();
                        cf_trLog.CreateCommand(UI_Command<Command_DTO>.ADD_TRANSACTION_LOG_ENTRY).Start_Command_Process();

                        Console.WriteLine(validator.Message);

                        ReturnData = new Command_DTO("OK", validator.Message);
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION_ERROR(Command_Menu_AddItemToStock): " + e.Message);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            Item item;
            Template_DTO returnData;
            Item returnItem;
            int itemId = (int)ExecuteData["itemId"];
            string itemName = (string)ExecuteData["itemName"];
            int itemQty = (int)ExecuteData["itemQuantity"];

            try
            {
                item = new Item(itemId, itemName, itemQty, DateTime.Now);
                returnData = DB_Facade.Item_Create(item);
                returnItem = (Item)returnData.ReturnObject;

                return new Command_DTO(returnData.Status, returnData.Message, returnItem);
            }
            catch (Exception e)
            {
                string status = "ERROR";
                string subMsg = null;

                if (e.ToString().Contains("ID_BELOW_1"))
                {
                    subMsg = "ID below 1.";
                }
                else if (e.ToString().Contains("QUANTITY_BELOW_ZERO"))
                {
                    subMsg = "Quantity below 1.";
                }
                else if (e.ToString().Contains("ITEM_NAME_EMPTY"))
                {
                    subMsg = "Name is empty.";
                }

                string message = "Adding item failed: " + subMsg;

                return new Command_DTO(status, message);
            }

        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator()
        {
            string employeeName = (string)ValidateData["employeeName"];
            int itemId = (int)ValidateData["itemId"];
            string itemName = (string)ValidateData["itemName"];
            int itemQuantity = (int)ValidateData["itemQuantity"];
            double itemPrice = (double)ValidateData["itemPrice"];

            List<string> errorList = new List<string>();
            string status = "OK";
            string message = "";
            string msg_prefix = "\nADD-ITEM: ";
            Validation_DTO return_dto;

            // check name is not empty
            CommandFactory cf_nameCheck = new CommandFactoryBuilder().WithEmployeeNameObject(EmployeeNameObject).Build();
            Command_DTO check_employee_dto = cf_nameCheck.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();

            if (check_employee_dto != null && check_employee_dto.Status == "ERROR")
            {
                status = "ERROR";
                errorList.Add(msg_prefix + check_employee_dto.Status + ": " + check_employee_dto.Message);
            }

            // check itemId is valid
            try
            {
                Item item = new(itemId, "test", 1, DateTime.Now);
            }
            catch (Exception e)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + status + ": " + e.Message);
            }

            // check itemName is valid
            try
            {
                Item tempItem = new(999, itemName, 999, DateTime.Now);
            }
            catch (Exception e)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + "ERROR: " + e.Message);
            }

            // check itemQuantity is valid
            try
            {
                Item tempItem = new(999, "Valid item", itemQuantity, DateTime.Now);
            }
            catch (Exception e)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + "ERROR: " + e.Message);
            }

            // check itemPrice is valid
            CommandFactory cf_priceCheck = new CommandFactoryBuilder().WithPrice(itemPrice).Build();
            Command_DTO check_price_dto = cf_priceCheck.CreateCommand(UI_Command<Command_DTO>.CHECK_PRICE).Start_Command_Process();

            if (check_price_dto != null && check_price_dto.Check == false)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + check_price_dto.Status + ": " + check_price_dto.Message);
            }

            // if all checks passed
            if (status == "OK")
            {
                message = $"\n{itemQuantity} {itemName}(s) added to stock by {employeeName}";

            }

            return_dto = new Validation_DTO(status, message, errorList);
            return new Command_DTO(return_dto);
        }
    }
}