using Entities;
using System.Collections.Generic;

namespace DTOs
{
    public class Item_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public bool Check {  get; private set; }

        public Item Item { get; private set; }

        public Dictionary<int, Item> Items { get; private set; }

        public List<string> SystemFeedback { get; private set; }

        public Item_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public Item_DTO(string status, string message, bool check)
        {
            this.Status = status;
            this.Message = message;
            this.Check = check;
        }

        public Item_DTO(string status, string message, Item item)
        {
            this.Status = status;
            this.Message = message;
            this.Item = item;
        }

        public Item_DTO(string status, string message, Dictionary<int, Item> items)
        {
            this.Status = status;
            this.Message = message;
            this.Items = items;
        }

        public Item_DTO(string status, string message, Item item, List<string> systemFeedback)
        {
            this.Status = status;
            this.Message = message;
            this.Item = item;
            this.SystemFeedback = systemFeedback;
        }
    }
}
