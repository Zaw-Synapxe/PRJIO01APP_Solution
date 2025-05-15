using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repositories;
using ToDoList.Domain.Entities;
using ToDoList.Shared;

namespace ToDoList.Application.Features.Players.Commands.UpdatePlayer
{
    public record UpdatePlayerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShirtNo { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    internal class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.Repository<Player>().GetByIdAsync(command.Id);
            if (player != null)
            {
                player.Name = command.Name;
                player.ShirtNo = command.ShirtNo;
                player.PhotoUrl = command.PhotoUrl;
                player.BirthDate = command.BirthDate;

                await _unitOfWork.Repository<Player>().UpdateAsync(player);
                player.AddDomainEvent(new PlayerUpdatedEvent(player));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(player.Id, "Player Updated.");
            }
            else
            {
                return await Result<int>.FailureAsync("Player Not Found.");
            }
        }

        //
    }

    //
}
