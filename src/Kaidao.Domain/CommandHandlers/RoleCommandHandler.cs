using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Role;
using Kaidao.Domain.Commands.Role;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Domain.CommandHandlers
{
    public class RoleCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewRoleCommand, bool>,
        IRequestHandler<UpdateRoleCommand, bool>,
        IRequestHandler<RemoveRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMediatorHandler Bus;

        public RoleCommandHandler(
            IRoleRepository RoleRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _roleRepository = RoleRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewRoleCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var role = new AppRole();
            role.Id = message.Id;
            role.Name = message.Name;
            role.NormalizedName = message.NormalizedName;
            role.IsSystemRole = message.IsSystemRole;

            if (_roleRepository.GetByName(role.Name) != null)
            {
                //Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Role has already been taken."));
                return Task.FromResult(false);
            }

            _roleRepository.Add(role);

            if (Commit())
            {
                //Bus.RaiseEvent(new AuthorRegisteredEvent(author.Id, author.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateRoleCommand message, CancellationToken cancellationToken)
		{
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var role = new AppRole();
            role.Id = message.Id;
            role.Name = message.Name;
            role.NormalizedName = message.NormalizedName;
            role.IsSystemRole = message.IsSystemRole;

            var existingRole = _roleRepository.GetByName(role.Name);

            if (existingRole != null && existingRole.Id != role.Id)
            {
                if (!existingRole.Equals(role))
                {
                    //Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _roleRepository.Update(role);

            if (Commit())
            {
                //Bus.RaiseEvent(new _RoleRepository(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _roleRepository.Dispose();
        }

		public Task<bool> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
		{
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _roleRepository.Remove(request.Id);

            if (Commit())
            {
                //Bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }
	}
}