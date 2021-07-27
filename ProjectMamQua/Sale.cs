namespace ProjectSEM3.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        public long ID { get; set; }

        public decimal? Price { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public long? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
