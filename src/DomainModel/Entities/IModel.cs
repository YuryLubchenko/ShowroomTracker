namespace DomainModel.Entities
{
    public interface IModel
    {
        public int Id { get; }

        public int ExternalId { get; }

        public string Name { get; }

        public string Link { get; }

        public string Image { get; }

        public string ModelInfoLink { get; }

        public bool Deleted { get; }

    }
}