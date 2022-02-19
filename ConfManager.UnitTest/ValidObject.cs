using System;
using System.Collections.Generic;

namespace ConfManager.UnitTest
{
    public class ValidObject
    {
        public Guid Id { get; set; }

        public List<ChildObject> Children;

        public override bool Equals(object obj)
        {
            ValidObject validObject = (ValidObject)obj;

            if (validObject == null) return false;

            if (Id != validObject.Id)
                return false;

            if (Children == null)
                return validObject.Children == null;

            if (validObject.Children == null)
                return false;

            if (Children.Count != validObject.Children.Count) return false;

            for (int i = 0; i < Children.Count; i++)
                if (!Equals(Children[i], validObject.Children[i]))
                    return false;

            return true;
        }
    }
}
