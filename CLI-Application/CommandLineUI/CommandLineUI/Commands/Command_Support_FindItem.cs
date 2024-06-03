using DatabaseGateway;
using DTOs;
using Entities;

namespace CommandLineUI.Commands
{
    public class Command_Support_FindItem : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public int ItemId { get; private set; }

        public Command_Support_FindItem(int itemId)
        {
            DB_Facade = new DataGatewayFacade();
            ItemId = itemId;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            if (ItemId < 1)
            {
                ReturnData = new Command_DTO("ERROR", "Item ID less than 1.");
            }
            else
            {
                Template_DTO item_dto = DB_Facade.Item_Read_One(ItemId);
                Item item = (Item)item_dto.ReturnObject;

                if (item_dto.Status != "OK")
                {
                    ReturnData = new Command_DTO("ERROR", "Item not found.");
                }
                else
                {
                    ReturnData = new Command_DTO("OK", "Item found.", item);
                }
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}