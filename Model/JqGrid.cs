namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public class JqGrid
    {
        public string sidx { get; set; }
        public string sord { get; set; }
        public int rows { get; set; }
        public int page { get; set; }
        public int count { get; set; }
    }
}
