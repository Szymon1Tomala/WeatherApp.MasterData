<h2> Weather forecast application </h2>

Project will consist of few components:

Gateway - for routing HTTP requests to proper backend services
<br />
Masterdata - app in this repository - for managing users and their cities preferences
<br />
WeatherApplication - front component for returning weather forecast data
<br />
RabbitMQ - message broker for asynchronous communication between services

<h3>Services communication for basic actions</h3>
User create/update/delete flow:

![diagram1](https://github.com/user-attachments/assets/5d8cc1b8-704f-4a05-b85b-18805f14a7fe)


Get weather forecast from external API background job flow:
![diagram2](https://github.com/user-attachments/assets/80c11912-c048-415d-bd68-2babbcbf4f9d)



User checking weather forecast flow:
![diagram3](https://github.com/user-attachments/assets/8793b38c-d5f0-4ea2-98ce-5af54d4f8114)
