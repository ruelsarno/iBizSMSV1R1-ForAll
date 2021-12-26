using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Functions
{
    public static class Messages
    {
        public static string messagesuccess = "Record added successfully!";
        public static string messageupdatesuccess = "Record updated successfully!";
        public static string messagedeletesuccess = "Record deleted successfully!";

        public static string messageunsuccessfulprocess = "Record process not successfully!";

        public static string messagenotexists = "Record does not exists!";
        public static string messageexists = "Record exists!";

        public static string messagesessionexpire = "Your session is expired!";
        public static string messageconfirmed = "Transaction was confirmed. Process is prohibited!";
        public static string messagenotsuccessful = "Transaction was confirmed. Process was not successfull!";
    }
}
