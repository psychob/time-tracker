using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Spys
{
    internal abstract class IOption
    {
        public enum InnerType
        {
            String,
            Bool,
            Integer,
        }

        public enum TypeTransformation
        {
            None,
        }

        public abstract InnerType Type
        {
            get;
        }

        public abstract TypeTransformation Transformation
        {
            get;
        }

        public abstract void SetString(string val);

        public abstract void SetBool(bool val);

        public abstract void SetInteger(int val);

        public abstract string GetString();

        public abstract bool GetBool();

        public abstract int GetInteger();
    }
}
