using System;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeHistoricalViewModel
    {
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}