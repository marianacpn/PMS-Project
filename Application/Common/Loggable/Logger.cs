using Domain.Interfaces;
using System;

namespace Application.Common.Loggable
{
    public class Logger<TEntity> : ILogger where TEntity : class, ILoggableEntity
    {
        [NonSerialized]
        private readonly Func<string> messageWhenCreate;
        public string MessageWhenCreated => messageWhenCreate.Invoke();

        [NonSerialized]
        private readonly Func<string> messageWhenUpdate;
        public string MessageWhenUpdated => messageWhenUpdate.Invoke();

        [NonSerialized]
        private readonly Func<string> messageWhenDelete;
        public string MessageWhenDeleted => messageWhenDelete.Invoke();

        public Logger(Func<string> messageWhenCreate = null, Func<string> messageWhenUpdate = null, Func<string> messageWhenDelete = null)
        {
            this.messageWhenCreate = messageWhenCreate ?? DefaultMessageWhenCreated;
            this.messageWhenUpdate = messageWhenUpdate ?? DefaultMessageWhenUpdated;
            this.messageWhenDelete = messageWhenDelete ?? DefaultMessageWhenDeleted;
        }

        public Logger(TEntity oldValue) : this()
        {
            SetValue(oldValue);
        }

        public string Type { get; private set; }

        public int EntityId { get; private set; }

        public string EntityIdentifier { get; private set; }

        public TEntity OldEntity { get; private set; }
        public TEntity Entity { get; private set; }

        private string LogTextCreate { get { return $"adicionou um(a) novo(a) {Type} "; } }
        private string LogTextDelete { get { return $"excluiu o(a) {Type} "; } }
        private string LogTextUpdate { get { return $"editou o(a) {Type} '{EntityIdentifier}' "; } }

        public void SetValue(TEntity obj)
        {
            Type = obj.Type;
            EntityId = obj.EntityId;
            EntityIdentifier = obj.EntityIdentifier;

            Entity = obj;
            OldEntity = LoggableMethods.DeepClone(obj);
        }

        public string DefaultMessageWhenCreated()
        {
            return $"{LogTextCreate}{LoggableMethods.LogToCreate(this)}";
        }

        public string DefaultMessageWhenUpdated()
        {
            (bool, string) result = LoggableMethods.LogToUpdate(this);

            if (result.Item1)
                return $"{LogTextUpdate}{result.Item2}";

            return $"{LogTextUpdate}editou campos não rastreados pelo log";
        }

        public string DefaultMessageWhenDeleted()
        {
            return $"{LogTextDelete}{LoggableMethods.LogToDelete(this)}";
        }
    }
}
