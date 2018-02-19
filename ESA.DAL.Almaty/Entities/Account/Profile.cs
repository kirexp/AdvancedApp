using System;

namespace DAL.Entities.Account {
    public class Profile : IEntity, IProfile, ICloneable {
        public virtual long Id { get; set; }
        public virtual string IIN { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime BirthDate { get; set; }

        public virtual string Region { get; set; }
        public virtual string City { get; set; }
        public virtual string District { get; set; }
        public virtual string Street { get; set; }
        public virtual string Building { get; set; }
        public virtual string Flat { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Department { get; set; }
        public virtual string Position { get; set; }
        public virtual string Email { get; set; }

      

        public override string ToString() {
            return string.Format("{0} {1} {2}", this.LastName, this.FirstName, this.MiddleName).Trim();
        }
        public virtual object Clone() {
            var result = new Profile();
            result = this.MemberwiseClone() as Profile;
            return result;
        }
    }
}
