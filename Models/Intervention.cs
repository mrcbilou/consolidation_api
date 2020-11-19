using System;
using System.Collections.Generic;

namespace RestApi.Models
{
    public partial class Intervention
    {       
        public long Id {get; set;}
        public long? employee_id {get; set;}
        public long? customer_id {get; set;}
        public long? building_id {get; set;}
        public long? battery_id {get; set;}
        public long? column_id {get; set;}
        public long? elevator_id {get; set;}
        public long? author {get; set;}
        public DateTime? start_date_and_time {get; set;}
        public DateTime? end_date_and_time {get; set;}
        public string status {get; set;}
        public string result {get; set;}
        public string report {get; set;}
        public DateTime? created_at {get; set;}
        public DateTime? updated_at {get; set;}
    }
}
