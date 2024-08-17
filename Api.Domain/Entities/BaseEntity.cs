using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        private DateTime? _CreateAt;

        [Key]
        [SwaggerSchema( ReadOnly = true )]
        public Guid Id { get; set; }

        [SwaggerIgnore]
        public DateTime? CreateAt
        {
            get { return _CreateAt; }
            set { _CreateAt = ( value == null ) ? DateTime.UtcNow : value; }
        }

        [SwaggerIgnore]
        public DateTime? UpdateAt { get; set; }
    }
}