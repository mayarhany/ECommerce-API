using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandelResponses
{
    public class ValidationErrorResponses : CustomException
    {
        public ValidationErrorResponses() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
