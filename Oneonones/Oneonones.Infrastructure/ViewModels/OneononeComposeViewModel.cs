using System.Collections.Generic;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeComposeViewModel
    {
        public OneononeViewModel Oneonone { get; set; }
        public IList<HistoricalViewModel> Historical { get; set; }
        public StatusViewModel Status { get; set; }
    }
}