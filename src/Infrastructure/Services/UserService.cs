using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Events;
using MediatR;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublisher _mediator;

        public UserService(IUserRepository repository, IMapper mapper, IPublisher mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Add(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            // Verificações de duplicidade
            if (await _repository.Exists(u => u.Login == user.Login))
                throw new InvalidOperationException("Login já existe.");

            if (await _repository.Exists(u => u.Email == user.Email))
                throw new InvalidOperationException("Email já existe.");

            if (await _repository.Exists(u => u.FirstName == user.FirstName && u.LastName == user.LastName))
                throw new InvalidOperationException("Nome e sobrenome já existem.");

            await _repository.Create(user);
            userDTO.UserId = user.Id;

            // Disparar evento de registro de usuário
            var userRegisteredEvent = new UserRegisteredEvent(user.Email, user.FirstName);
            await _mediator.Publish(new DomainEventNotification<UserRegisteredEvent>(userRegisteredEvent));
        }

        public async Task<UserDTO> GetById(int? id)
        {
            var user = await _repository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _repository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task Remove(int? id)
        {
            var user = await _repository.GetById(id);
            await _repository.Remove(user);
        }

        public async Task Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            // Verificações de duplicidade
            if (await _repository.Exists(u => u.Login == user.Login && u.Id != user.Id))
                throw new InvalidOperationException("Login já existe.");

            if (await _repository.Exists(u => u.Email == user.Email && u.Id != user.Id))
                throw new InvalidOperationException("Email já existe.");

            if (await _repository.Exists(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Id != user.Id))
                throw new InvalidOperationException("Nome e sobrenome já existem.");

            await _repository.Update(user);
        }
    }
}

