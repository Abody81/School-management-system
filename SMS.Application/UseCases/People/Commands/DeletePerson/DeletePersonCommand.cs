using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.UseCases.People.Commands.DeletePerson;

public record class DeletePersonCommand(int PersonId);

