namespace Oneonones.Domain.Messages
{
    public static class HistoricalsMessages
    {
        public static string InvalidOccurrence(DateTime occurrence) => $"Occurrence {occurrence:yyyy-MM-dd} is invalid.";
        public static string Empty(string email) => $"{email} historical is empty.";
        public static string Empty(string leaderEmail, string ledEmail) => $"{leaderEmail} and {ledEmail} historical is empty.";
        public static string NotFound(string id) => $"Historical with id {id} was not found.";
        public static string NotFound(string leaderEmail, string ledEmail, DateTime occurrence) => $"{leaderEmail} and {ledEmail} one-on-one at {occurrence:yyyy-MM-dd} was not found.";
        public static string Conflict(string leaderEmail, string ledEmail, DateTime occurrence) => $"{leaderEmail} and {ledEmail} one-on-one at {occurrence:yyyy-MM-dd} was already created.";
        public static string Insert(string leaderEmail, string ledEmail, DateTime occurrence) => $"Fail to insert {leaderEmail} and {ledEmail} one-on-one at {occurrence:yyyy-MM-dd}.";
        public static string Update(string leaderEmail, string ledEmail, DateTime occurrence) => $"Fail to update {leaderEmail} and {ledEmail} one-on-one at {occurrence:yyyy-MM-dd}.";
        public static string Delete(string id) => $"Fail to delete historical with id {id}.";
    }
}
