using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Translator
{
    public static class RemarkTranslator
    {
        public static Model.Remark ToDomainModel(this DataContract.Remark remark)
        {
            if(remark == null) return null;
            return new Model.Remark()
            {
                Text = remark.Text
            };
        }

        public static DataContract.Remark ToDataContract(this Model.Remark remark)
        {
            if (remark == null) return null;
            return new DataContract.Remark()
            {
                Text = remark.Text,
                CreateTimeStamp = remark.CreateTimeStamp
            };
        }
    }
}
