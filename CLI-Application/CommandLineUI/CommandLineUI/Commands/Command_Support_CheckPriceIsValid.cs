using DTOs;

namespace CommandLineUI.Commands
{
    public class Command_Support_CheckPriceIsValid : UI_Command<Command_DTO>
    {
        public Command_DTO ReturnData { get; private set; }
        public double Price { get; private set; }

        public Command_Support_CheckPriceIsValid(double price)
        {
            Price = price;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            string status = null;
            string message = null;
            bool check = false;

            if (Price < 0)
            {
                status = "ERROR";
                message = "Price below 0.";
                check = false;
                ReturnData = new Command_DTO(status, message, check);
            }
            else
            {
                status = "OK";
                message = "Price valid.";
                check = true;
                ReturnData = new Command_DTO(status, message, check);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}