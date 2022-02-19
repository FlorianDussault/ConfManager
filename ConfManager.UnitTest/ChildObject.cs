using System;

namespace ConfManager.UnitTest
{
    /// <summary>
    /// Child Test Object
    /// </summary>
    public class ChildObject
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            return Id == ((ChildObject)obj).Id;
        }
    }
}
