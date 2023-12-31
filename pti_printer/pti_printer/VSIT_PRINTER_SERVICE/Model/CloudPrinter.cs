﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCloudPrint.Model
{
    [DataContract]
    public class CloudPrinter
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string proxy { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string capsHash { get; set; }

        [DataMember]
        public string createTime { get; set; }

        [DataMember]
        public string updateTime { get; set; }

        [DataMember]
        public string accessTime { get; set; }

        [DataMember]
        public bool confirmed { get; set; }

        [DataMember]
        public int numberOfDocuments { get; set; }

        [DataMember]
        public int numberOfPages { get; set; }

        [DataMember]
        public string connectionStatus { get; set; }
    }

    [DataContract]
    public class CloudPrinterResponse
    {
        [DataMember]
        public bool success { get; set; }

        [DataMember]
        public List<CloudPrinter> printers { get; set; }
    }
}
