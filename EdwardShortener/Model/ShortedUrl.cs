namespace EdwardShortener.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShortedUrl")]
    public partial class ShortedUrl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShortedUrl()
        {
            Clicks = new HashSet<Click>();
        }

        [Key]
        public int idShortedUrl { get; set; }

        [Column("shortedUrl")]
        public string shortedUrl1 { get; set; }

        public string longUrl { get; set; }

        public DateTime created { get; set; }

        public int? pageStatus { get; set; }

        public DateTime? lastStatusChange { get; set; }

        public Guid Fk_idUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Click> Clicks { get; set; }

        public virtual User User { get; set; }
    }
}
