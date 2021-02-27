using System.Collections.Generic;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeComposeViewModel
    {
        public OneononeViewModel Oneonone { get; set; }
        public IList<OneononeHistoricalViewModel> Historical { get; set; }
        public OneononeStatusViewModel Status { get; set; }
    }
}