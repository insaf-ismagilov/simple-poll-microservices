﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Constants;

namespace SimplePoll.Editor.Application.Handlers
{
	public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, int>
	{
		private readonly IDatabaseRepository _databaseRepository;

		public CreatePollCommandHandler(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}

		public Task<int> Handle(CreatePollCommand request, CancellationToken cancellationToken)
		{
			return _databaseRepository.GetAsync<int>(Functions.Poll.Create,
				DbParameterInfoHelper.Create(nameof(request.Title), request.Title),
				DbParameterInfoHelper.Create(nameof(request.Status), request.Status),
				DbParameterInfoHelper.Create(nameof(request.Type), request.Type),
				DbParameterInfoHelper.CreateJsonb(nameof(request.Options), request.Options));
		}
	}
}