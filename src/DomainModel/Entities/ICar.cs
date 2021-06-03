namespace DomainModel.Entities
{
    public interface ICar
    {
        int Id { get; }

        int ExternalId { get; }

        int ModificationId { get; }

        int ComplectationId { get; }

        int PackageId { get; }

        int ColorExteriorId { get; }

        int ColorInteriorId { get; }

        decimal Price { get; }

        int Year { get; }

        string Ocn { get; }

        string ModelName { get; }

        bool Deleted { get; }
    }
}