namespace SystemProgramming_WPF_01.Model_DataBaseForBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataScienceTable")]
    public partial class DataScienceTable
    {
        [Key]
        public int DataScienceId { get; set; }

        public long MemorySize { get; set; }
    }
}
