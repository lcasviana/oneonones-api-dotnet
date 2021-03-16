using System;
using Oneonones.Domain.Common;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeHistoricalViewModel : Identifier
    {
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}