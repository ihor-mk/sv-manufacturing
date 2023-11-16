namespace SunVita.Core.DAL.Entities
{
    public  class DoneTaskEmployee
    {
        public long DoneTaskId { get; set; }
        public long EmployeeId { get; set; }

        public DoneTaskEmployee(long doneTaskId, long employeeId)
        {
            DoneTaskId = doneTaskId;
            EmployeeId = employeeId;
        }
    }
}
