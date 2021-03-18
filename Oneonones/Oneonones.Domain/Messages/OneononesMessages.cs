namespace Oneonones.Domain.Messages
{
    public static class OneononesMessages
    {
        public static string InvalidFrequency(int frequency) => $"Frequency {frequency} is not valid.";
        public static string NotFound(string leaderEmail, string ledEmail) => $"{leaderEmail} and {ledEmail} one-on-one was not found.";
        public static string Conflict(string leaderEmail, string ledEmail) => $"{leaderEmail} and {ledEmail} one-on-one was already created.";
        public static string Insert(string leaderEmail, string ledEmail) => $"Fail to insert {leaderEmail} and {ledEmail} one-on-one.";
        public static string Update(string leaderEmail, string ledEmail) => $"Fail to update {leaderEmail} and {ledEmail} one-on-one.";
        public static string Delete(string leaderEmail, string ledEmail) => $"Fail to delete {leaderEmail} and {ledEmail} one-on-one.";
    }
}