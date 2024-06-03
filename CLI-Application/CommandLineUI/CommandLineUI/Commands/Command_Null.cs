using DTOs;

namespace CommandLineUI.Commands
{
    // This class implements the Null Object design pattern
    public class Command_Null : UI_Command<Command_DTO>
    {
        public Command_DTO ReturnData { get; private set; }

        public Command_Null()
        {
        }

        public Command_DTO Start_Command_Process() { return new Command_DTO(new Null_DTO()); }
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }

    }
}