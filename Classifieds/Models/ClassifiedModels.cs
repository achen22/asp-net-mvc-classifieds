using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class ClassifiedType
    {
        public byte Id { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char")]
        [Display(Name = "Type")]
        public string Name { get; set; }
    }

    /// <summary>
    /// A classified ad that corresponds to a record in the database.
    /// </summary>
    public class ClassifiedAd
    {
        /* Fields */
        /// <summary>
        /// Auto-generated unique ID for this record; this is 0 for records that have yet to be added to the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key reference to the ApplicationUser model that created this ad
        /// </summary>
        [MaxLength(128)]
        public string UserId { get; set; }

        /// <summary>
        /// Foreign key reference to the ClassifiedType of this ad
        /// </summary>
        public byte TypeId { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }

        /* Relationships */
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(TypeId))]
        public ClassifiedType Type { get; set; }
    }

    /// <summary>
    /// View model to validate input when adding or updating a ClassifiedAd
    /// </summary>
    public class ClassifiedAdViewModel
    {
        public byte TypeId { get; set; }

        [MaxLength(128)]
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpireDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime? ExpireTime { get; set; }

        public int? jsTimezoneOffset { get; set; }
    }
}