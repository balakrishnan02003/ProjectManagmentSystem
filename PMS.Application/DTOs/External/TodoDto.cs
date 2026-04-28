using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Application.DTOs.External
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

    }
}
