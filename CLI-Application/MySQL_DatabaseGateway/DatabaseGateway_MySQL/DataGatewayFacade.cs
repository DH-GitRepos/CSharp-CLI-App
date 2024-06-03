using Entities;
using DTOs;

namespace DatabaseGateway
{
    // This class implements a facade
    public class DataGatewayFacade
    {
        private readonly EmployeeGateway DB_Employee;
        private readonly ItemGateway DB_Item;
        private readonly TransactionLogEntryGateway DB_Transaction;

        public DataGatewayFacade()
        {
            DB_Employee = new EmployeeGateway();
            DB_Item = new ItemGateway();
            DB_Transaction = new TransactionLogEntryGateway();
        }
                
        public Template_DTO Item_Create(Item item) { return DB_Item.Create(item); }
        public Template_DTO Item_Read_One(int itemId) { return DB_Item.Read_Find_One(itemId); }
        public Template_DTO Item_Read_All() { return DB_Item.Read_All(); }
        public Template_DTO Item_Update_Add(object item, int itemQty) { return DB_Item.Update_Add(item, itemQty); }
        public Template_DTO Item_Update_Remove(object item, int itemQty) { return DB_Item.Update_Remove(item, itemQty); }

        public Template_DTO Employee_Create(Employee employee) { return DB_Employee.Create(employee); }
        public Template_DTO Employee_Read_One(string name) { return DB_Employee.Read_Find_One(name); }
        public Template_DTO Employee_Read_All() { return DB_Employee.Read_All(); }

        public Template_DTO Transaction_Create(object transaction) { return DB_Transaction.Create(transaction); }
        public Template_DTO Transaction_Read_One(object transaction) { return DB_Transaction.Read_Find_One(transaction); }
        public Template_DTO Transaction_Read_All() { return DB_Transaction.Read_All(); }

    }
}
