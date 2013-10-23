using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Utilities
{
    public static class Basic
    {
        public static int GenerateId(List<Interfaces.IIdentificable> toCheck)
        {
            var id = 1;
            foreach (var o in toCheck)
            {
                if (o.ID >= id)
                {
                    id = o.ID + 1;
                }
            }
            return id;
        }
    }
}
