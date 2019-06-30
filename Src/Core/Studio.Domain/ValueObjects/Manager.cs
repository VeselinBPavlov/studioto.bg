namespace Studio.Domain.ValueObjects
{
    using Studio.Common;
    using Studio.Domain.Exceptions;
    using Studio.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;

    public class Manager : ValueObject<Manager>
    {
        private Manager()
        {
        }

        public static Manager For(string accountString)
        {
            var manager = new Manager();

            try
            {
                var index = accountString.IndexOf(" ", StringComparison.Ordinal);
                manager.FirstName = accountString.Substring(0, index);
                manager.LastName = accountString.Substring(index + 1);
            }
            catch (ArgumentException)
            {
                throw new ManagerInvalidException(new ArgumentException(GlobalConstants.ManagerException));
            }

            return manager;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public static implicit operator string(Manager manager)
        {
            return manager.ToString();
        }

        public static explicit operator Manager(string managerString)
        {
            return For(managerString);
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.FirstName;
            yield return this.LastName;
        }
    }
}
