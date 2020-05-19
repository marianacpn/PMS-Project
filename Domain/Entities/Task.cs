using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Task
    {
        public Task(Guid id, string code, string name, DateTime startDate, DateTime finishDate, int state, int projectId)
        {
            Id = id;
            Code = code;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
            ProjectId = projectId;
        }

        protected Task()
        {
            _subtasks = new List<Task>();
        }

        public Guid Id { get; set; }
        public string Code { get; private set; }

        public string Name { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime FinishDate { get; private set; }

        public int State { get; private set; }


        public int ProjectId { get; private set; }

        public Project Project { get; private set; }


        private readonly List<Task> _subtasks;
        public ICollection<Task> Subtasks => _subtasks;
    }
}
