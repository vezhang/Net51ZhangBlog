using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.DBRepository
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _dbContext;
        private IDbSet<T> _dbSet;

        public EfRepository(IDbContext context)
        {
            this._dbContext = context;
        }

        public IDbSet<T> Entities
        {
            get
            {
                if (_dbSet == null)
                    this._dbSet = _dbContext.Set<T>();
                return this._dbSet;
            }
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                this.Entities.Add(item);
                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public void Insert(IEnumerable<T> items)
        {
            try
            {
                if(items == null)
                    throw new ArgumentNullException("items");

                foreach (var item in items)
                {
                    this.Entities.Add(item);
                }

                this._dbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public void Update(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                this._dbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public void Update(IEnumerable<T> items)
        {
            try
            {
                if (items == null)
                    throw new ArgumentNullException("items");

                this._dbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public void Delete(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                this.Entities.Remove(item);

                this._dbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public void Delete(IEnumerable<T> items)
        {
            try
            {
                if (items == null)
                    throw new ArgumentNullException("items");

                foreach (var item in items)
                {
                    this.Entities.Remove(item);
                }

                this._dbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(this.GetErrorText(e), e);
            }
        }

        public IQueryable<T> Table 
        {
            get { return this.Entities; }
        }

        public IQueryable<T> TableNoTracking 
        {
            get { return this.Entities.AsNoTracking(); }
        }

        protected string GetErrorText(DbEntityValidationException e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var vrs in e.EntityValidationErrors)
            {
                foreach (var vr in vrs.ValidationErrors)
                {
                    stringBuilder.AppendFormat("Property Name: {0}, Error:{1} \r\n", vr.PropertyName, vr.ErrorMessage);
                }        
            }

            return stringBuilder.ToString();
        }
    }
}