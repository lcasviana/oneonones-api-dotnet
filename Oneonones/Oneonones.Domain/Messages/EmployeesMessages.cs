namespace Oneonones.Domain.Messages
{
    public static class EmployeesMessages
    {
        public const string Same = "Leader and led can't be the same.";
        public const string InvalidEmail = "Email is empty or whitespaces.";
        public const string InvalidEmailLeader = "Leader email is empty or whitespaces.";
        public const string InvalidEmailLed = "Led email is empty or whitespaces.";
        public const string InvalidName = "Name is empty or whitespaces.";
        public static string NotFoundId(string id) => $"Employee with id {id} was not found.";
        public static string NotFoundEmail(string email) => $"Employee with email {email} was not found.";
        public static string NotFoundLeader(string id) => $"Leader with id {id} was not found.";
        public static string NotFoundLed(string id) => $"Led with id {id} was not found.";
        public static string Conflict(string email) => $"Employee with email {email} was already created.";
        public static string Insert(string email) => $"Fail to insert employee with email {email}.";
        public static string Update(string email) => $"Fail to update employee with email {email}.";
        public static string Delete(string id) => $"Fail to delete employee with id {id}.";
    }
}