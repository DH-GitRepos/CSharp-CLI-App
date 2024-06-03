using DatabaseGateway;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_AddQuantityToItem : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }
        public Dictionary<string, object> ExecuteData { get; private set; }
        public Dictionary<string, object> ValidateData { get; private set; }
        public double Price { get; private set; }

        public Command_Menu_AddQuantityToItem()
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
                int quantityToAdd = ConsoleReader.ReadInteger("How many items would you like to add?");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");

                ValidateData["employeeName"] = employeeName;
                ValidateData["itemId"] = itemId;
                ValidateData["quantityToAdd"] = quantityToAdd;

                EmployeeNameObject = new Dictionary<string, string>
                {
                    { "name", employeeName }
                };
                Price = itemPrice;

                Validation_DTO validator = Command_Process_Validator().ValidationDTO;

                if (validator.Status == "OK")
                {
                    CommandFactory cf_findItem = new CommandFactoryBuilder().WithItemID(itemId).Build();
                    Command_DTO find_item_dto = cf_findItem.CreateCommand(UI_Command<Command_DTO>.FIND_ITEM).Start_Command_Process();
                    Item item = find_item_dto.ItemEntity;

                    ExecuteData["quantityToAdd"] = quantityToAdd;
                    ExecuteData["find_item_dto"] = find_item_dto;

                    Command_DTO execute_return = Execute_Command_Process();
                    Item itemAdd = (Item)execute_return.ReturnItems["item"];

                    TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", itemAdd.ID, itemAdd.Name, itemPrice, quantityToAdd, employeeName, DateTime.Now);
                    CommandFactory cf_trLog = new CommandFactoryBuilder().WithTransaction(transaction).Build();
                    cf_trLog.CreateCommand(UI_Command<Command_DTO>.ADD_TRANSACTION_LOG_ENTRY).Start_Command_Process();

                    CommandFactory cf_2 = new CommandFactoryBuilder().Build();
                    cf_2.CreateCommand(UI_Command<Command_DTO>.VIEW_INVENTORY_REPORT).Start_Command_Process();

                    ReturnData = new Command_DTO(validator.Status, validator.Message);

                }
                else
                {
                    CommandFactory cf_1 = new CommandFactoryBuilder().WithOutput(validator.Feedback).Build();
                    cf_1.CreateCommand(UI_Command<Command_DTO>.DISPLAY_RESULTS).Start_Command_Process();

                    ReturnData = new Command_DTO(validator.Status, validator.Message);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION_ERROR(Command_Menu_AddQuantityToItem>Do_Process): " + e.Message);
            }

            return ReturnData;

        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process()
        {
            int quantity = (int)ExecuteData["quantityToAdd"];
            Command_DTO find_item_response = (Command_DTO)ExecuteData["find_item_dto"];

            string status = null;
            string message = null;
            Command_DTO returnDTO = null;
            Template_DTO processedItemDTO = null;
            List<string> returnResult = new List<string>();

            if (find_item_response.Status == "OK")
            {
                processedItemDTO = DB_Facade.Item_Update_Add(find_item_response.ItemEntity, quantity);

                returnResult.Add(string.Format(
                    "\n{0} items have been added to Item ID: {1} on {2}",
                    quantity,
                    find_item_response.ItemEntity.ID,
                    DateTime.Now.ToString("dd/MM/yyyy")
                    ));

                Dictionary<string, object> results = new Dictionary<string, object>
                {
                    { "item", find_item_response.ItemEntity },
                    { "systemFeedback", returnResult }
                };

                returnDTO = new("OK", processedItemDTO.Message, results);
            }
            else
            {
                if (quantity < 0)
                {
                    status = "ERROR";
                    message = "Cannot add a quantity less than 0.";
                    returnDTO = new Command_DTO(status, message);
                }
                else
                {
                    status = "ERROR";
                    message = "Invalid item.";
                    returnDTO = new Command_DTO(status, message);
                }
            }

            return returnDTO;

        }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator()
        {
            string employeeName = (string)ValidateData["employeeName"];
            int itemId = (int)ValidateData["itemId"];
            int quantityToAdd = (int)ValidateData["quantityToAdd"];

            List<string> errorList = new List<string>();
            string status = "OK";
            string message = "";
            string msg_prefix = "\nADD-QTY: ";
            Validation_DTO return_dto = null;

            // check name is not empty
            CommandFactory cf = new CommandFactoryBuilder().WithEmployeeNameObject(EmployeeNameObject).Build();
            Command_DTO check_employee_dto = cf.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();

            if (check_employee_dto != null && check_employee_dto.Status == "ERROR")
            {
                status = "ERROR";
                errorList.Add(msg_prefix + check_employee_dto.Status + ": " + check_employee_dto.Message);
            }

            // check itemId is valid
            try
            {
                Item item = new Item(itemId, "test", 1, DateTime.Now);
            }
            catch (Exception e)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + status + ": " + e.Message);
            }

            Template_DTO itemResponse = DB_Facade.Item_Read_One(itemId);

            if (itemResponse != null && itemResponse.Status == "ERROR")
            {
                status = "ERROR";
                errorList.Add(msg_prefix + itemResponse.Status + ": " + itemResponse.Message);
            }

            // check itemQuantity is valid
            try
            {
                Item tempItem = new Item(999, "Valid item", quantityToAdd, DateTime.Now);
            }
            catch (Exception e)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + "ERROR: " + e.Message);
            }

            // check itemPrice is valid
            CommandFactory cf_2 = new CommandFactoryBuilder().WithPrice(Price).Build();
            Command_DTO check_price_dto = cf_2.CreateCommand(UI_Command<Command_DTO>.CHECK_PRICE).Start_Command_Process();

            if (check_price_dto != null && check_price_dto.Check == false)
            {
                status = "ERROR";
                errorList.Add(msg_prefix + check_price_dto.Status + ": " + check_price_dto.Message);
            }

            // if all checks passed
            if (status == "OK")
            {
                Template_DTO item_dto = DB_Facade.Item_Read_One(itemId);
                Item item = (Item)item_dto.ReturnObject;
                message = $"\n{quantityToAdd} {item.Name}(s) added to stock by {employeeName}";
            }

            return_dto = new Validation_DTO(status, message, errorList);

            return new Command_DTO(return_dto);
        }
    }
}