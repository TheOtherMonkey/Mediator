# Mediator

A very simplistic implementation of the Mediator Design Pattern. 

Allows for asynchronous CQRS requests to be made that are fulfilled by objects that implement the IHandleRequests<TRequest, TResponse> interface.

Also incorporates a centralised in-memory event aggregator to handle the publish/subscribe metaphor.
