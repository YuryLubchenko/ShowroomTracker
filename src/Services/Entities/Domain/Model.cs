using DomainModel.Entities;
using Services.Entities.Api;

namespace Services.Entities.Domain
{
    public class Model: IModel
    {
        public Model()
        {
        }

        public Model(ShowroomModel model)
        {
            ExternalId = model.Id;
            Name = model.Name;
            Link = model.Link;
            Image = model.Img;
            ModelInfoLink = model.ModelInfoLink;
        }

        public Model(IModel model)
        {
            Id = model.Id;
            ExternalId = model.ExternalId;
            Name = model.Name;
            Link = model.Link;
            Image = model.Image;
            ModelInfoLink = model.ModelInfoLink;
            Deleted = Deleted;
        }

        public int Id { get; set; }

        public int ExternalId { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public string ModelInfoLink { get; set; }

        public bool Deleted { get; }
    }
}