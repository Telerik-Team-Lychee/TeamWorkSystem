namespace TWS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using TWS.Data.Repositories;
    using TWS.Models;

    public class TwsData : ITwsData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public TwsData()
            : this(new TwsDbContext())
        {
        }

        public TwsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IRepository<TeamWork> TeamWorks
        {
            get
            {
                return this.GetRepository<TeamWork>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Resource> Resources
        {
            get
            {
                return this.GetRepository<Resource>();
            }
        }

        public IRepository<Assignment> Assignments
        {
            get
            {
                return this.GetRepository<Assignment>();
            }
        }

        public IRepository<TeamWorkRequest> TeamWorkRequests
        {
            get
            {
                return this.GetRepository<TeamWorkRequest>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}