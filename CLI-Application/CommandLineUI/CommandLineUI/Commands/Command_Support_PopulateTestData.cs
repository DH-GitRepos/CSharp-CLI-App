using DatabaseGateway;
using DTOs;
using Entities;
using System;

namespace CommandLineUI.Commands
{
    public class Command_Support_PopulateTestData : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }

        public Command_Support_PopulateTestData()
        {
            DB_Facade = new DataGatewayFacade();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            try
            {
                // Populate database
                DB_Facade.Employee_Create(new Employee("Graham"));
                DB_Facade.Employee_Create(new Employee("Phil"));
                DB_Facade.Employee_Create(new Employee("Jan"));

                Item item1 = new(1, "Pencil", 10, DateTime.Now);
                TransactionLogEntry tr1 = new("Item Added", item1.ID, item1.Name, 0.25f, item1.Quantity, "Graham", DateTime.Now);
                Item item2 = new(2, "Eraser", 20, DateTime.Now);
                TransactionLogEntry tr2 = new("Item Added", item2.ID, item2.Name, 0.15f, item2.Quantity, "Phil", DateTime.Now);

                DB_Facade.Item_Create(item1);
                Item item1_insertCheck = (Item)DB_Facade.Item_Read_One(item1.ID).ReturnObject;

                DB_Facade.Item_Create(item2);
                Item item2_insertCheck = (Item)DB_Facade.Item_Read_One(item2.ID).ReturnObject;

                DB_Facade.Transaction_Create(tr1);
                DB_Facade.Transaction_Create(tr2);

                int qtyToRemove = 4;
                DB_Facade.Item_Update_Remove(item2, qtyToRemove);
                Item item2_update_RemoveCheck = (Item)DB_Facade.Item_Read_One(item2.ID).ReturnObject;
                TransactionLogEntry tr3 = new("Quantity Removed", item2.ID, item2.Name, -1, qtyToRemove, "Graham", DateTime.Now);
                DB_Facade.Transaction_Create(tr3);

                int qtyToAdd = 2;
                DB_Facade.Item_Update_Add(item2_update_RemoveCheck, qtyToAdd);
                Item item2_update_AddCheck = (Item)DB_Facade.Item_Read_One(item2.ID).ReturnObject;
                TransactionLogEntry tr4 = new("Quantity Added", item2.ID, item2.Name, 0.33, qtyToAdd, "Phil", DateTime.Now);
                DB_Facade.Transaction_Create(tr4);

                ReturnData = new Command_DTO("OK", "Test data created successfully.");
            }
            catch (Exception e)
            {
                ReturnData = new Command_DTO("ERROR", e.Message);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }

    }
}