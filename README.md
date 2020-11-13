# Mailer

1. Project has configured swagger, in memory database, and it's ready to be running.
2. Only mocked part is SmtpClient.
3. Logging is skipped to keep it as clear as possible.
4. Service architecture is chosen assuming that number of emails won't be very big. Otherwise, instead of using hosted service, something more separated should be used - e.q separated windows service.
5. I added few sample test for both application and domain layer, to present how I work with both types. Anyway, in real use, for application layer some integrations tests should be written, to test hosted service.
6. Queries handlers can be called directly from controller, like in SendAll method - I moved them to service to avoid adding mapping layer - to keep everything simple.
