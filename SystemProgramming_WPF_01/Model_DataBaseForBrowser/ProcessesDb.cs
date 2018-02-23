namespace SystemProgramming_WPF_01.Model_DataBaseForBrowser
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProcessesDb : DbContext
    {
        //name = ProcessesDb
        public ProcessesDb()
            : base("name=ProcessesDb")
        {
        }

        public virtual DbSet<DataScienceTable> DataScienceTables { get; set; }
        public virtual DbSet<Process> Processes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Process>()
                .Property(e => e.ProcessName)
                .IsUnicode(false);
        }
    }
}
