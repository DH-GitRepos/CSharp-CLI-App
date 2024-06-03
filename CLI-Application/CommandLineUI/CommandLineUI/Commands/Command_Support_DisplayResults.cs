using DTOs;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class Command_Support_DisplayResults : UI_Command<Command_DTO>
    {
        public List<string> Results { get; private set; }

        public Command_Support_DisplayResults(List<string> results)
        {
            Results = results;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            foreach (string result in Results)
            {
                Console.WriteLine($"{result}");
            }

            return new Command_DTO("OK", "Results displayed.");
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}