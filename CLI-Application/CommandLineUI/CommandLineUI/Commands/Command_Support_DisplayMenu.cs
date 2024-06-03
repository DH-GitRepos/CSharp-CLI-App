using DTOs;
using CommandLineUI.MenuManager;

namespace CommandLineUI.Commands
{
    public class Command_Support_DisplayMenu : UI_Command<Command_DTO>
    {
        public static Command_Support_DisplayMenu INSTANCE { get; private set; } = new Command_Support_DisplayMenu();

        private readonly MenuMain menu;

        public Command_DTO ReturnData { get; private set; }

        public Command_Support_DisplayMenu()
        {
            menu = CreateMenu();
        }

        private MenuMain CreateMenu()
        {
            MenuMain menu = new MenuMain("Stock menu");

            menu.Add(CreateStockManagementMenu());
            menu.Add(CreateReportsMenu());
            menu.Add(CreateAppMenu());

            return menu;
        }

        private MenuMain CreateAppMenu()
        {
            MenuMain menu = new MenuMain("App menu");
            menu.Add(new MenuItem(UI_Command<Command_DTO>.EXIT, "Exit"));
            return menu;
        }

        private MenuMain CreateStockManagementMenu()
        {
            MenuMain menu = new MenuMain("Stock Management menu");
            menu.Add(new MenuItem(UI_Command<Command_DTO>.ADD_ITEM_TO_STOCK, "Add item to stock"));
            menu.Add(new MenuItem(UI_Command<Command_DTO>.ADD_QUANTITY_TO_ITEM, "Add quantity to item"));
            menu.Add(new MenuItem(UI_Command<Command_DTO>.TAKE_QUANTITY_FROM_ITEM, "Take quantity from item"));
            return menu;
        }

        private MenuMain CreateReportsMenu()
        {
            MenuMain menu = new MenuMain("Reports menu");
            menu.Add(new MenuItem(UI_Command<Command_DTO>.VIEW_INVENTORY_REPORT, "View inventory report"));
            menu.Add(new MenuItem(UI_Command<Command_DTO>.VIEW_FINANCIAL_REPORT, "View financial report"));
            menu.Add(new MenuItem(UI_Command<Command_DTO>.VIEW_TRANSACTION_LOG, "View transaction log"));
            menu.Add(new MenuItem(UI_Command<Command_DTO>.VIEW_PERSONAL_USAGE_REPORT, "View personal usage report"));
            return menu;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            menu.Print("");
            return new Command_DTO("OK", "Menu displayed");
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}