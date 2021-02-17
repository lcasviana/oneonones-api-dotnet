namespace Oneonones.Domain.Messages
{
    public static class EmployeesMessages
    {
        public const string InvalidEmail = "Email is empty or whitespaces.";
        public const string InvalidEmailLeader = "Leader email is empty or whitespaces.";
        public const string InvalidEmailLed = "Led email is empty or whitespaces.";
        public const string InvalidName = "Name is empty or whitespaces.";
        public const string NotFound = "Employee with email {0} was not found.";
        public const string NotFoundLeader = "Leader with email {0} was not found.";
        public const string NotFoundLed = "Led with email {0} was not found.";
        public const string Conflict = "Employee with email {0} was already created.";
        public const string Insert = "Fail to insert {0}.";
        public const string Update = "Fail to update {0}.";
        public const string Delete = "Fail to delete {0}.";
    }
}