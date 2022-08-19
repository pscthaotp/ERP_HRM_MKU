using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Module.DTO
{
    public class DTO_SmsGateway
    {
        public string responseCode = string.Empty;
        public string responseCode_Value = string.Empty;
        public Hashtable responseData = new Hashtable();
        public string errorMessage = string.Empty;

        public DTO_SmsGateway() { }
    }
}
