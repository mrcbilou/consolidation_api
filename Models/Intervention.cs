using System;
using System.Collections.Generic;

namespace RestApi.Models
{
    public partial class Intervention
    {       
        public int id {get; set;}
        public int employee_id {get; set;}
        public int customer_id {get; set;}
        public int building_id {get; set;}
        public int battery_id {get; set;}
        public int? column_id {get; set;}
        public int? elevator_id {get; set;}
        public string intervention_status {get; set;}
        public int author {get; set;}
        public DateTime? start_date_and_time {get; set;}
        public DateTime? end_date_and_time {get; set;}
        public string result {get; set;}
        public string report {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}
        public static DateTime Today { get; }
    }
}
