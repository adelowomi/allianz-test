using System;
namespace Allianz.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public enum Statuses
    {
        PENDING = 1,
        ACTIVE,
        INACTIVE,
        REJECTED,
        SOLD,
        COMPLETED,
        ACCEPTED,
        REVIEWED,
        FAILED,
        PAID
    }
}

