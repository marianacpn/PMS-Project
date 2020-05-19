using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Project
    {
        public Project(Guid id, string code, string name, DateTime startDate, DateTime finishDate, int state)
        {
            Id = id;
            Code = code;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
        }

        protected Project()
        {
            _tasks = new List<Task>();
            _subprojects = new List<Project>();
        }

        public Guid Id { get; set; }
        public string Code { get; private set; }

        public string Name { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime FinishDate { get; private set; }

        public int State { get; private set; }


        private readonly List<Task> _tasks;
        public ICollection<Task> Tasks => _tasks;

        private readonly List<Project> _subprojects;

   

        public ICollection<Project> Subprojects => _subprojects;

    }
}
