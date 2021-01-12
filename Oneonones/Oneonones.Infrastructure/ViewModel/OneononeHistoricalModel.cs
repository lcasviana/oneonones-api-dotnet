using System;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeHistoricalModel
    {
        public EmployeeModel Leader { get; set; }
        public EmployeeModel Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}