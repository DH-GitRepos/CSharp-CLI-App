using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace CommandLineUI.Commands
{
    public class CommandFactoryBuilder
    {
        public List<string> Output { get; private set; }
        public string EmployeeName { get; private set; }
        public string ItemName { get; private set; }
        public double Price { get; private set; }
        public int ItemId { get; private set; }
        public int ItemQty { get; private set; }
        public DateTime Date { get; private set; }
        public Dictionary<string, string> EmployeeNameObject { get; private set; }
        public TransactionLogEntry Transaction { get; private set; }
        public string TransactionType { get; private set; }
        public Report_DTO ReportDTO { get; private set; }


        public CommandFactoryBuilder()
        {
            Output = new List<string>();
            EmployeeName = string.Empty;
            ItemName = string.Empty;
            Price = 0;
            ItemId = 0;
            ItemQty = 0;
            EmployeeNameObject = new Dictionary<string, string>();
            Transaction = null;
            TransactionType = null;
            ReportDTO = null;
        }

        public CommandFactory Build()
        {
            return new CommandFactory(Output, EmployeeName, ItemName, Price, ItemId, ItemQty, Date, EmployeeNameObject, Transaction, TransactionType, ReportDTO);
        }

        public CommandFactoryBuilder WithOutput(List<string> output)
        {
            Output = output;
            return this;
        }

        public CommandFactoryBuilder WithEmployeeName(string employeeName)
        {
            EmployeeName = employeeName;
            return this;
        }

        public CommandFactoryBuilder WithItemName(string itemName)
        {
            ItemName = itemName;
            return this;
        }

        public CommandFactoryBuilder WithPrice(double price)
        {
            Price = price;
            return this;
        }

        public CommandFactoryBuilder WithItemID(int itemId)
        {
            ItemId = itemId;
            return this;
        }

        public CommandFactoryBuilder WithItemQty(int itemQty)
        {
            ItemQty = itemQty;
            return this;
        }

        public CommandFactoryBuilder WithDate(DateTime date)
        {
            Date = date;
            return this;
        }

        public CommandFactoryBuilder WithEmployeeNameObject(Dictionary<string, string> employeeNameObject)
        {
            EmployeeNameObject = employeeNameObject;
            return this;
        }

        public CommandFactoryBuilder WithTransaction(TransactionLogEntry transaction)
        {
            Transaction = transaction;
            return this;
        }

        public CommandFactoryBuilder WithTransactionType(string transactionType)
        {
            TransactionType = transactionType;
            return this;
        }

        public CommandFactoryBuilder WithReportDTO(Report_DTO reportDTO)
        {
            ReportDTO = reportDTO;
            return this;
        }
    }
}
