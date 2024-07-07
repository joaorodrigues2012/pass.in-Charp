using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
	public ResponseRegisterJson Execute(RequestEventJson request) {

		Validate(request);

		var dbContent = new PassInDbContext();

		var entity = new Infrastructure.Entities.Event
		{
			Title = request.Title,
			Details = request.Details,
			Maximum_Attendees = request.MaximumAttendees,
			Slug = request.Title.ToLower().Replace(" ", "-"),
		};

		dbContent.Events.Add(entity);
		
		dbContent.SaveChanges();

		return new ResponseRegisterJson{
			Id = entity.Id
		};
	}

	private void Validate(RequestEventJson request) {

		if (request.MaximumAttendees <= 0) {
			throw new ErrorOnvalidationException("The maximum attendees is invalid.");
		}

		if (string.IsNullOrWhiteSpace(request.Title)) {
			throw new ErrorOnvalidationException("The title is invalid.");
		}

		if (string.IsNullOrWhiteSpace(request.Details))
		{
			throw new ErrorOnvalidationException("The details is invalid.");
		}

	}
}
