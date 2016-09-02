namespace EdwardShortener.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Click
    {
        [Key]
        public int idClciks { get; set; }

        public DateTime created { get; set; }

        public int Fk_idShortedUrl { get; set; }

        public virtual ShortedUrl ShortedUrl { get; set; }
    }
}
