using System;

namespace Entities
{
    public class Item
    {
        public int ID { get; }
        public string Name { get; }
        public int Quantity { get; private set; }
        public DateTime DateCreated { get; }

        public Item(int id, string name, int quantity, DateTime dateCreated)
        {
            string errorMsg = "";

            if (id < 1)
            {
                errorMsg += "ID_BELOW_1";
            }

            if (quantity < 0)
            {
                errorMsg += "QUANTITY_BELOW_ZERO";
            }

            if (name.Length == 0)
            {
                errorMsg += "ITEM_NAME_EMPTY";
            }

            if (errorMsg.Length > 0)
            {
                throw new Exception("ITEM_ENTITY_ERROR -> " + errorMsg);
            }

            ID = id;
            Name = name;
            Quantity = quantity;
            DateCreated = dateCreated;
        }

        public void AddQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception("ERROR: Quantity being added is below 0");
            }

            Quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception("ERROR: Quantity being removed is below 0");
            }

            if (quantity > Quantity)
            {
                throw new Exception("ERROR: Quantity too many");
            }

            Quantity -= quantity;
        }
    }
}
