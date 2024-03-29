﻿using MediatR;
using PetStore.Application.Base;

namespace PetStore.Application.Mouse;

public class MyDomainEventHandler : IDomainEventHandler<MyEvent>
{
    public async Task Handle(MyEvent event1, CancellationToken cancellationToken)
    {
        await Unit.Task.ConfigureAwait(false);
    }
}