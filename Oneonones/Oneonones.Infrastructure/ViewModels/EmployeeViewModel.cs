﻿using Oneonones.Domain.Common;

namespace Oneonones.Infrastructure.ViewModels
{
    public class EmployeeViewModel : Identifier
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}