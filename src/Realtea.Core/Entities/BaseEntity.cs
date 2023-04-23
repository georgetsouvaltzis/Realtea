namespace Realtea.Core.Entities
{
    public abstract record BaseEntity
    {
        protected BaseEntity()
        {
            CreatedAt = DateTimeOffset.Now;
        }

        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}

