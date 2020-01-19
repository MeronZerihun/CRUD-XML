using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Notes.Models
{
    public class Note
    {
        public string id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string body { get; set; }

        public Note(){
            DateTime date = DateTime.Now;
            id = String.Format(
                    "{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                    date.Year, date.Month, date.Day,
                    date.Hour, date.Minute, date.Second, date.Millisecond
                   );
        }
        
    }
}
