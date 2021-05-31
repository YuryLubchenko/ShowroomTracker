using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModel.Entities;

namespace Database.Entities
{
    [Table("Models")]
    internal class Model: IModel
    {
        public Model()
        {
        }

        public Model(IModel model)
        {
            CopyProperties(model);
        }

        [Key]
        public int Id { get; set; }

        public int ExternalId { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public string ModelInfoLink { get; set; }

        public bool Deleted { get; set; }

        public void CopyProperties(IModel model)
        {
            Id = model.Id;
            ExternalId = model.ExternalId;
            Name = model.Name;
            Link = model.Link;
            Image = model.Image;
            ModelInfoLink = model.ModelInfoLink;
            Deleted = model.Deleted;
        }
    }
}