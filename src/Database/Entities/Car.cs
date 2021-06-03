using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModel.Entities;

namespace Database.Entities
{
    [Table("Cars")]
    internal class Car: ICar
    {
        public Car()
        {
        }

        public Car(ICar car)
        {
        }

        [Key]
        public int Id { get; set; }

        public int ExternalId { get; set; }

        public int ModificationId { get; set; }

        public int ComplectationId { get; set; }

        public int PackageId { get; set; }

        public int ColorExteriorId { get; set; }

        public int ColorInteriorId { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }

        public string Ocn { get; set; }

        public string ModelName { get; set; }

        public bool Deleted { get; set; }

        public void CopyProperties(ICar car)
        {
            Id = car.Id;
            ExternalId = car.ExternalId;
            ModificationId = car.ModificationId;
            ComplectationId = car.ComplectationId;
            PackageId = car.PackageId;
            ColorExteriorId = car.ColorExteriorId;
            ColorInteriorId = car.ColorInteriorId;
            Price = car.Price;
            Year = car.Year;
            Ocn = car.Ocn;
            ModelName = car.ModelName;
            Deleted = car.Deleted;
        }
    }
}