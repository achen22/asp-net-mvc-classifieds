using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class ClassifiedAd
    {
        /* Fields */
        public int Id { get; set; }

        [MaxLength(128)]
        public string UserId { get; set; }

        public byte TypeId { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public DateTime? Expires { get; set; }

        /* Relationships */
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(TypeId))]
        public ClassifiedType Type { get; set; }
    }

    public class ClassifiedType
    {
        public byte Id { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char")]
        public string Name { get; set; }
    }
}