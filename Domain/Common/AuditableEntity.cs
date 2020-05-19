using System;

namespace Domain.Common
{
    [Serializable]
    public abstract class AuditableEntity
    {
        public int CreatedById { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }


        public int? ModifiedById { get; private set; }
        public string ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }



        public bool Deleted { get; private set; }
        public int? DeletedById { get; private set; }
        public string DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }


        public void MarkAsDeleted(int userId, string userName)
        {
            Deleted = true;
            DeletedById = userId;
            DeletedBy = userName;
            DeletedOn = DateTime.Now;
        }

        public void MarkAsUnDeleted(int userId, string userName)
        {
            Deleted = false;
            ModifiedById = userId;
            ModifiedBy = userName;
            ModifiedOn = DateTime.Now;
        }

        public void MarkAsChanged(int userId, string userName)
        {
            ModifiedById = userId;
            ModifiedBy = userName;
            ModifiedOn = DateTime.Now;
        }

        public void MarkAsCreated(int userId, string userName)
        {
            CreatedById = userId;
            CreatedBy = userName;
            CreatedOn = DateTime.Now;
        }
    }
}
