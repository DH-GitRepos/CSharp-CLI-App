using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Menu_ViewInventoryReport : UI_Command<Command_DTO>
    {
        public Command_DTO ReturnData { get; private set; }

        public Command_Menu_ViewInventoryReport()
        {
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            CommandFactory cf_1 = new CommandFactoryBuilder().Build();
            Command_DTO items_fetch = cf_1.CreateCommand(UI_Command<Command_DTO>.GET_ITEMS).Start_Command_Process();
            Dictionary<int, Item> stockItems = items_fetch.Items;

            Console.WriteLine("\nInventory Report:");
            Console.WriteLine(
            "\n{0, -4} {1, -20} {2, -20}",
            "ID",
            "Name",
            "Quantity");

            foreach (var pair in stockItems)
            {
                int key = pair.Key;
                Item value = pair.Value;

                Console.WriteLine(string.Format(
                    "\n{0, -4} {1, -20} {2, -20}",
                    value.ID,
                    value.Name,
                    value.Quantity
                    ));
            }

            ReturnData = new Command_DTO("OK", "Inventory report completed.");

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }

    }
}