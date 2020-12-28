using System.ComponentModel.DataAnnotations;

namespace Meetings.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
