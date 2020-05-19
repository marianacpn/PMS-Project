using Domain.Exceptions;
using Domain.Interfaces;
using Shared.Support.Enums;
using System;

namespace Domain.Entities
{
    public class AuditableLog
    {
        protected AuditableLog()
        {

        }

        public AuditableLog(ILoggableEntity entity, string observation, AuditableLogTypesEnum auditLogTypesEnum, int createdById, string createdBy)
        {
            if (!(entity is object))
                throw new AuditableLogException($"Varivavel {nameof(entity)} não pode ser null");

            EntityType = entity.Type;
            EntityId = entity.EntityId;
            EntityIdentifier = entity.EntityIdentifier;

            Observation = $"O Usuário '{createdBy}' " + observation;
            AuditLogType = (int)auditLogTypesEnum;
            CreatedById = createdById;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
        }

        public AuditableLog(ILogger logger, AuditableLogTypesEnum auditLogTypesEnum, int createdById, string createdBy)
        {
            if (!(logger is object))
                throw new AuditableLogException($"Varivavel {nameof(logger)} não pode ser null");

            EntityType = logger.Type;
            EntityId = logger.EntityId;
            EntityIdentifier = logger.EntityIdentifier;

            Observation = $"O Usuário '{createdBy}' " + auditLogTypesEnum switch
            {
                AuditableLogTypesEnum.Create => logger.MessageWhenCreated,
                AuditableLogTypesEnum.Update => logger.MessageWhenUpdated,
                AuditableLogTypesEnum.Delete => logger.MessageWhenDeleted,
                _ => throw new AuditableLogException($"Valor {auditLogTypesEnum} não configurado")
            };

            AuditLogType = (int)auditLogTypesEnum;
            CreatedById = createdById;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
        }

        public AuditableLog(string observation, int createdById, string createdBy)
        {
            EntityType = "Informação";
            EntityId = null;
            Observation = observation;
            AuditLogType = (int)AuditableLogTypesEnum.Info;
            CreatedOn = DateTime.Now;
            CreatedById = createdById;
            CreatedBy = createdBy;
        }

        public int Id { get; private set; }

        public string EntityType { get; private set; }

        public int? EntityId { get; private set; }

        public string EntityIdentifier { get; private set; }

        public string Observation { get; private set; }

        public int AuditLogType { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public int CreatedById { get; private set; }

        public string CreatedBy { get; private set; }

        public string Type => "Log de Auditoria";
    }
}
