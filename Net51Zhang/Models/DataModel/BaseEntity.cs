using System;

namespace Net51Zhang.Models.DataModel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public override int GetHashCode()
        {
            if (Equals(this.Id, default(int)))
            {
                return base.GetHashCode();
            }

            return this.Id.GetHashCode();
        }

        private Type GetUnproxiedType()
        {
            return this.GetType();
        }

        private static bool IsTransient(BaseEntity entity)
        {
            return entity != null && Equals(entity.Id, default(int));
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public virtual bool Equals(BaseEntity entity)
        {
            if (entity == null)
                return false;
            if (ReferenceEquals(this, entity))
                return true;
            if (!IsTransient(this) && !IsTransient(entity) && Equals(this.Id, entity.Id))
            {
                var thisType = this.GetUnproxiedType();
                var type = entity.GetUnproxiedType();

                return thisType.IsAssignableFrom(type) || type.IsAssignableFrom(thisType);
            }

            return false;
        }
    }
}