using System.ComponentModel.DataAnnotations;

namespace HRA.Transversal.CustomValidation
{
    public class RequiredNullAttribute : ValidationAttribute
    {
        public RequiredNullAttribute()
        {}

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            if (strValue.ToLower() == "null")
            {
                return false;
            }

            return true;
        }
    }
}
