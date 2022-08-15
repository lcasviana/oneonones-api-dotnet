using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Inputs;

public class OneononeInsert : OneononeUpdate
{
    /// <summary>
    /// Leader's id
    /// </summary>
    /// <example>
    /// da66d861-c980-4ea4-b967-9a9c297c07f6
    /// </example>
    public Guid? LeaderId { get; set; }
    /// <summary>
    /// Led's id
    /// </summary>
    /// <example>
    /// 3e47c543-9e4f-4f80-bddb-b5c0b2775d41
    /// </example>
    public Guid? LedId { get; set; }

}

public class OneononeUpdate
{
    /// <summary>
    /// Oneonone's frequency
    /// </summary>
    public Frequency? Frequency { get; set; }
}
