namespace EdwardShortener.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ShortedUrls = new HashSet<ShortedUrl>();
        }

        [Key]
        public Guid idUser { get; set; }

        [Required]
        public string userPass { get; set; }

        [Required]
        public string userName { get; set; }
        public string imgScr { get; set; }
        public string realName { get; set; }
        public string gender { get; set; }
        public string dateB { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShortedUrl> ShortedUrls { get; set; }
    }
}
