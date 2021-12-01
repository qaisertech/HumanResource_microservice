using System;

namespace Leave.Microservice.ViewModel
{
    public class LeaveViewModel
    {
        public Guid EmployeeID { get; set; }
        public string LeaveName { get; set; }
        public int NumberOfDays { get; set; }
        public string? Status { get; set; }
    }
}
