using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_Quiz_Class
{
    public class Quiz_Main
    {
        public int Id { get;set; }

        public string Name { get; set; } = string.Empty;

        public List<Question> Questions { get; set; } = new();
    }
}
