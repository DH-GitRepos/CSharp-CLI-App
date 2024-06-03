using DatabaseGateway;
using DTOs;
using Entities;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Support_GetItems : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public Dictionary<int, Item> Items { get; private set; }

        public Command_Support_GetItems()
        {
            DB_Facade = new DataGatewayFacade();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            Template_DTO items_query = DB_Facade.Item_Read_All();

            if (items_query.Status == "ERROR")
            {
                ReturnData = new Command_DTO(items_query.Status, items_query.Message);
            }
            else
            {
                Items = (Dictionary<int, Item>)items_query.ReturnObject;
                ReturnData = new Command_DTO(items_query.Status, items_query.Message, Items);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}