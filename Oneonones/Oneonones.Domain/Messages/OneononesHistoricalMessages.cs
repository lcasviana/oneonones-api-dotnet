namespace Oneonones.Domain.Messages
{
    public static class OneononesHistoricalMessages
    {
        public const string InvalidOccurrence = "Occurrence is invalid.";
        public const string NotFound = "{0} and {1} one-on-one at {2} was not found.";
        public const string Conflict = "{0} and {1} one-on-one at {2} was already created.";
        public const string Insert = "Fail to insert {0} and {1} one-on-one at {2}.";
        public const string Update = "Fail to update {0} and {1} one-on-one at {2}.";
        public const string Delete = "Fail to delete {0} and {1} one-on-one at {2}.";
    }
}