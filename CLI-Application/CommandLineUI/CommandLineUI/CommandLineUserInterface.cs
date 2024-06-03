using System;
using CommandLineUI.Commands;
using DTOs;

namespace CommandLineUI
{
    // This class is in the frameworks and drivers circle of the Clean Architecture model
    public class CommandLineUserInterface
    {
        public CommandLineUserInterface() 
        { 
        }

        public void Start()
        {
            CommandFactory factory = new CommandFactoryBuilder().Build();

            Command_DTO connection_dto = factory.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            if (connection_dto != null && connection_dto.Status != "ERROR")
            {
                UI_Command<Command_DTO> displayMenu = factory.CreateCommand(UI_Command<Command_DTO>.DISPLAY_MENU);

                displayMenu.Start_Command_Process();
                int choice = GetMenuChoice();

                while (choice != UI_Command<Command_DTO>.EXIT)
                {
                    factory.CreateCommand(choice).Start_Command_Process();

                    displayMenu.Start_Command_Process();
                    choice = GetMenuChoice();
                }
            }
            else
            {
                Console.WriteLine(connection_dto.Status + ": " + connection_dto.Message);

            }
        }

        private int GetMenuChoice()
        {
            int option = ConsoleReader.ReadInteger("\nOption");
            while (option < 1 || option > 9)
            {
                Console.WriteLine("\nChoice not recognised. Please try again");
                option = ConsoleReader.ReadInteger("\nOption");
            }
            return option;
        }
    }
}
