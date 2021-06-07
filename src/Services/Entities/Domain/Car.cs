using DomainModel.Entities;
using Services.Entities.Api;

namespace Services.Entities.Domain
{
    internal class Car: ICar
    {
        public Car(ShowroomCar car)
        {
            ExternalId = car.Id;
            ModificationId = car.ModificationId;
            ComplectationId = car.ComplectationId;
            PackageId = car.PackageId;
            ColorExteriorId = car.ColorExteriorId;
            ColorInteriorId = car.ColorInteriorId;
            Price = car.Price;
            Year = car.Year;
            Ocn = car.Ocn;
            ModelName = car.ModelName;
        }

        public Car(ICar car)
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
    }
}