using Entities;
using System.Collections.Generic;

namespace DTOs
{
    public class TransactionLogEntry_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public TransactionLogEntry Transaction { get; private set; }

        public List<TransactionLogEntry> Transactions { get; private set; }

        public TransactionLogEntry_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public TransactionLogEntry_DTO(string status, string message, TransactionLogEntry transaction)
        {
            this.Status = status;
            this.Message = message;
            this.Transaction = transaction;
        }

        public TransactionLogEntry_DTO(string status, string message, List<TransactionLogEntry> transactions)
        {
            this.Status = status;
            this.Message = message;
            this.Transactions = transactions;
        }
    }
}
