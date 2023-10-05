using Microsoft.EntityFrameworkCore;
using VsitPrinter.Infrastructure.Entities;

namespace VsitPrinter.Infrastructure
{
    public class PrinterDbContext : DbContext
    {
        public PrinterDbContext(DbContextOptions<PrinterDbContext> options) : base(options)
        {
        }

        public DbSet<PrinterJobExecuted> PrinterJobExecuteds { get; set; }
        public DbSet<PrinterJobFailed> PrinterJobFaileds { get; set; }
        public DbSet<PrinterJobPending> PrinterJobPendings { get; set; }
        public DbSet<PrinterDevice> PrinterDevices { get; set; }
    }
}