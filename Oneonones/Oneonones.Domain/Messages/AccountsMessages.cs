namespace Oneonones.Domain.Messages
{
    public static class AccountsMessages
    {
        public const string InvalidPassword = "Invalid password.";
        public const string NotFoundAccount = "Account not registered.";
        public static string NotFound(string id) => $"Account with employee id {id} was not found.";
        public static string Conflict(string id) => $"Account with employee id {id} was already created.";
        public static string Insert(string id) => $"Fail to insert account with employee id {id}.";
        public static string Update(string id) => $"Fail to update account with employee id {id}.";
        public static string Delete(string id) => $"Fail to delete account with employee id {id}.";
    }
}