namespace SystemProgramming_WPF_01.Model_DataBaseForBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Process
    {
        public int Id { get; set; }

        public int ProcessId { get; set; }

        [Required]
        [StringLength(64)]
        public string ProcessName { get; set; }

        public long ProcessMemorySize { get; set; }
    }
}
