# some-storage-backend-test

### Solution

The solution used for this tests was:

* .NET 8 Minimal APIs
* Rabbit MQ with MassTransit
* Docker compose to run the RabbitMQ (Rancher)

PixelApi project communicates with the Storage service through the Rabbit MQ queues. This technique improve reability of the solution to store the readings from the users.

To handle the problem of concurrency in the log file, I've used a semaphore to allow the async operation on the file, so, the threads don't block the thread pool of the aspnet.

### Tests

I used Moq with xUnit to do the tests, however I didn't did the integrations tests, because of the time and because minimal api's has some problems with it.

### Docker Images
To build the images, run the powershell script `build-images.ps1`


### Run the solution

To test manually, run:

`docker-compose up` // to run rabbitmq

F5 in the solution and do a request in the postman for example like:

`curl --location 'https://localhost:7201/track' \
--header 'Referer: https://example.com' \
--header 'User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3' \
--header 'X-Forwarded-For: 192.168.1.1'`

Thank you!