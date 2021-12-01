namespace Zamagon.Services.BackOffice;

public class BODatabaseInitializer : IDatabaseInitializer
{
    private Db db;


    public BODatabaseInitializer(Db db)
    {
        this.db = db;
    }

    public async Task Seed(string migrationName)
    {
        Employee sam = new Employee { Name = "Sam", TimeCards = new List<TimeCard>() };
        Employee melinda = new Employee { Name = "Melinda", TimeCards = new List<TimeCard>() };

        TimeCard t1 = new TimeCard { Employee = sam, HoursWorked = 12, WorkDate = DateTime.Now };
        TimeCard t2 = new TimeCard { Employee = sam, HoursWorked = 10, WorkDate = DateTime.Now.AddDays(-7) };

        TimeCard t3 = new TimeCard { Employee = melinda, HoursWorked = 12, WorkDate = DateTime.Now };
        TimeCard t4 = new TimeCard { Employee = melinda, HoursWorked = 9, WorkDate = DateTime.Now.AddDays(-7) };

        db.Entry(sam).State = EntityState.Added;
        db.Entry(melinda).State = EntityState.Added;
        db.Entry(t1).State = EntityState.Added;
        db.Entry(t2).State = EntityState.Added;
        db.Entry(t3).State = EntityState.Added;
        db.Entry(t4).State = EntityState.Added;

        await db.SaveChangesAsync();
    }
}
