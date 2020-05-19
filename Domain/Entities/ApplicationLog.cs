using Shared.Support.Enums;
using System;

namespace Domain.Entities
{
    public class ApplicationLog
    {
        protected ApplicationLog()
        {

        }

        public ApplicationLog(Exception ex, ApplicationLogTypesEnum applicationLogTypesEnum, string observation, int createdById, string createdBy)
        {
            Exception = ex?.ToString();
            Observation = observation;
            ApplicationLogType = (int)applicationLogTypesEnum;
            CreatedOn = DateTime.Now;
            CreatedById = createdById;
            CreatedBy = createdBy;
        }

        public int Id { get; private set; }

        public string Exception { get; private set; }

        public string Observation { get; private set; }

        public int ApplicationLogType { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public int CreatedById { get; private set; }

        public string CreatedBy { get; private set; }


        public string Type => "Log da Aplicação";
    }
}
