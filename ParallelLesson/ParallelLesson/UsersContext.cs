namespace ParallelLesson
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("name=UsersContext")
        {
        }       

        public virtual DbSet<User> Users { set; get; }        
    }    
}