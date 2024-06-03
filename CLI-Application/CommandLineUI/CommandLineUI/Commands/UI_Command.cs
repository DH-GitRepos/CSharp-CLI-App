namespace CommandLineUI.Commands
{
    // This class and its implementing classes implement the Command design pattern
    public interface UI_Command<Command_DTO>
    {
        public const int ADD_ITEM_TO_STOCK = 1;
        public const int ADD_QUANTITY_TO_ITEM = 2;
        public const int TAKE_QUANTITY_FROM_ITEM = 3;
        public const int VIEW_INVENTORY_REPORT = 4;
        public const int VIEW_FINANCIAL_REPORT = 5;
        public const int VIEW_TRANSACTION_LOG = 6;
        public const int VIEW_PERSONAL_USAGE_REPORT = 7;
        public const int EXIT = 8;
        public const int DISPLAY_MENU = 9;
        public const int INITIALISE_DATABASE = 10;
        public const int DISPLAY_RESULTS = 11;
        public const int CHECK_EMPLOYEE_EXISTS = 12;
        public const int ADD_EMPLOYEE = 13;
        public const int CHECK_PRICE = 14;
        public const int ADD_TRANSACTION_LOG_ENTRY = 15;
        public const int FIND_ITEM = 16;
        public const int GET_ITEMS = 17;
        public const int POPULATE_TEST_DATA = 18;

        // Start_Command_Process method to be implemented in the commands
        public abstract Command_DTO Start_Command_Process();

        // Execute_Command_Process method to be implemented in the commands
        public abstract Command_DTO Execute_Command_Process();

        // Command_Process_Validator method to be implemented in the commands
        public abstract Command_DTO Command_Process_Validator();
    }
}